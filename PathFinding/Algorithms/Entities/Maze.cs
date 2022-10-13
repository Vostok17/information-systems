using System.Text;

namespace Algorithms
{
    public class Maze : IMaze
    {
        private readonly int[,] _matrix;

        public Maze(int[,] matrix)
        {
            Rows = matrix.GetLength(dimension: 0);
            Cols = matrix.GetLength(dimension: 1);

            _matrix = new int[Rows, Cols];
            Array.Copy(matrix, _matrix, matrix.Length);
        }

        public int Rows { get; }

        public int Cols { get; }

        public int this[int x, int y]
        {
            get => _matrix[x, y];
            set => _matrix[x, y] = value;
        }

        public int this[Cell p]
        {
            get => _matrix[p.X, p.Y];
            set => _matrix[p.X, p.Y] = value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    sb.Append(_matrix[i, j]).Append(' ');
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
