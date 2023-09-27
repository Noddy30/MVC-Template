using System;
using Microsoft.EntityFrameworkCore;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Data;

namespace Template.Repositories.ScoreCards
{
	public interface IScoreCardRepository
	{
        Task CreateAsync(ScoreCard model);
        Task UpdateAsync(string id, ScoreCard model);
        Task<ScoreCard?> GetModelAsync(string id);
        Task DeleteAsync(string id);
        Task RestoreAsync(string id);
        Task<List<ScoreCard?>> GetAllScoreCardsAsync();
    }

	public class ScoreCardRepository : IScoreCardRepository
	{
        private readonly AppDbContext _dbcontext;
        public ScoreCardRepository(AppDbContext dbcontext)
		{
            _dbcontext = dbcontext;
        }

        public async Task CreateAsync(ScoreCard model)
        {
            _dbcontext.ScoreCards.Add(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var model = await _dbcontext.ScoreCards
                .Where(x => x.PublicId == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return;
            }

            model.IsDeleted = true;
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<ScoreCard?>> GetAllScoreCardsAsync()
        {
            var model = await _dbcontext.ScoreCards
                .Where(w => w.IsDeleted == false)
                .ToListAsync();
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public async Task<ScoreCard?> GetModelAsync(string id)
        {
            var model = await _dbcontext.ScoreCards
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
            var model = await _dbcontext.ScoreCards
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.PublicId == id);

            model.IsDeleted = false;

            _dbcontext.Update(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, ScoreCard model)
        {
            var course = await _dbcontext.ScoreCards
                .Where(x => x.PublicId == id)
                .FirstOrDefaultAsync();

            _dbcontext.ScoreCards.Update(model);
            await _dbcontext.SaveChangesAsync();
        }
    }
}

