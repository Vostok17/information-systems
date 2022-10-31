using PathFinding.Algorithms;

namespace Game
{
    public class GameState
    {
        public Cell Player { get; set; }

        public Cell Enemy { get; set; }

        public Cell Finish { get; set; }

        public bool IsGameOver() => Player.Equals(Enemy) || Player.Equals(Finish);
    }
}
