using System;
using System.Collections.Generic;

namespace ReversiBase
{
    public class Solver
    {
        private Func<Game, int> _heuristic;
        private int MaxPly { get; }
        private TileColor Color { get; }

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

            foreach (var (_, play) in game.PossiblePlays())
            {
                var childScore = AlphaBeta(game.ForkGame(play), ply - 1, prune, !max, alpha, beta).Item1;

                if (bestScore != optimizer(bestScore, childScore))
                {
                    bestPlay = play;
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

        private void SetHeuristic(Func<Game, TileColor, int> heuristic)
        {
            _heuristic = game => heuristic(game, Color);
        }

        #region Heuristics

        public static int TileCountHeuristic(Game game, TileColor color)
        {
            var black = game.Board.GetColorCount(TileColor.Black);
            var white = game.Board.GetColorCount(TileColor.White);

            if (black + white == 0)
                return 0;

            return color switch
            {
                TileColor.Black => 100 * (black - white) / (black + white),
                TileColor.White => 100 * (white - black) / (white + black),
                _ => 0
            };
        }

        public static int ActualMobilityHeuristic(Game game, TileColor color)
        {
            var currentPlayer = game.IsPlayerBlack ? TileColor.Black : TileColor.White;

            int maxMobility, minMobility;

            if (currentPlayer == color)
            {
                maxMobility = game.PossiblePlays().Count;
                minMobility = game.PossiblePlays(true).Count;
            }
            else
            {
                maxMobility = game.PossiblePlays(true).Count;
                minMobility = game.PossiblePlays().Count;
            }

            if (maxMobility + minMobility > 0)
                return 100 * (maxMobility - minMobility) / (maxMobility + minMobility);
            return 0;
        }

        public static int CornersHeuristic(Game game, TileColor color)
        {
            var currentPlayer = game.IsPlayerBlack ? TileColor.Black : TileColor.White;
            var corners = new List<Tuple<int, int>>
            {
                Tuple.Create(0, 0),
                Tuple.Create((int)Board.Size - 1, 0),
                Tuple.Create(0, (int)Board.Size - 1),
                Tuple.Create((int)Board.Size - 1, (int)Board.Size - 1)
            };

            var maxScore = 0;
            var minScore = 0;

            foreach (var (item1, item2) in corners)
            {
                var t = game.Board[item1, item2];
                if (t != null)
                {
                    if (t.Color == color)
                        ++maxScore;
                    else if (t.Color != TileColor.Blank)
                        ++minScore;
                }
            }

            if (maxScore + minScore > 0)
                return 100 * (maxScore - minScore) / (maxScore + minScore);
            return 0;
        }

        public static int WeightedHeuristic(Game game, TileColor color)
        {
            var weights = new int[(int)Board.Size, (int)Board.Size];
            var score = 0;

            #region Weights

            weights[0, 0] = 4;
            weights[0, 1] = -3;
            weights[0, 2] = 2;
            weights[0, 3] = 2;

            weights[1, 0] = -3;
            weights[1, 1] = -4;
            weights[1, 2] = -1;
            weights[1, 3] = -1;

            weights[2, 0] = 2;
            weights[2, 1] = -1;
            weights[2, 2] = 1;
            weights[2, 3] = 0;

            weights[3, 0] = 2;
            weights[3, 1] = -1;
            weights[3, 2] = 0;
            weights[3, 3] = 1;

            #endregion

            for (var i = 0; i < Board.Size; i++)
            {
                for (var j = 0; j < Board.Size; j++)
                {
                    var im = i < 4 ? i : 3 - i % 4;
                    var jm = j < 4 ? j : 3 - j % 4;

                    var t = game.Board[i, j];
                    if (t != null)
                    {
                        if (t.Color == color)
                            score += weights[im, jm];
                        else if (t.Color != TileColor.Blank)
                            score -= weights[im, jm];
                    }
                }
            }
            return score;
        }

        #endregion
    }
}