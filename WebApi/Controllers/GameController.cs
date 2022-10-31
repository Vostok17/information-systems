using Game;
using Microsoft.AspNetCore.Mvc;
using PathFinding.Algorithms;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameStateService _gameStateService;

        public GameController(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        [HttpGet]
        [Route("maze")]
        public ActionResult<MazeDto> GetMaze()
        {
            var maze = _gameStateService.GetMaze();

            return Ok(maze);
        }

        [HttpGet]
        [Route("states")]
        public ActionResult<IEnumerable<GameState>> GetStates()
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

            Maze maze = new Maze(matrix);

            var game = new GameInfo()
            {
                Maze = maze,
            };

            game.Start();

            return Ok(game.GameStates);
        }
    }
}
