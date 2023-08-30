using System;
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

    }

}

