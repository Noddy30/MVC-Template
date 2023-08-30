using System;
using Template.Data;

namespace Template.Repositories.Players
{
    public interface IPlayerRepository
    {
        //Task CreateAsync(Login loginModel);
        //Task UpdateAsync(string id, Login loginModel);
        //Task<Login?> GetModelAsync(string id);
        //Task DeleteAsync(string id);
        //Task RestoreAsync(string id);
    }
    public class PlayerRepository : IPlayerRepository
	{

        private readonly AppDbContext _dbcontext;

        public PlayerRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
    }
}

