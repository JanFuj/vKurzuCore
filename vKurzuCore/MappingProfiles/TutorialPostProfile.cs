using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Models;
using vKurzuCore.ViewModels.Dto;

namespace vKurzuCore.MappingProfiles
{
    public class TutorialPostProfile : Profile
    {
        public TutorialPostProfile()
        {
            CreateMap<TutorialPost, TutorialPostDto>();
             
            CreateMap<TutorialPostDto, TutorialPost>()
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

        }
    }
}
