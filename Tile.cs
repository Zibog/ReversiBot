using System;

namespace ReversiBot
{
    public enum TileColor
    {
        Blank = 0,
        Black = 1,
        White = 2
    }
    
    public class Tile
    {
        public TileColor Color { get; private set; }
        private Tuple<int, int> Coordinates { get; set; }
        private bool _placed = false;

        public Tile(TileColor color)
        {
            Color = color;
            Coordinates = Tuple.Create(-1, -1);
        }

        public Tuple<int, int> Place(int x, int y)
        {
            _placed = true;
            Coordinates = Tuple.Create(x, y);
            return Coordinates;
        }

        public Tuple<int, int> Place(string coords)
        {
            if (_placed)
                throw new InvalidOperationException("Tile already placed at " + coords);
            var (x, y) = StringToCoordinates(coords);
            return Place(x, y);
        }

        public TileColor Flip()
        {
            if (Color == TileColor.Black)
                Color = TileColor.White;
            else
                Color = TileColor.Black;
            return Color;
        }

        public static (int, int) StringToCoordinates(string coords)
        {
            var x = coords[0] switch
            {
                'a' => 0,
                'b' => 1,
                'c' => 2,
                'd' => 3,
                'e' => 4,
                'f' => 5,
                'g' => 6,
                'h' => 7,
                _ => -1
            };
            var y = coords[1] - '0' - 1;
            return (x, y);
        }
    }
}