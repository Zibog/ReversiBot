using System;
using System.Collections.Generic;

namespace ReversiBase
{
    public class Game
    {
        public bool IsPlayerBlack { set; get; }
        private Board Board { set; get; }
        private bool _deadlock = false;
        public TileColor? Winner
        {
            get
            {
                if (GameOver())
                {
                    var blackCount = Board.GetColorCount(TileColor.Black);
                    var whiteCount = Board.GetColorCount(TileColor.White);
                    if (blackCount > whiteCount)
                        return TileColor.Black;
                    if (whiteCount > blackCount)
                        return TileColor.White;
                    return null;
                }
                if (_deadlock)
                {
                    return TileColor.Blank;
                }
                return null;
            }
        }

        public Game(uint size = Board.Size)
        {
            Board = new Board(size);
            IsPlayerBlack = true;
            int x, y;
            x = y = ((int)size - 1) / 2;
            
            Place(x, y++);
            Place(x++, y);
            Place(x, y--);
            Place(x, y);
        }

        public Game(Game prev)
        {
            Board = new Board(prev.Board);
            IsPlayerBlack = prev.IsPlayerBlack;
            _deadlock = prev._deadlock;
        }
        
        private Tile Place(int x, int y)
        {
            var placement = Board.Place(x, y, IsPlayerBlack ? TileColor.Black : TileColor.White);
            if (placement != null)
                NextTurn();
            return placement;
        }

        private Tile Place(Tuple<int, int> coords)
        {
            return Place(coords.Item1, coords.Item2);
        }

        private void NextTurn()
        {
            IsPlayerBlack = !IsPlayerBlack;
        }
        
        public TileColor ColorAt(int x, int y)
        {
            return Board[x, y] == null ? TileColor.Blank : Board[x, y].Color;
        }

        public void UsePlay(Play p)
        {
            Place(p.Coords);
            foreach (var t in p.AffectedTiles)
                Board[t.Coords].Flip();

            var i = 0;
            while (i <= 2 && PossiblePlays().Count == 0)
            {
                ++i;
                NextTurn();
            }
            _deadlock = i >= 2;
        }

        public Dictionary<Tuple<int, int>, Play> PossiblePlays(bool otherPlayer = false)
        {
            var possiblePositions = Board.GetOpenAdjacentTiles();
            var res = new Dictionary<Tuple<int, int>, Play>();
            var playerColor = IsPlayerBlack ? TileColor.Black : TileColor.White;
            
            if (otherPlayer)
                playerColor = IsPlayerBlack ? TileColor.White : TileColor.Black;

            foreach (var coords in possiblePositions)
            {
                var possiblePlay = new Play(Board, playerColor, coords);
                if (possiblePlay.AffectedTiles != null)
                    res.Add(coords, possiblePlay);
            }

            return res;
        }

        public Game ForkGame(Play play)
        {
            var game = new Game(this);
            game.UsePlay(play);
            return game;
        }

        public bool GameOver() => Board.IsFull() || _deadlock;
    }
}