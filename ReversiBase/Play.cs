using System;
using System.Collections.Generic;

namespace ReversiBase
{
    public class Play
    {
        public TileColor Color { get; }
        public Tuple<int, int> Coords { get; }
        public List<Tile> AffectedTiles { private set; get; }

        public Play(Board board, TileColor color, Tuple<int, int> coords)
        {
            Color = color;
            Coords = coords;

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

                    if (board[ix, iy].Color == color)
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

        private void AddAffected(IEnumerable<Tile> affectedTiles)
        {
            AffectedTiles ??= new List<Tile>();
            AffectedTiles.AddRange(affectedTiles);
        }
    }
}