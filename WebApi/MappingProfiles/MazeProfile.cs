﻿using AutoMapper;
using PathFinding.Algorithms;
using WebApi.Models;

namespace WebApi.MappingProfiles
{
    public class MazeProfile : Profile
    {
        public MazeProfile()
        {
            CreateMap<Maze, MazeDto>()
                .ForMember(dest => dest.Maze, opt => opt.MapFrom(src => src.ToString()));
        }
    }
}
