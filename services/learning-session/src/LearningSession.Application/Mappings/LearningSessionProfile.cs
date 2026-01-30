using AutoMapper;
using LearningSession.Application.DTOs;
using LearningSession.Domain.Entities;

namespace LearningSession.Application.Mappings
{
    public class LearningSessionProfile : Profile
    {
        public LearningSessionProfile()
        {
            CreateMap<LearningSessionEntity, LearningSessionDto>()
                .ForMember(dest => dest.LearningActivityIds, opt => opt.MapFrom(src => src.LearningActivityIds));
        }
    }
}

