namespace WebApi.Models
{
    public class MazeDto
    {
        public int Height { get; init; }

        public int Width { get; init; }

        public string Maze { get; init; } = string.Empty;
    }
}
