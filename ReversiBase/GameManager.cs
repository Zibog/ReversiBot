using System;

namespace ReversiBase
{
    public class GameManager
    {
        private Game _game;
        private readonly Solver[] _agents = new Solver[2];
        private readonly Play[] _humanPlay = new Play[2];

        public GameManager(Func<Game, TileColor, int> heuristic1, int ply1, Func<Game, TileColor, int> heuristic2,
            int ply2, uint size = Board.Size)
        {
            _game = new Game(size);
            _agents[0] = new Solver(TileColor.Black, heuristic1, ply1);
            _agents[1] = new Solver(TileColor.White, heuristic2, ply2);
        }

        public GameManager(Func<Game, TileColor, int> heuristic, int ply, TileColor color, uint size = Board.Size)
        {
            _game = new Game(size);
            var index = color == TileColor.Black ? 0 : 1;
            _agents[index] = new Solver(color, heuristic, ply);
        }

        public GameManager(uint size = Board.Size)
        {
            _game = new Game(size);
        }

        public Game Next()
        {
            var index = _game.IsPlayerBlack ? 0 : 1;
            var agent = _agents[index];

            if (agent == null)
            {
                if (_humanPlay[index] == null)
                    return null;
                _game.UsePlay(_humanPlay[index]);
                _humanPlay[0] = null;
                _humanPlay[1] = null;
            }
            else
            {
                var play = agent.ChoosePlay(_game);
                if (play != null)
                    _game.UsePlay(play);
                else
                    throw new ArgumentException();
            }
            return GetGame();
        }

        public Play OutsidePlay(Play p)
        {
            if (p == null) return null;
            var index = p.Color == TileColor.Black ? 0 : 1;
            var agent = _agents[index];

            if (agent != null)
            {
                Console.WriteLine("Do you think you're AI?");
                return null;
            }

            if (_game.IsPlayerBlack && p.Color != TileColor.Black
                || !_game.IsPlayerBlack && p.Color != TileColor.White)
                throw new ArgumentException();

            _humanPlay[index] = p;
            return p;
        }
        
        public Game GetGame() => new(_game);

        public void Reset() => _game = new Game();
    }
}