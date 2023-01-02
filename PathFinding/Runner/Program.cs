using PathFinding.Algorithms;
using Runner;

int[,] matrix =
{
    { 1, 0, 1, 1, 1 },
    { 1, 0, 1, 0, 1 },
    { 1, 1, 1, 0, 1 },
    { 0, 0, 0, 0, 1 },
    { 1, 1, 1, 0, 1 },
};

Cell source = new Cell(0, 0);
Cell dest = new Cell(4, 4);

Console.WriteLine("Start: {0}", source);
Console.WriteLine("End:   {0}\n", dest);

IPathFinder[] algorithms =
{
    new LeeAlgorithm(),
    new AStar(),
};

foreach (var algo in algorithms)
{
    var maze = new Maze(matrix);
    var (dist, path) = algo.Run(maze, source, dest);

    Console.WriteLine(algo);

    if (dist == -1)
    {
        Console.WriteLine("Shortest Path doesn't exist");
        continue;
    }

    Console.WriteLine("Length of the Shortest Path is " + dist);

    path.Insert(0, source);

    var drawer = new ConsolePathDrawer(maze, path);
    drawer.Draw();

    Console.WriteLine();
}
