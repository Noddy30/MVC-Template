using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Areas.Identity.Data.PaginationDataTables;
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

        //public async Task<string> GetPaginated(ScoreCardPagination model)
        //{
        //    var data = _dbcontext.ScoreCards.AsQueryable();

        //    // Apply search filter if it's provided
        //    if (!string.IsNullOrEmpty(model.CourseId))
        //    {
        //        data = data.Where(x => x.GofToString().Contains(model.CourseId.ToUpper()));
        //    }

        //    // Determine the sort direction
        //    bool isAscending = model.SortDir_0 == "asc";

        //    // Sort the data based on the column index
        //    //data = model.SortCol_0 switch
        //    //{
        //    //    0 => (isAscending) ? data.OrderBy(x => x.Name) : data.OrderByDescending(x => x.Name),
        //    //    1 => (isAscending) ? data.OrderBy(x => x.City) : data.OrderByDescending(x => x.City),
        //    //    2 => (isAscending) ? data.OrderBy(x => x.Country) : data.OrderByDescending(x => x.Country),
        //    //    3 => (isAscending) ? data.OrderBy(x => x.Holes) : data.OrderByDescending(x => x.Holes),
        //    //    _ => data // Handle default case if needed
        //    //};

        //    var totalCount = await data.CountAsync();

        //    var displayedMembers = await data
        //        .Skip(model.DisplayStart)
        //        .Take(model.DisplayLength)
        //        .Select(a => new
        //        {
        //            Id = a.Id,
        //            PublicId = a.PublicId,
        //            Hole = a.Hole,
        //            Par = a.Par,
        //            Handicap = a.Handicap
        //        })
        //        .ToListAsync();

        //    var result = new
        //    {
        //        Echo = model.Echo,
        //        TotalRecords = totalCount,
        //        TotalDisplayRecords = totalCount,
        //        data = displayedMembers
        //    };

        //    return JsonConvert.SerializeObject(result);
        //}
    }
}

