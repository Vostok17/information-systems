namespace WebApi.Models
{
    public class MazeDto
    {
        public int Rows { get; init; }

        public int Cols { get; init; }

        public string Maze { get; init; } = string.Empty;
    }
}
