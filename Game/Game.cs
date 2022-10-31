using PathFinding.Algorithms;

namespace Game
{
    public class GameInfo
    {


        public List<GameState> GameStates { get; } = new();

        public Maze? Maze { get; init; }

        public string Start()
        {
            GameState state = CreateInitialState();
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

        private GameState CreateInitialState()
        {
            Random rnd = new Random();
            var closed = new List<Cell>();

            return new GameState
            {
                Player = RandomizeCell(),
                Enemy = RandomizeCell(),
                Finish = RandomizeCell(),
            };

            Cell RandomizeCell()
            {
                Cell cell;
                do
                {
                    cell = new Cell(rnd.Next(0, Maze.Rows), rnd.Next(0, Maze.Cols));
                }
                while (closed.Contains(cell) || Maze[cell] == 0);

                closed.Add(cell);
                return cell;
            }
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
