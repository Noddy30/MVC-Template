using AutoMapper;
using Template.Areas.Identity.Data;
using Template.Areas.Identity.Data.Models.FrontEnd;
using Template.Areas.Identity.Data.Viewmodels.FrontEnd;
using Template.Data;

namespace Template.Repositories.WhereTo
{
    public interface IWhereToRepository
    {
        Task CreateAsync(WhereToViewModel model);
        
    }
    public class WhereToRepository : IWhereToRepository
    {
        private readonly AppDbContext _dbcontext;
        private readonly IMapper _mapper;

        public WhereToRepository(AppDbContext dbcontext, IMapper mapper)
		{
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task CreateAsync(WhereToViewModel viewModel)
        {
            var model = _mapper.Map<Areas.Identity.Data.Models.FrontEnd.WhereTo> (viewModel);

            _dbcontext.WhereTos.Add(model);
            await _dbcontext.SaveChangesAsync();
        }
    }
}

