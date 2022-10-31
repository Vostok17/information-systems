using PathFinding.Algorithms;

namespace Game
{
    public class GameInfo
    {
        public List<GameState> GameStates { get; } = new();

        public Maze? Maze { get; init; }

        public string Start()
        {
            var state = new GameState
            {
                Player = RandomizeStartLocation(),
                Enemy = RandomizeStartLocation(),
                Finish = RandomizeStartLocation(),
            };
            GameStates.Add(state);

            MiniMax miniMax = new MiniMax(Maze);

            while (!state.IsGameOver())
            {
                miniMax.RunClassic(state, 1, true);
                state = miniMax.BestMove;
                GameStates.Add(state);
            }

            return DetermineWinner(GameStates.Last());
        }

        private Cell RandomizeStartLocation()
        {
            Random rnd = new Random();

            Cell coords;
            do
            {
                coords = new Cell(rnd.Next(0, Maze.Rows), rnd.Next(0, Maze.Cols));
                Console.WriteLine(coords + " === " + Maze[coords]);
            }
            while (Maze[coords] == 0);

            return coords;
        }

        private string DetermineWinner(GameState state)
        {
            if (state.Player.Equals(state.Enemy))
            {
                return "Player lost!";
            }

            if (state.Player.Equals(state.Finish))
            {
                return "Player won!";
            }

            throw new NotImplementedException();
        }
    }
}
