using System;

namespace ReversiBase
{
    public class Solver
    {
        private Func<Game, int> _heuristic;
        public int MaxPly { private set; get; }
        public TileColor Color { private set; get; }

        public Solver(TileColor color, Func<Game, TileColor, int> heuristic, int ply)
        {
            Color = color;
            SetHeuristic(heuristic);
            MaxPly = ply;
        }

        public Play ChoosePlay(Game game, bool prune = true)
        {
            game = new Game(game);
            return AlphaBeta(game, MaxPly, prune).Item2;
        }

        private Tuple<int, Play> AlphaBeta(Game game, int ply = 5, bool prune = true, bool max = true,
            int alpha = int.MinValue, int beta = int.MaxValue)
        {
            var possiblePlays = ply == 0 ? null : game.PossiblePlays();

            if (ply == 0 || game.GameOver() || possiblePlays.Count == 0)
                return new Tuple<int, Play>(_heuristic(game), null);

            var optimizer = max ? (Func<int, int, int>)Math.Max : Math.Min;
            var bestScore = max ? int.MinValue : int.MaxValue;
            Play bestPlay = null;

            foreach (var pair in game.PossiblePlays())
            {
                var childScore = AlphaBeta(game.ForkGame(pair.Value), ply - 1, prune, !max, alpha, beta).Item1;

                if (bestScore != optimizer(bestScore, childScore))
                {
                    bestPlay = pair.Value;
                    bestScore = childScore;
                }

                if (max)
                    alpha = optimizer(alpha, bestScore);
                else
                    beta = optimizer(beta, bestScore);
                
                if (prune && beta <= alpha)
                    break;
            }

            return new Tuple<int, Play>(bestScore, bestPlay);
        }

        public void SetHeuristic(Func<Game, TileColor, int> heuristic)
        {
            _heuristic = game => heuristic(game, Color);
        }
    }
}