using System.Text;

namespace PathFinding.Algorithms
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

        public void Print(Cell player, Cell enemy, Cell Finish)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (player.X == i && player.Y == j)
                    {
                        PrintColored(ConsoleColor.Green, "p");
                        Console.Write(" ");
                        continue;
                    }

                    if (enemy.X == i && enemy.Y == j)
                    {
                        PrintColored(ConsoleColor.Blue, "e");
                        Console.Write(" ");
                        continue;
                    }

                    if (Finish.X == i && Finish.Y == j)
                    {
                        PrintColored(ConsoleColor.Black, "e");
                        Console.Write(" ");
                        continue;
                    }

                    if (_matrix[i, j] == 1)
                    {
                        PrintColored(ConsoleColor.White, "1");
                        Console.Write(" ");
                    }
                    else
                    {
                        PrintColored(ConsoleColor.Red, "0");
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine(sb.ToString());
        }

        private void PrintColored(ConsoleColor color, string value)
        {
            Console.BackgroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }
    }
}
