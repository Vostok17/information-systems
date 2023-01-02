using PathFinding.Algorithms;
using static System.Math;

namespace ArcadeGame
{
    public class MiniMax<TState>
        where TState : new()
    {
        public MiniMax(
            IMaze maze,
            Predicate<TState> isGameOver,
            Func<TState, bool, IEnumerable<TState>> expand,
            Func<TState, bool, int> evaluate)
        {
            Maze = maze;
            IsGameOver = isGameOver;
            Expand = expand;
            Evaluate = evaluate;
        }

        public IMaze Maze { get; }

        public Predicate<TState> IsGameOver { get; }

        public Func<TState, bool, IEnumerable<TState>> Expand { get; }

        public Func<TState, bool, int> Evaluate { get; }

        public (int Eval, TState State) RunClassic(TState state, int depth, bool maximizingPlayer)
        {
            if (depth == 0 || IsGameOver(state))
            {
                int eval = Evaluate(state, maximizingPlayer);
                return (eval, state);
            }

            var bestState = new TState();

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;

                foreach (var child in Expand(state, maximizingPlayer))
                {
                    int eval = RunClassic(child, depth - 1, false).Eval;

                    if (maxEval < eval)
                    {
                        maxEval = eval;
                        bestState = child;
                    }
                }

                return (maxEval, bestState);
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (var child in Expand(state, maximizingPlayer))
                {
                    int eval = RunClassic(child, depth - 1, true).Eval;

                    if (minEval > eval)
                    {
                        minEval = eval;
                        bestState = child;
                    }
                }

                return (minEval, bestState);
            }
        }

        public (int Eval, TState State) RunAlphaBeta(
            TState state,
            int depth,
            int alpha,
            int beta,
            bool maximizingPlayer)
        {
            if (depth == 0 || IsGameOver(state))
            {
                int eval = Evaluate(state, maximizingPlayer);
                return (eval, state);
            }

            var bestState = new TState();

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;

                foreach (var child in Expand(state, maximizingPlayer))
                {
                    int eval = RunAlphaBeta(child, depth - 1, alpha, beta, false).Eval;

                    if (maxEval < eval)
                    {
                        maxEval = eval;
                        bestState = child;
                    }

                    alpha = Max(alpha, eval);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return (maxEval, bestState);
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (var child in Expand(state, maximizingPlayer))
                {
                    int eval= RunAlphaBeta(child, depth - 1, alpha, beta, true).Eval;

                    if (minEval > eval)
                    {
                        minEval = eval;
                        bestState = child;
                    }

                    beta = Min(beta, eval);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return (minEval, bestState);
            }
        }
    }
}
