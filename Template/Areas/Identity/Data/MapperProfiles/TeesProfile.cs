using System;
using AutoMapper;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Areas.Identity.Data.Viewmodels.TeeBoxes;

namespace Template.Areas.Identity.Data.MapperProfiles
{
	public class TeesProfile : Profile
	{
		public TeesProfile()
		{
            CreateMap<TeesViewModel, Tees>();
            CreateMap<Tees, TeesViewModel>();
        }
	}
}

