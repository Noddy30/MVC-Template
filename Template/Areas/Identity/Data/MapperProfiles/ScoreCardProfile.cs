using System;
using AutoMapper;
using Template.Areas.Identity.Data.Models.ScoreCards;
using Template.Areas.Identity.Data.Viewmodels.ScoreCards;

namespace Template.Areas.Identity.Data.MapperProfiles
{
	public class ScoreCardProfile : Profile
	{
		public ScoreCardProfile()
		{
            CreateMap<ScoreCardViewModel, ScoreCard>()
                .ForMember(dest => dest.Tees, opt => opt.MapFrom(src => src.Tees.Values.ToList()));
            CreateMap<ScoreCard, ScoreCardViewModel>()
                .ForMember(dest => dest.Tees, opt => opt.MapFrom(src => src.Tees.ToDictionary(t => t.Color, t => t)));
        }
	}
}

