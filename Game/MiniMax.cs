using PathFinding.Algorithms;

namespace Game
{
    public class MiniMax
    {
        public MiniMax(Maze maze)
        {
            Maze = maze;
        }

        public Maze Maze { get; }

        public GameState? BestMove { get; private set; }

        public int RunClassic(GameState state, int depth, bool maximizingPlayer)
        {
            if (depth == 0 || state.IsGameOver())
            {
                return StaticEvaluation(state);
            }

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;
                foreach (var child in Expand(state))
                {
                    int eval = RunClassic(child, depth - 1, false);

                    if (maxEval < eval)
                    {
                        maxEval = eval;
                        BestMove = child;
                    }
                }

                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (var child in Expand(state))
                {
                    int eval = RunClassic(child, depth - 1, true);

                    if (minEval > eval)
                    {
                        minEval = eval;
                        BestMove = child;
                    }
                }

                return minEval;
            }
        }

        private int StaticEvaluation(GameState state)
        {
            var pathFinder = new AStar();

            return -pathFinder.Run(Maze, state.Player, state.Finish);
        }

        private IEnumerable<GameState> Expand(GameState state)
        {
            int[] dx = { -1, 0, 0, 1 };
            int[] dy = { 0, -1, 1, 0 };

            var children = new List<GameState>();

            Cell player = state.Player;

            for (int i = 0; i < 4; i++)
            {
                Cell nextMove = new Cell(player.X + dx[i], player.Y + dy[i]);

                if (IsInsideBounds(nextMove) && Maze[nextMove] == 1)
                {
                    children.Add(new GameState()
                    {
                        Player = nextMove,
                        Enemy = state.Enemy,
                        Finish = state.Finish,
                    });
                }
            }

            return children;
        }

        private bool IsInsideBounds(Cell c)
        {
            return c.X >= 0 && c.X < Maze!.Rows && c.Y >= 0 && c.Y < Maze.Cols;
        }
    }
}
