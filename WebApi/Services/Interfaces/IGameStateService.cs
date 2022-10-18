using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface IGameStateService
    {
        MazeDto GetMaze();
    }
}
