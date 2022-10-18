using Algorithms;
using AutoMapper;
using WebApi.Models;

namespace WebApi.MappingProfiles
{
    public class MazeProfile : Profile
    {
        public MazeProfile()
        {
            CreateMap<Maze, MazeDto>()
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Rows))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Cols))
                .ForMember(dest => dest.Maze, opt => opt.MapFrom(src => src.ToString()));
        }
    }
}
