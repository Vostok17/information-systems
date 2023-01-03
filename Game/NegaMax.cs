using PathFinding.Algorithms;

namespace ArcadeGame
{
    public class NegaMax<TState>
        where TState : new()
    {
        public NegaMax(
            IMaze maze,
            Predicate<TState> isGameOver,
            Func<TState, bool, IEnumerable<TState>> expand,
            Func<TState, bool, float> evaluate)
        {
            Maze = maze;
            IsGameOver = isGameOver;
            Expand = expand;
            Evaluate = evaluate;
        }

        public IMaze Maze { get; }

        public Predicate<TState> IsGameOver { get; }

        public Func<TState, bool, IEnumerable<TState>> Expand { get; }

        public Func<TState, bool, float> Evaluate { get; }

        public (float Eval, TState State) RunClassic(TState state, int depth, int color)
        {
            if (depth == 0 || IsGameOver(state))
            {
                float eval = color * Evaluate(state, color > 0);
                return (eval, state);
            }

            var bestState = new TState();
            float maxEval = float.NegativeInfinity;

            foreach (var child in Expand(state, color > 0))
            {
                float eval = -RunClassic(child, depth - 1, -color).Eval;

                if (maxEval < eval)
                {
                    maxEval = eval;
                    bestState = child;
                }
            }

            return (maxEval, bestState);
        }

        public (float Eval, TState State) RunAlphaBeta(TState state, int depth, float alpha, float beta, int color)
        {
            if (depth == 0 || IsGameOver(state))
            {
                float eval = color * Evaluate(state, color > 0);
                return (eval, state);
            }

            var bestState = new TState();
            float maxEval = float.NegativeInfinity;

            foreach (var child in Expand(state, color > 0))
            {
                float eval = -RunAlphaBeta(child, depth - 1, -beta, -alpha, -color).Eval;

                if (maxEval < eval)
                {
                    maxEval = eval;
                    bestState = child;
                }

                if (alpha >= beta)
                {
                    break;
                }
            }

            return (maxEval, bestState);
        }
    }
}
