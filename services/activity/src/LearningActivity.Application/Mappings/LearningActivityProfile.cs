using AutoMapper;
using LearningActivity.Domain.Entities;
using LearningActivity.Application.DTOs;

namespace LearningActivity.Application.Mappings
{
    public class LearningActivityProfile : Profile
    {
        public LearningActivityProfile()
        {
            CreateMap<LearningActivityEntity, LearningActivityDto>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.MaterialIds, opt => opt.MapFrom(src => src.MaterialIds));
        }
    }
}

