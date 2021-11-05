using System;
using System.Collections.Generic;

namespace ReversiBase
{
    class Play
    {
        public TileColor Color { private set; get; }
        public Tuple<int, int> Coords { private set; get; }
        public List<Tile> AffectedTiles { private set; get; }

        public Play(TileColor color, Tuple<int, int> coords, List<Tile> affected)
        {
            Color = color;
            Coords = coords;
            AffectedTiles = affected;
        }

        public Play(Board board, TileColor color, Tuple<int, int> coords)
        {
            Color = color;
            Coords = coords;

            var playerColor = color;
            var opponentColor = color == TileColor.Black ? TileColor.White : TileColor.Black;

            var x = coords.Item1;
            var y = coords.Item2;

            for (double theta = 0; theta < 2 * Math.PI; theta += (Math.PI / 4))
            {
                var rayTiles = new List<Tile>();

                var dx = (int) Math.Round(Math.Cos(theta), MidpointRounding.AwayFromZero);
                var dy = (int) Math.Round(Math.Sin(theta), MidpointRounding.AwayFromZero);

                var ix = x + dx;
                var iy = y + dy;

                while (ix >= 0 && ix < Board.Size && iy >= 0 && iy < Board.Size)
                {
                    if (board[ix, iy] == null)
                        break;
                    
                    if (board[ix, iy].Color != opponentColor && ix == x + dx && iy == y + dy)
                        break;

                    if (board[ix, iy].Color == playerColor)
                    {
                        AddAffected(rayTiles);
                        break;
                    }
                    
                    rayTiles.Add(board[ix, iy]);

                    ix += dx;
                    iy += dy;
                }
            }
        }

        private void AddAffected(Tile affectedTile)
        {
            AffectedTiles ??= new List<Tile>();
            AffectedTiles.Add(affectedTile);
        }

        private void AddAffected(List<Tile> affectedTiles)
        {
            AffectedTiles ??= new List<Tile>();
            AffectedTiles.AddRange(affectedTiles);
        }
    }
}