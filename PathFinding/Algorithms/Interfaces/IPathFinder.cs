namespace PathFinding.Algorithms
{
    public interface IPathFinder
    {
        IMaze? Maze { get; }

        (int Distance, List<Cell> Path) Run(IMaze maze, Cell src, Cell dest);
    }
}
