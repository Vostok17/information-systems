using PathFinding.Algorithms;

namespace Runner
{
    public class ConsolePathDrawer
    {
        private readonly IMaze _maze;
        private readonly IEnumerable<Cell> _path;

        public ConsolePathDrawer(IMaze maze, IEnumerable<Cell> path)
        {
            _maze = maze;
            _path = path;
        }

        public void Draw()
        {
            for (int i = 0; i < _maze.Rows; i++)
            {
                for (int j = 0; j < _maze.Cols; j++)
                {
                    char c = _maze[i, j] switch
                    {
                        1 => '+',
                        0 => 'W',
                        _ => throw new NotImplementedException()
                    };

                    if (_path.Contains(new Cell(i, j)))
                    {
                        PrintColored(c, ConsoleColor.Green);
                    }
                    else
                    {
                        Print(c);
                    }
                }

                Console.WriteLine();
            }
        }

        private static void PrintColored(char c, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Print(c);
            Console.ResetColor();
        }

        private static void Print(char c)
        {
            Console.Write(c + " ");
        }
    }
}
