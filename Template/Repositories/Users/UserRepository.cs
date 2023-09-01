using System;
using Template.Areas.Identity.Data;
using Template.Data;

namespace Template.Repositories.Users
{
	public interface IUserRepository
	{
        //Task CreateAsync(ApplicationUser loginModel);
        //Task UpdateAsync(string id, ApplicationUser loginModel);
        //Task<ApplicationUser?> GetModelAsync(string id);
        //Task DeleteAsync(string id);
        //Task RestoreAsync(string id);
        //Task
    }
    public class UserRepository : IUserRepository
	{
        private readonly AppDbContext _dbcontext;

        public UserRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
    }
}

