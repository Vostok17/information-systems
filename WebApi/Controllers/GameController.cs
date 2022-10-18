using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<MazeDto> GetMaze()
        {
            var maze = _gameStateService.GetMaze();

            return Ok(maze);
        }
    }
}
