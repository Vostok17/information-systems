using PathFinding.Algorithms;

namespace ArcadeGame
{
    public class Game
    {
        public Game(IMaze maze)
        {
            Maze = maze ?? throw new ArgumentNullException(nameof(maze));
        }

        public List<GameState> GameStates { get; } = new();

        public IMaze Maze { get; init; }

        public Cell Finish { get; private set; }

        public string Run()
        {
            GameState state = RandomizeInitialState();

            // Test state.
            state = new GameState
            {
                Player = new Cell(9, 4),
                Enemy = new Cell(4, 9),
            };
            Finish = new Cell(3, 2);

            GameStates.Add(state);

            var minimax = new MiniMax<GameState>(
                Maze,
                IsGameOver,
                Expand,
                StaticEvaluation);

            do
            {
                (_, state) = minimax.RunAlphaBeta(state, 10, int.MinValue, int.MaxValue, true);
                GameStates.Add(state);

                (_, state) = minimax.RunAlphaBeta(state, 3, int.MinValue, int.MaxValue, false);
                GameStates.Add(state);
            }
            while (!IsGameOver(state));

            GameStates.RemoveAt(GameStates.Count - 1);
            return DetermineWinner(state);
        }

        private GameState RandomizeInitialState()
        {
            Random rnd = new Random();
            var closed = new HashSet<Cell>();

            Finish = RandomizeCell();

            return new GameState
            {
                Player = RandomizeCell(),
                Enemy = RandomizeCell(),
            };

            Cell RandomizeCell()
            {
                Cell cell;
                do
                {
                    int x = rnd.Next(0, Maze.Rows);
                    int y = rnd.Next(0, Maze.Cols);
                    cell = new Cell(x, y);
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

            if (state.Player.Equals(Finish))
            {
                return "Player won!";
            }

            throw new NotImplementedException();
        }

        private bool IsGameOver(GameState state)
        {
            Cell player = state.Player;
            Cell enemy = state.Enemy;

            return player.Equals(enemy) || player.Equals(Finish);
        }

        private int StaticEvaluation(GameState state)
        {
            var pathFinder = new LeeAlgorithm();

            var (distanceToEnemy, pathToEnemy) = pathFinder.Run(Maze, state.Player, state.Enemy);

            if (pathToEnemy?.Count > 1)
            {
                var (distanceToFinish, _) = pathFinder.Run(Maze, state.Player, Finish);

                return distanceToEnemy - distanceToFinish;
            }

            return int.MinValue;
        }

        private IEnumerable<GameState> Expand(GameState state, bool isPlayer)
        {
            Cell current = isPlayer ? state.Player : state.Enemy;

            int[] dx = { -1, 0, 0, 1 };
            int[] dy = { 0, -1, 1, 0 };

            var children = new List<Cell>();

            for (int i = 0; i < 4; i++)
            {
                Cell next = new Cell(current.X + dx[i], current.Y + dy[i]);

                if (IsInsideBounds(next) && Maze[next] == 1)
                {
                    children.Add(next);
                }
            }

            return isPlayer
                ? children.Select(cell => new GameState { Player = cell, Enemy = state.Enemy })
                : children.Select(cell => new GameState { Player = state.Player, Enemy = cell });
        }

        private bool IsInsideBounds(Cell c)
        {
            return c.X >= 0
                && c.X < Maze.Rows
                && c.Y >= 0
                && c.Y < Maze.Cols;
        }
    }
}
