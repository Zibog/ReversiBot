using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ReversiBase;

namespace ReversiUI
{
    public partial class ReversiUI : Form
    {
        private enum GameMode
        {
            Human,
            Tile,
            Mobility,
            Corners, 
            Weighted,
        }

        GameManager manager;
        Game game;
        private GameMode whiteMode = GameMode.Human;
        private GameMode blackMode = GameMode.Human;
        private int whitePlyVal = 5;
        private int blackPlyVal = 5;
        private DataGridView gameBoard;
        private const int BOARD_SIZE = 8;
        private Bitmap blank;
        private Bitmap black;
        private Bitmap white;
        private Bitmap hint;
        private int bitmapPadding = 6;

        Dictionary<Tuple<int, int>, Play> playable;

        public ReversiUI()
        {
            blank = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0-windows", @"\green.bmp"));
            black = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0-windows", @"\black.bmp"));
            white = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0-windows", @"\white.bmp"));
            hint = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0-windows", @"\hint.bmp"));


            manager = new GameManager(BOARD_SIZE);
            game = manager.GetGame();

            InitializeComponent();

            gameBoard = new DataGridView
            {
                BackColor = SystemColors.ButtonShadow,
                ForeColor = SystemColors.ControlText,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Size = new Size(855, 582),
                AutoSize = false,
                Name = "gameBoard",
            };

            gameBoard.AllowUserToAddRows = false;

            ConfigureForm();
            SizeGrid();
            CreateColumns();
            CreateRows();

            playable = game.PossiblePlays();

            UpdateBoard();
        }


        #region SetupGrid
        
        private void ConfigureForm()
        {
            AutoSize = true;

            gameBoard.AllowUserToAddRows = false;
            gameBoard.CellClick += new
                DataGridViewCellEventHandler(ClickCell);
            gameBoard.SelectionChanged += new
                EventHandler(Change_Selection);

            GamePanel.Controls.Add(gameBoard);
        }


        private void SizeGrid()
        {
            gameBoard.ColumnHeadersVisible = false;
            gameBoard.RowHeadersVisible = false;
            gameBoard.AllowUserToResizeColumns = false; ;
            gameBoard.AllowUserToResizeRows = false;
            gameBoard.BorderStyle = BorderStyle.None;
            gameBoard.RowTemplate.Height = blank.Height +
                                           2 * bitmapPadding + 1;
            gameBoard.AutoSize = true;
        }


        private void CreateColumns()
        {
            DataGridViewImageColumn imageColumn;
            int columnCount = 0;
            do
            {
                Bitmap unMarked = blank;
                imageColumn = new DataGridViewImageColumn();

                imageColumn.Width = blank.Width + 2 * bitmapPadding + 1;

                imageColumn.Image = unMarked;
                gameBoard.Columns.Add(imageColumn);
                columnCount = columnCount + 1;
            }
            while (columnCount < Board.Size);
        }

        private void CreateRows()
        {
            for (int i = 0; i < Board.Size; i++)
            {
                gameBoard.Rows.Add();
            }
        }

        private void Change_Selection(object sender, EventArgs e)
        {
            this.gameBoard.ClearSelection();
        }
        
        #endregion

        private void RenderGameOver()
        {
            var res = MessageBox.Show(
                "Player " + (game.Winner == TileColor.Black ? "Black " : "White ") +
                "won. Do do you want to play again?", "The End", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                Reset(this, EventArgs.Empty);
            }
        }

        private void ClickCell(object sender, DataGridViewCellEventArgs e)
        {
            Tuple<int, int> destCoords = Tuple.Create(e.ColumnIndex, e.RowIndex);

            playable.TryGetValue(destCoords, out Play p);
            Play humanPlay = manager.OutsidePlay(p);
            if (humanPlay == null) return;
            Game next = manager.Next();
            if(next != null)
            {
                game = next;
            }
            else
            {
                throw new ArgumentException("No human player/other game manager error");
            }
            playable = game.PossiblePlays();
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            for (int x = 0; x < Board.Size; x++)
            {
                for(int y = 0; y < Board.Size; y++)
                {
                    DataGridViewImageCell cell = (DataGridViewImageCell)gameBoard.Rows[y].Cells[x];
                    switch (game.ColorAt(x, y))
                    {
                        case TileColor.Black:
                            cell.Value = black;
                            break;
                        case TileColor.White:
                            cell.Value = white;
                            break;
                        default:
                            if (playable != null && playable.ContainsKey(Tuple.Create(x, y)))
                            {
                                cell.Value = hint;
                            } else
                            {
                                cell.Value = blank;
                            }
                            break;
                    }
                }
            }

            if (game.Winner != null)
            {
                RenderGameOver();
            }
            
            string player = game.IsPlayerBlack ? "Black" : "White";
            TileColor playerColor = game.IsPlayerBlack ? TileColor.Black : TileColor.White;

            Console.WriteLine("The tile counting heuristic returns: " + Solver.TileCountHeuristic(game, playerColor) + " for " + player);
            Console.WriteLine("The corners heuristic returns: " + Solver.CornersHeuristic(game, playerColor) + " for " + player);
            Console.WriteLine("The weighted heuristic returns: " + Solver.WeightedHeuristic(game, playerColor) + " for " + player);
            Console.WriteLine("The mobility heuristic returns: " + Solver.TileCountHeuristic(game, playerColor) + " for " + player);
        }

        private void ChangeGameMode(object sender, EventArgs e)
        {
            RadioButton c = (RadioButton)sender;
            if (!c.Checked) return;
            switch (c.Tag)
            {
                case "cornersWhite":
                    whiteMode = GameMode.Corners;
                    break;
                case "cornersBlack":
                    blackMode = GameMode.Corners;
                    break;
                case "humanWhite":
                    whiteMode = GameMode.Human;
                    break;
                case "humanBlack":
                    blackMode = GameMode.Human;
                    break;
                case "weightedWhite":
                    whiteMode = GameMode.Weighted;
                    break;
                case "weightedBlack":
                    blackMode = GameMode.Weighted;
                    break;
                case "tileWhite":
                    whiteMode = GameMode.Tile;
                    break;
                case "tileBlack":
                    blackMode = GameMode.Tile;
                    break;
            }
            SetNewGame();
        }

        private void SetNewGame()
        {
            Func<Game, TileColor, int> whiteHeuristic = null;
            Func<Game, TileColor, int> blackHeuristic = null;
            switch (whiteMode)
            {
                case GameMode.Corners:
                    whiteHeuristic = Solver.CornersHeuristic;
                    break;
                case GameMode.Tile:
                    whiteHeuristic = Solver.TileCountHeuristic;
                    break;
                case GameMode.Weighted:
                    whiteHeuristic = Solver.WeightedHeuristic;
                    break;
                case GameMode.Mobility:
                    whiteHeuristic = Solver.ActualMobilityHeuristic;
                    break;
            }

            switch (blackMode)
            {
                case GameMode.Corners:
                    blackHeuristic = Solver.CornersHeuristic;
                    break;
                case GameMode.Tile:
                    blackHeuristic = Solver.TileCountHeuristic;
                    break;
                case GameMode.Weighted:
                    blackHeuristic = Solver.WeightedHeuristic;
                    break;
                case GameMode.Mobility:
                    blackHeuristic = Solver.ActualMobilityHeuristic;
                    break;
            }

            if (whiteMode == GameMode.Human && blackMode == GameMode.Human)
            {
                manager = new GameManager();
            }
            else if (whiteMode == GameMode.Human)
            {
                manager = new GameManager(blackHeuristic, blackPlyVal, TileColor.Black);
            }
            else if (blackMode == GameMode.Human)
            {
                manager = new GameManager(whiteHeuristic, whitePlyVal, TileColor.White);
            }
            else
            {
                manager = new GameManager(blackHeuristic, blackPlyVal, whiteHeuristic, whitePlyVal);
            }

            game = manager.GetGame();
            playable = game.PossiblePlays();
            UpdateBoard();
        }

        private void NextMove(object sender, EventArgs e)
        {
            if (game.Winner != null) return;
            Game next = manager.Next();
            if (next != null) game = next;
            playable = game.PossiblePlays();
            UpdateBoard();

        }

        private void Reset(object sender, EventArgs e)
        {
            manager.Reset();
            game = manager.GetGame();
            playable = game.PossiblePlays();
            UpdateBoard();
        }

        private void SetPly(object sender, EventArgs e)
        {
            NumericUpDown c = (NumericUpDown)sender;
            switch (c.Tag)
            {
                case "white":
                    whitePlyVal = Convert.ToInt32(c.Value);
                    break;
                case "black":
                    blackPlyVal = Convert.ToInt32(c.Value);
                    break;
            }
            SetNewGame();
        }
    }
}
