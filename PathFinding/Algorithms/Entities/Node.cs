namespace Algorithms
{
    public class Node
    {
        public Node()
        {
        }

        public Node(Cell cell, int distance)
            : this(cell, distance, null)
        {
        }

        public Node(Cell cell, int distance, Node? previous)
        {
            Cell = cell;
            Distance = distance;
            Previous = previous;
        }

        public Cell Cell { get; }

        public int Distance { get; }

        public Node? Previous { get; }
    }
}
