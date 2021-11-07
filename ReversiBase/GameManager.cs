using System;

namespace ReversiBase
{
    public class GameManager
    {
        private Game _game;
        public Solver[] Agents = new Solver[2];
        private Play[] _humanPlay = new Play[2];
        private int _play;

        public GameManager(Func<Game, TileColor, int> heuristic1, int ply1, Func<Game, TileColor, int> heuristic2,
            int ply2, uint size = Board.Size)
        {
            _game = new Game(size);
            Agents[0] = new Solver(TileColor.Black, heuristic1, ply1);
            Agents[1] = new Solver(TileColor.White, heuristic2, ply2);
            _play = 0;
        }

        public GameManager(Func<Game, TileColor, int> heuristic, int ply, TileColor color, uint size = Board.Size)
        {
            _game = new Game(size);
            var index = color == TileColor.Black ? 0 : 1;
            Agents[index] = new Solver(color, heuristic, ply);
            _play = 0;
        }

        public GameManager(uint size = Board.Size)
        {
            _game = new Game(size);
            _play = 0;
        }

        public Game Next()
        {
            var index = _game.IsPlayerBlack ? 0 : 1;
            ++_play;
            var agent = Agents[index];

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
        
        public Game GetGame() => new(_game);

        public void Reset() => _game = new Game();
    }
}