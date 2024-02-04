using AutoMapper;
using Contracts.DTOs.Stories;
using Domain.Entities;
using Services.DTOs.Episodes;

namespace Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Story, StoryResponseDTO>();
        CreateMap<Episode, EpisodeResponseDTO>()
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Audio.Link));
    }
}