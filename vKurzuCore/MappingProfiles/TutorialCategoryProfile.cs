using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.MappingProfiles
{
    public class TutorialCategoryProfile : Profile
    {
        public TutorialCategoryProfile()
        {
            CreateMap<TutorialCategory, TutorialCategoryDto>();
            CreateMap<TutorialCategoryDto, TutorialCategory>();

        }
    }
}
