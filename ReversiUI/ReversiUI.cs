using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ReversiBase;

namespace ReversiUI
{
    public partial class ReversiUi : Form
    {
        private enum GameMode
        {
            Human,
            Tile,
            Mobility,
            Corners, 
            Weighted,
        }

        private GameManager _manager;
        private Game _game;
        private GameMode _whiteMode = GameMode.Human;
        private GameMode _blackMode = GameMode.Human;
        private int _whitePlyVal = 5;
        private int _blackPlyVal = 5;
        private readonly DataGridView _gameBoard;
        private const int BoardSize = 8;
        private readonly Bitmap _blank;
        private readonly Bitmap _black;
        private readonly Bitmap _white;
        private readonly Bitmap _hint;
        private const int BitmapPadding = 6;

        private Dictionary<Tuple<int, int>, Play> _playable;

        public ReversiUi()
        {
            _blank = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0-windows", @"\green.bmp"));
            _black = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0-windows", @"\black.bmp"));
            _white = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0-windows", @"\white.bmp"));
            _hint = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0-windows", @"\hint.bmp"));


            _manager = new GameManager(BoardSize);
            _game = _manager.GetGame();

            InitializeComponent();

            _gameBoard = new DataGridView
            {
                BackColor = SystemColors.ButtonShadow,
                ForeColor = SystemColors.ControlText,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Size = new Size(855, 582),
                AutoSize = false,
                Name = "gameBoard",
            };

            _gameBoard.AllowUserToAddRows = false;

            ConfigureForm();
            SizeGrid();
            CreateColumns();
            CreateRows();

            _playable = _game.PossiblePlays();

            UpdateBoard();
        }


        #region SetupGrid
        
        private void ConfigureForm()
        {
            AutoSize = true;

            _gameBoard.AllowUserToAddRows = false;
            _gameBoard.CellClick += ClickCell;
            _gameBoard.SelectionChanged += Change_Selection;

            GamePanel.Controls.Add(_gameBoard);
        }


        private void SizeGrid()
        {
            _gameBoard.ColumnHeadersVisible = false;
            _gameBoard.RowHeadersVisible = false;
            _gameBoard.AllowUserToResizeColumns = false;
            _gameBoard.AllowUserToResizeRows = false;
            _gameBoard.BorderStyle = BorderStyle.None;
            _gameBoard.RowTemplate.Height = _blank.Height + 2 * BitmapPadding + 1;
            _gameBoard.AutoSize = true;
        }


        private void CreateColumns()
        {
            var columnCount = 0;
            do
            {
                var imageColumn = new DataGridViewImageColumn();

                imageColumn.Width = _blank.Width + 2 * BitmapPadding + 1;

                imageColumn.Image = _blank;
                _gameBoard.Columns.Add(imageColumn);
                ++columnCount;
            }
            while (columnCount < Board.Size);
        }

        private void CreateRows()
        {
            for (var i = 0; i < Board.Size; i++)
                _gameBoard.Rows.Add();
        }

        private void Change_Selection(object sender, EventArgs e)
        {
            _gameBoard.ClearSelection();
        }
        
        #endregion

        private void RenderGameOver()
        {
            var res = MessageBox.Show(
                "Player " + (_game.Winner == TileColor.Black ? "Black " : "White ") +
                "won. Do do you want to play again?", "The End", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                Reset(this, EventArgs.Empty);
        }

        private void ClickCell(object sender, DataGridViewCellEventArgs e)
        {
            var destCoords = Tuple.Create(e.ColumnIndex, e.RowIndex);

            _playable.TryGetValue(destCoords, out var p);
            var humanPlay = _manager.OutsidePlay(p);
            if (humanPlay == null) return;
            var next = _manager.Next();
            if(next != null)
                _game = next;
            else
                throw new ArgumentException("No human player/other game manager error");
            _playable = _game.PossiblePlays();
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            for (var x = 0; x < Board.Size; x++)
            {
                for(var y = 0; y < Board.Size; y++)
                {
                    var cell = (DataGridViewImageCell)_gameBoard.Rows[y].Cells[x];
                    switch (_game.ColorAt(x, y))
                    {
                        case TileColor.Black:
                            cell.Value = _black;
                            break;
                        case TileColor.White:
                            cell.Value = _white;
                            break;
                        default:
                            if (_playable != null && _playable.ContainsKey(Tuple.Create(x, y)))
                                cell.Value = _hint;
                            else
                                cell.Value = _blank;
                            break;
                    }
                }
            }

            if (_game.Winner != null)
                RenderGameOver();
            
            var player = _game.IsPlayerBlack ? "Black" : "White";
            var playerColor = _game.IsPlayerBlack ? TileColor.Black : TileColor.White;

            Console.WriteLine("The tile counting heuristic returns: " + Solver.TileCountHeuristic(_game, playerColor) + " for " + player);
            Console.WriteLine("The corners heuristic returns: " + Solver.CornersHeuristic(_game, playerColor) + " for " + player);
            Console.WriteLine("The weighted heuristic returns: " + Solver.WeightedHeuristic(_game, playerColor) + " for " + player);
            Console.WriteLine("The mobility heuristic returns: " + Solver.TileCountHeuristic(_game, playerColor) + " for " + player);
        }

        private void ChangeGameMode(object sender, EventArgs e)
        {
            var c = (RadioButton)sender;
            if (!c.Checked) return;
            switch (c.Tag)
            {
                case "cornersWhite":
                    _whiteMode = GameMode.Corners;
                    break;
                case "cornersBlack":
                    _blackMode = GameMode.Corners;
                    break;
                case "humanWhite":
                    _whiteMode = GameMode.Human;
                    break;
                case "humanBlack":
                    _blackMode = GameMode.Human;
                    break;
                case "weightedWhite":
                    _whiteMode = GameMode.Weighted;
                    break;
                case "weightedBlack":
                    _blackMode = GameMode.Weighted;
                    break;
                case "tileWhite":
                    _whiteMode = GameMode.Tile;
                    break;
                case "tileBlack":
                    _blackMode = GameMode.Tile;
                    break;
            }
            SetNewGame();
        }

        private void SetNewGame()
        {
            Func<Game, TileColor, int> whiteHeuristic = null;
            Func<Game, TileColor, int> blackHeuristic = null;
            whiteHeuristic = _whiteMode switch
            {
                GameMode.Corners => Solver.CornersHeuristic,
                GameMode.Tile => Solver.TileCountHeuristic,
                GameMode.Weighted => Solver.WeightedHeuristic,
                GameMode.Mobility => Solver.ActualMobilityHeuristic,
                _ => whiteHeuristic
            };

            blackHeuristic = _blackMode switch
            {
                GameMode.Corners => Solver.CornersHeuristic,
                GameMode.Tile => Solver.TileCountHeuristic,
                GameMode.Weighted => Solver.WeightedHeuristic,
                GameMode.Mobility => Solver.ActualMobilityHeuristic,
                _ => blackHeuristic
            };

            if (_whiteMode == GameMode.Human && _blackMode == GameMode.Human)
            {
                _manager = new GameManager();
            }
            else if (_whiteMode == GameMode.Human)
            {
                _manager = new GameManager(blackHeuristic, _blackPlyVal, TileColor.Black);
            }
            else if (_blackMode == GameMode.Human)
            {
                _manager = new GameManager(whiteHeuristic, _whitePlyVal, TileColor.White);
            }
            else
            {
                _manager = new GameManager(blackHeuristic, _blackPlyVal, whiteHeuristic, _whitePlyVal);
            }

            _game = _manager.GetGame();
            _playable = _game.PossiblePlays();
            UpdateBoard();
        }

        private void NextMove(object sender, EventArgs e)
        {
            if (_game.Winner != null) return;
            var next = _manager.Next();
            if (next != null) _game = next;
            _playable = _game.PossiblePlays();
            UpdateBoard();
        }

        private void Reset(object sender, EventArgs e)
        {
            _manager.Reset();
            _game = _manager.GetGame();
            _playable = _game.PossiblePlays();
            UpdateBoard();
        }

        private void SetPly(object sender, EventArgs e)
        {
            var c = (NumericUpDown)sender;
            switch (c.Tag)
            {
                case "white":
                    _whitePlyVal = Convert.ToInt32(c.Value);
                    break;
                case "black":
                    _blackPlyVal = Convert.ToInt32(c.Value);
                    break;
            }
            SetNewGame();
        }
    }
}
