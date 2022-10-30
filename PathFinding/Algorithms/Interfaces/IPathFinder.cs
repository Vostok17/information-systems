namespace PathFinding.Algorithms
{
    public interface IPathFinder
    {
        int? Distance { get; }

        IEnumerable<Cell>? Path { get; }

        IMaze? Maze { get; }

        int Run(int[,] matrix, Cell src, Cell dest);
    }
}
