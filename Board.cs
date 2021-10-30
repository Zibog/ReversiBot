using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ReversiBot
{
    public class Board
    {
        /// <summary>
        /// Size of board's dimensions
        /// </summary>
        private const uint Size = 8;

        private readonly Tile[,] _board;

        public Board(uint size)
        {
            _board = new Tile[size, size];
        }

        public Board(Board other)
        {
            _board = Clone(other)._board;
        }

        private static Board Clone(Board b)
        {
            using var ms = new MemoryStream();
            var fmt = new BinaryFormatter();
            fmt.Serialize(ms, b);
            ms.Position = 0;
            return (Board) fmt.Deserialize(ms);
        }

        private Tile this[int x, int y] => _board[x, y];

        public Tile this[string coords]
        {
            get 
            { 
                var (x, y) = Tile.StringToCoordinates(coords);
                return this[x, y];
            }
        }

        private Tile Place(int x, int y, TileColor color)
        {
            if (_board[x, y] != null)
                return null;
            var placement = new Tile(color);
            placement.Place(x, y);
            _board[x, y] = placement;
            return placement;
        }

        public Tile Place(string coords, TileColor color)
        {
            var (x, y) = Tile.StringToCoordinates(coords);
            return Place(x, y, color);
        }

        public bool IsFull() => _board.Cast<Tile>().All(tile => tile != null);

        public int GetColorCount(TileColor color) => _board.Cast<Tile>().Count(tile => tile.Color == color);
    }
}