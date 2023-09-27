using System;
using Microsoft.EntityFrameworkCore;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Data;

namespace Template.Repositories.TeeBoxes
{
	public interface ITeeBoxRepository
	{
        Task CreateAsync(TeeBox model);
        Task UpdateAsync(string id, TeeBox model);
        Task<TeeBox?> GetModelAsync(string id);
        Task DeleteAsync(string id);
        Task RestoreAsync(string id);
        Task<List<TeeBox?>> GetAllTeeBoxesAsync();
    }
	public class TeeBoxRepository : ITeeBoxRepository
    {
        private readonly AppDbContext _dbcontext;

        public TeeBoxRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task CreateAsync(TeeBox model)
        {
            _dbcontext.TeeBoxes.Add(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var model = await _dbcontext.TeeBoxes
                .Where(x => x.PublicId == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return;
            }
            //_dbcontext.ApplicationUsers.Remove(model);
            //await _dbcontext.SaveChangesAsync();

            model.IsDeleted = true;
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<TeeBox?> GetModelAsync(string id)
        {
            var model = await _dbcontext.TeeBoxes
                .Where(x => x.PublicId == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task RestoreAsync(string id)
        {
            var model = await _dbcontext.TeeBoxes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.PublicId == id);

            model.IsDeleted = false;

            _dbcontext.Update(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, TeeBox model)
        {
            var course = await _dbcontext.TeeBoxes
                .Where(x => x.PublicId == id)
                .FirstOrDefaultAsync();

            _dbcontext.TeeBoxes.Update(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<TeeBox?>> GetAllTeeBoxesAsync()
        {
            var model = await _dbcontext.TeeBoxes
                .Where(w => w.IsDeleted == false)
                .ToListAsync();
            if (model == null)
            {
                return null;
            }
            return model;
        }
    }
}

