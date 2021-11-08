using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ReversiBase
{
    [Serializable]
    public class Board
    {
        /// <summary>
        /// Size of board's dimensions
        /// </summary>
        public const uint Size = 8;

        private readonly Tile[,] _board;

        public Board(uint size = Size)
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

        public Tile this[int x, int y] => _board[x, y];

        public Tile this[string coords]
        {
            get 
            { 
                var (x, y) = Tile.StringToCoordinates(coords);
                return this[x, y];
            }
        }

        public Tile this[Tuple<int, int> coords] => this[coords.Item1, coords.Item2];

        public Tile Place(int x, int y, TileColor color)
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

        private bool IsAdjacent(int x, int y)
        {
            for(int xi = Math.Max(0, x - 1); xi <= x + 1 && xi < Size; xi++)
                for (int yi = Math.Max(0, y - 1); yi <= y + 1 && yi < Size; yi++)
                    if (_board[xi, yi] != null) return true;
            return false;
        }

        public List<Tuple<int, int>> GetOpenAdjacentTiles()
        {
            var openAdjacent = new List<Tuple<int, int>>();
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                    if (_board[x, y] == null && IsAdjacent(x, y))
                        openAdjacent.Add(Tuple.Create(x, y));
            return openAdjacent;
        }

        public bool IsFull() => _board.Cast<Tile>().All(tile => tile != null);

        public int GetColorCount(TileColor color) => _board.Cast<Tile>().Count(tile => tile != null && tile.Color == color);
    }
}