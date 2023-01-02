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

        public (int Eval, TState State) RunClassic(TState state, int depth, int color)
        {
            if (depth == 0 || IsGameOver(state))
            {
                int eval = color * Evaluate(state, color > 0);
                return (eval, state);
            }

            var bestState = new TState();
            int maxEval = int.MinValue;

            foreach (var child in Expand(state, color > 0))
            {
                int eval = -RunClassic(child, depth - 1, -color).Eval;

                if (maxEval < eval)
                {
                    maxEval = eval;
                    bestState = child;
                }
            }

            return (maxEval, bestState);
        }
    }
}
