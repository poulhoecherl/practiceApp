using AutoMapper;
using Practice.Data.Models;
using Practice.Services.DTOs;

namespace Practice.Services.Profiles
{
    public class PracticeProfile : Profile
    {
        public PracticeProfile()
        {
            CreateMap<Session, SessionResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ReverseMap();

            CreateMap<Song, SongResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
