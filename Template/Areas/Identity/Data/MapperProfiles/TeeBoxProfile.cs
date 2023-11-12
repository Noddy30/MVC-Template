using System;
using AutoMapper;
using Template.Areas.Identity.Data.Models.TeeBoxes;
using Template.Areas.Identity.Data.Viewmodels.TeeBoxes;

namespace Template.Areas.Identity.Data.MapperProfiles
{
	public class TeeBoxProfile : Profile
	{
		public TeeBoxProfile()
		{
            CreateMap<TeeBoxViewModel, TeeBox>();
            CreateMap<TeeBox, TeeBoxViewModel>();
        }
	}
}

