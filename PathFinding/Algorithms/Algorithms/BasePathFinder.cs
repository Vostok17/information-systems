using static System.Math;

namespace PathFinding.Algorithms
{
    public abstract class BasePathFinder
    {
        public IMaze? Maze { get; protected set; }

        public int DistanceToNextCell { get; init; } = 10;

        public abstract (int Distance, List<Cell> Path) Run(IMaze maze, Cell src, Cell dest);

        protected virtual List<Cell> BacktracePath(Cell src, Node res)
        {
            Node curr = res;
            var path = new List<Cell>();

            while (!src.Equals(curr.Cell))
            {
                path.Add(curr.Cell);
                curr = curr.Previous;
            }

            path.Reverse();
            return path;
        }

        protected bool IsInsideBounds(Cell c)
        {
            return c.X >= 0 && c.X < Maze!.Rows && c.Y >= 0 && c.Y < Maze.Cols;
        }

        protected int GetDistanceToNextCell(int dx, int dy)
        {
            return (dx == 0 || dy == 0) ? DistanceToNextCell : GetDiagonalDistanceToNextCell();
        }

        private int GetDiagonalDistanceToNextCell()
        {
            return (int)Sqrt(Pow(DistanceToNextCell, 2) * 2);
        }
    }
}
