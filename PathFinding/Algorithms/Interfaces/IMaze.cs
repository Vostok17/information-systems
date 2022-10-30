namespace PathFinding.Algorithms
{
    public interface IMaze
    {
        int Rows { get; }

        int Cols { get; }

        public int this[int x, int y] { get; set; }

        int this[Cell p] { get; set; }

        string ToString();
    }
}
