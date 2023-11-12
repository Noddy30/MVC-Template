using System;
using AutoMapper;
using Template.Areas.Identity.Data.Models.Courses;
using Template.Areas.Identity.Data.Viewmodels.Courses;

namespace Template.Areas.Identity.Data.MapperProfiles
{
	public class GolfCourseProfile : Profile
	{
		public GolfCourseProfile()
        { 
            CreateMap<GolfCourseViewModel, GolfCourse>()
                .ForMember(dest => dest.Scorecard, opt => opt.MapFrom(src => src.Scorecard))
                .ForMember(dest => dest.TeeBoxes, opt => opt.MapFrom(src => src.TeeBoxes));
            CreateMap<GolfCourse, GolfCourseViewModel>()
                .ForMember(dest => dest.Scorecard, opt => opt.MapFrom(src => src.Scorecard))
             .ForMember(dest => dest.TeeBoxes, opt => opt.MapFrom(src => src.TeeBoxes));
		}
	}
}

