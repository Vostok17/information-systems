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
                Player = new Cell(4, 8),
                Enemy = new Cell(5, 8),
            };
            Finish = new Cell(3, 2);

            GameStates.Add(state);

            var minimax = new NegaMax<GameState>(
                Maze,
                IsGameOver,
                Expand,
                StaticEvaluation);

            do
            {
                state = minimax.RunClassic(state, 10, 1).State;
                GameStates.Add(state);

                state = MoveEnemy(state);
                GameStates.Add(state);
            }
            while (!IsGameOver(state));

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

        private GameState MoveEnemy(GameState state)
        {
            var pathFinder = new LeeAlgorithm();

            List<Cell> pathToPlayer = pathFinder.Run(Maze, state.Enemy, state.Player).Path;

            if (pathToPlayer.Count > 0)
            {
                state.Enemy = pathToPlayer[0];
            }

            return state;
        }

        private string DetermineWinner(GameState state)
        {
            if (state.Player.Equals(state.Enemy))
            {
                return "Player lost!";
            }

            if (state.Player.Equals(Finish))
            {
                // Remove last enemy's move because it's redunant.
                GameStates.RemoveAt(GameStates.Count - 1);

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

        private int StaticEvaluation(GameState state, bool isPlayer)
        {
            var pathFinder = new LeeAlgorithm();

            int distanceToEnemy = pathFinder.Run(Maze, state.Player, state.Enemy).Path.Count;

            if (distanceToEnemy <= 1)
            {
                return int.MinValue;
            }

            if (!isPlayer)
            {
                return distanceToEnemy;
            }

            int distanceToFinish = pathFinder.Run(Maze, state.Player, Finish).Path.Count;

            if (distanceToFinish <= 1)
            {
                return int.MaxValue - distanceToFinish;
            }

            return distanceToEnemy - distanceToFinish;
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
