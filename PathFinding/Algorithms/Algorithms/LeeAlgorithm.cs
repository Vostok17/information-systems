namespace PathFinding.Algorithms
{
    public class LeeAlgorithm : BasePathFinder, IPathFinder
    {
        public override (int Distance, IList<Cell>? Path) Run(IMaze maze, Cell src, Cell dest)
        {
            Maze = maze;

            if (Maze[src] == 0 || Maze[dest] == 0)
            {
                return (-1, null);
            }

            bool[,] visited = new bool[Maze.Rows, Maze.Cols];
            visited[src.X, src.Y] = true;

            var q = new Queue<Node>();
            q.Enqueue(new Node(src, 0));

            int[] dx = { -1, 0, 0, 1 };
            int[] dy = { 0, -1, 1, 0 };

            while (q.Count > 0)
            {
                Node curr = q.Dequeue();
                Cell c = curr.Cell!;

                if (c.Equals(dest))
                {
                    var path = BacktracePath(src, curr);
                    return (curr.Distance, path);
                }

                for (int i = 0; i < 4; i++)
                {
                    Cell next = new Cell(c.X + dx[i], c.Y + dy[i]);

                    if (IsInsideBounds(next) && Maze[next] == 1 && !visited[next.X, next.Y])
                    {
                        visited[next.X, next.Y] = true;
                        q.Enqueue(new Node(next, curr.Distance + 10, curr));
                    }
                }
            }

            return (-1, null);
        }

        public override string ToString() => "Lee (Wave) Algorithm";
    }
}
