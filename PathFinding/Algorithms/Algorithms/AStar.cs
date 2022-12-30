using static System.Math;

namespace PathFinding.Algorithms
{
    public class AStar : BasePathFinder, IPathFinder
    {
        private readonly Func<Cell, Cell, int> _heuristics;

        public AStar(Func<Cell, Cell, int>? heuristics = null)
        {
            _heuristics = heuristics ?? ManhattanHeuristics;
        }

        public override (int Distance, IList<Cell>? Path) Run(IMaze maze, Cell src, Cell dest)
        {
            Maze = maze;

            if (Maze[src] == 0 || Maze[dest] == 0)
            {
                return (-1, null);
            }

            var open = new PriorityQueue<Node, int>();
            var closed = new HashSet<Cell>();

            open.Enqueue(new Node(src, 0), int.MaxValue);

            int[] dx = { -1, -1, -1, 0, 1, 0, 1, 1 };
            int[] dy = { -1, 1, 0, -1, -1, 1, 0, 1 };

            do
            {
                Node curr = open.Dequeue();
                Cell c = curr.Cell!;

                closed.Add(c);

                if (c.Equals(dest))
                {
                    var path = BacktracePath(src, curr);
                    return (curr.Distance, path);
                }

                for (int i = 0; i < 8; i++)
                {
                    Cell next = new Cell(c.X + dx[i], c.Y + dy[i]);

                    if (IsInsideBounds(next) && Maze[next] == 1 && !closed.Contains(next))
                    {
                        open.Enqueue(
                            new Node(next, curr.Distance + GetDistanceToNextCell(dx[i], dy[i]), curr),
                            _heuristics.Invoke(next, dest));
                    }
                }
            }
            while (open.Count > 0);

            return (-1, null);
        }

        public override string ToString() => "A*";

        private int ManhattanHeuristics(Cell c1, Cell c2)
        {
            return (Abs(c1.X - c2.X) + Abs(c1.Y - c2.Y)) * 10;
        }
    }
}
