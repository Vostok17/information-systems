using AutoMapper;
using PathFinding.Algorithms;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class GameStateService : IGameStateService
    {
        private readonly IMapper _mapper;

        public GameStateService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public MazeDto GetMaze()
        {
            int[,] matrix =
            {
                { 1, 0, 1, 1, 0, 0, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 0, 0, 1 },
                { 0, 1, 0, 0, 1, 0, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1, 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1, 0, 1, 1, 1, 1 },
                { 1, 1, 0, 1, 1, 1, 1, 0, 1, 0 },
                { 1, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 1, 0, 0, 1, 0, 0, 1, 1, 0 },
                { 0, 1, 1, 1, 1, 0, 0, 0, 1, 1 },
            };

            var maze = new Maze(matrix);

            return _mapper.Map<MazeDto>(maze);
        }
    }
}
