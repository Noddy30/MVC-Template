using System;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Viewmodels;
using Template.Data;

namespace Template.Repositories.Users
{
	public interface IUserRepository
	{
        Task CreateAsync(ApplicationUser userModel);
        Task UpdateAsync(string id, ApplicationUser loginModel);
        Task<ApplicationUser?> GetModelAsync(string id);
        Task DeleteAsync(string id);
        Task RestoreAsync(string id);
        Task<List<ApplicationUser?>> GetAllUsersAsync();
        Task<string> GetUserRoleByUserIdAsync(string id);
        Task<(IdentityResult Result, string UserId)> CreateUserAsync(UserViewModel viewModel);
        Task<(IdentityResult Result, string UserId)> UpdateUserAsync(string id, UserViewModel viewModel);
        //Task<string> GetPaginated();
    }
    public class UserRepository : IUserRepository
	{
        private readonly AppDbContext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(AppDbContext dbcontext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateAsync(ApplicationUser userModel)
        {
            _dbcontext.ApplicationUsers.Add(userModel);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var model = await _dbcontext.ApplicationUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return;
            }
            //_dbcontext.ApplicationUsers.Remove(model);
            //await _dbcontext.SaveChangesAsync();

            model.IsDeleted = true;
            await _userManager.UpdateAsync(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<ApplicationUser?> GetModelAsync(string id)
        {
            var model = await _dbcontext.ApplicationUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task<List<ApplicationUser?>>GetAllUsersAsync()
        {
            var model = await _dbcontext.ApplicationUsers.Where(w => w.IsDeleted == false).ToListAsync();
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task<string>GetUserRoleByUserIdAsync(string id)
        {
            var userRoleid = await _dbcontext.UserRoles.Where(w => w.UserId == id).Select(s => s.RoleId).FirstOrDefaultAsync();
            var role = await _dbcontext.Roles.Where(w => w.Id == userRoleid).Select(s => s.Name).FirstOrDefaultAsync();

            if (role == null)
            {
                return null;
            }
            return role;
        }

        public async Task<(IdentityResult Result, string UserId)> CreateUserAsync(UserViewModel viewModel)
        {
            var userModel = new ApplicationUser
            {
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                FirstName = viewModel.Name,
                LastName = viewModel.LastName,
                UserName = viewModel.Email
            };

            var result = await _userManager.CreateAsync(userModel);
            if (!result.Succeeded)
            {
                return (result, userModel.Id);
            }
            // Only assign a role to the user if they get successfully registered
            var role = await _roleManager.FindByNameAsync(viewModel.Role);
            var roleResult = await _userManager.AddToRoleAsync(userModel, role.Name);
            // Check if roles were successfully added to user
            if (!roleResult.Succeeded)
            {
                return (roleResult, userModel.Id);
            }

            // Success
            return (result, userModel.Id);
        }

        public async Task<(IdentityResult Result, string UserId)> UpdateUserAsync(string id, UserViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                // Handle the case where the user with the provided ID doesn't exist.
                return (IdentityResult.Failed(), id);
            }

            // Compare and update fields that have changed
            if (user.Email != viewModel.Email)
            {
                user.Email = viewModel.Email;
            }

            if (user.PhoneNumber != viewModel.PhoneNumber)
            {
                user.PhoneNumber = viewModel.PhoneNumber;
            }

            if (user.FirstName != viewModel.Name)
            {
                user.FirstName = viewModel.Name;
            }

            if (user.LastName != viewModel.LastName)
            {
                user.LastName = viewModel.LastName;
            }

            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, viewModel.Password);
            }

            // Check if the role has changed
            var userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            if (!userRoles.Contains(viewModel.Role))
            {
                // Removing previous roles
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                {
                    return (removeResult, user.Id);
                }

                // Adding new role
                var role = await _roleManager.FindByNameAsync(viewModel.Role);
                var addResult = await _userManager.AddToRoleAsync(user, role.Name);
                if (!addResult.Succeeded)
                {
                    return (addResult, user.Id);
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            return (updateResult, user.Id);
        }

        public async Task RestoreAsync(string id)
        {
            var model = await _dbcontext.ApplicationUsers
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id);

            model.IsDeleted = false;

            _dbcontext.Update(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, ApplicationUser loginModel)
        {
            var login = await _dbcontext.ApplicationUsers.Where(x => x.Id == id).FirstOrDefaultAsync();
            _dbcontext.ApplicationUsers.Update(login);
            await _dbcontext.SaveChangesAsync();
        }

        //public async Task<string> GetPaginated()
        //{
        //    var data = _dbContext.Course.AsNoTracking().AsQueryable();
        //    var totalRecords = await data.CountAsync();
        //    var filter = model.Search.Value;
        //    var column = model.Order[0].Column.ToString();
        //    var direction = model.Order[0].Dir;
        //    var pageSize = model.Length ?? 50;
        //    var start = model.Start;
        //    int initialPage = start / pageSize ?? 0;
        //    if (!string.IsNullOrEmpty(filter))
        //    {
        //        var filterUpper = filter.ToUpper();
        //        // Create a list of allowed enum values based on the filter
        //        var allowedCourseTypes = Enum.GetValues(typeof(Shared.Constants.Enums.CourseType))
        //                                     .Cast<Shared.Constants.Enums.CourseType>()
        //                                     .Where(ct => ct.ToString().Replace("_", " ").ToUpper().Contains(filterUpper))
        //                                     .ToList();
        //        data = data.Where(x =>
        //            x.Name.ToUpper().Contains(filterUpper) ||
        //            x.Institution.Name.ToUpper().Contains(filterUpper) ||
        //            x.Faculty.Name.ToUpper().Contains(filterUpper) ||
        //            allowedCourseTypes.Contains(x.CourseType));
        //    }
        //    if (!string.IsNullOrEmpty(model.LetterFilter) && model.LetterFilter != "All")
        //    {
        //        var letterUpper = model.LetterFilter.ToUpper();
        //        data = data.Where(x => x.Name.ToUpper().StartsWith(letterUpper));
        //    }
        //    // Order data
        //    data = column switch
        //    {
        //        "1" => direction == "asc" ?
        //            data.OrderBy(x => x.Name) :
        //            data.OrderByDescending(x => x.Name),
        //        "2" => direction == "asc" ?
        //            data.OrderBy(x => x.Institution.Name) :
        //            data.OrderByDescending(x => x.Institution.Name),
        //        "3" => direction == "asc" ?
        //            data.OrderBy(x => x.Faculty.Name) :
        //            data.OrderByDescending(x => x.Faculty.Name),
        //        "4" => direction == "asc" ?
        //            data.OrderBy(x => x.CourseType) :
        //            data.OrderByDescending(x => x.CourseType),
        //        _ => data
        //    };
        //    var recordsFiltered = await data.CountAsync();
        //    var test = data.ToList();
        //    // Paginate
        //    var filtered = await data
        //        .Skip(initialPage * pageSize)
        //        .Take(pageSize)
        //        .Select(x => new
        //        {
        //            Id = x.PublicId,
        //            Name = x.Name,
        //            Institution = x.Institution.Name,
        //            Faculty = x.Faculty.Name,
        //            Type = x.CourseType == null ? "" : x.CourseType.ToString().Replace("_", " "),
        //            Method = x.MethodOfDelivery == null ? "" : x.MethodOfDelivery.ToString().Replace("_", " "),
        //            NQF_Level = x.NQFType == null ? "" : x.NQFType.ToString().Replace("_", " "),
        //            Disabilities = x.isDisability,
        //            Learnership = _dbContext.Course_LinkedLearnerships.Where(w => w.CourseId == x.Id).Select(s => s.LearnershipId).Count(),
        //            Occupation = _dbContext.Course_LinkedOccupations.Where(w => w.CourseId == x.Id).Select(s => s.OccupationId).Count(),
        //            CareerPath = _dbContext.Course_LinkedOccupations.Where(w => w.Deleted == false && w.CourseId != null && w.CourseId == x.Id).Count() >= 1 ? true : false,
        //            CourseChooser = _dbContext.RequiredScores.Where(w => w.Deleted == false && w.CourseId != null && w.CourseId == x.Id).Count() >= 1 ? true : false
        //        })
        //        .ToListAsync();
        //    var response = new { model.Draw, recordsTotal = totalRecords, recordsFiltered, data = filtered };
        //    var serialized = JsonConvert.SerializeObject(response);
        //    return serialized;
        //}
    }
}

