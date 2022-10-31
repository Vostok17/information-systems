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
    int dist = algo.Run(new Maze(matrix), source, dest);

    Console.WriteLine(algo);

    if (dist != -1)
    {
        Console.WriteLine("Length of the Shortest Path is " + dist);

        var drawer = new ConsolePathDrawer(algo.Maze!, algo.Path!);
        drawer.Draw();
    }
    else
    {
        Console.WriteLine("Shortest Path doesn't exist");
    }

    Console.WriteLine();
}
