using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using Template.Data;

namespace Template.Repositories.Roles
{
    public interface IRoleRepository
    {
        //Task CreateAsync(Login loginModel);
        //Task UpdateAsync(string id, Login loginModel);
        //Task<Login?> GetModelAsync(string id);
        //Task DeleteAsync(string id);
        //Task RestoreAsync(string id);
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbcontext;

        public RoleRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //public async Task CreateAsync(Login loginModel)
        //{
        //    _dbcontext.Logins.Add(loginModel);
        //    await _dbcontext.SaveChangesAsync();
        //}

        //public async Task UpdateAsync(string id, Login loginModel)
        //{
        //    var login = await _dbcontext.Logins.Where(x => x.Id == id).FirstOrDefaultAsync();
        //    _dbcontext.Logins.Update(login);
        //    await _dbcontext.SaveChangesAsync();
        //}

        //public async Task<Login?> GetModelAsync(string id)
        //{
        //    var model = await _dbcontext.Logins.Where(x => x.Id == id).FirstOrDefaultAsync();
        //    if (model == null)
        //    {
        //        return null;
        //    }
        //    return model;
        //}

        //public async Task DeleteAsync(string id)
        //{
        //    var model = await _dbcontext.Logins.Where(x => x.Id == id).FirstOrDefaultAsync();
        //    if (model == null)
        //    {
        //        return;
        //    }
        //    _dbcontext.Logins.Remove(model);
        //    await _dbcontext.SaveChangesAsync();
        //}

        //public async Task RestoreAsync(string id)
        //{
        //    var model = await _dbcontext.Logins
        //        .IgnoreQueryFilters()
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    model.isDeleted = false;

        //    _dbcontext.Update(model);
        //    await _dbcontext.SaveChangesAsync();
        //}


        //public async Task<List<DropdownGenericModel>> GetAllBenefitsForDropdown()
        //{
        //    var model = new List<Benefit>();
        //    model = await _dbContext.Benefit.Where(x => !x.Deleted).AsQueryable().ToListAsync();
        //    var list = model.Select(s => new DropdownGenericModel
        //    {
        //        id = s.Id,
        //        name = s.Name
        //    }).ToList();
        //    return list;
        //}

        //public async Task<string> GetPaginated(CourseDataTablePaginate model)
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

