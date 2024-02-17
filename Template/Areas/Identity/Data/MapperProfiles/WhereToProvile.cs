using AutoMapper;
using Template.Areas.Identity.Data.Models.FrontEnd;
using Template.Areas.Identity.Data.Viewmodels.FrontEnd;

namespace Template.Areas.Identity.Data.MapperProfiles
{
	public class WhereToProvile : Profile
    {
		public WhereToProvile()
		{
            CreateMap<WhereToViewModel, WhereTo>();
            CreateMap<WhereTo, WhereToViewModel>();
        }
	}
}

