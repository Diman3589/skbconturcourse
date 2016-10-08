using System.Collections.Generic;

namespace MysticSquare
{
    public class GameDecorator
    {
        public Game _firstGame { get; }
        private Game _currentGame;
        private List<int> _logList;

        public GameDecorator(Game game)
        {
            _firstGame = game;
            _logList = new List<int>();
        }

        private void CreateCopyGame()
        {
            _currentGame = new Game(_firstGame);
            foreach (var log in _logList)
            {
                _currentGame.Shift(log);
            }
        }

        public new int this[int x, int y]
        {
            get
            {
                CreateCopyGame();
                return _currentGame[x, y];    
            }
        }

        public new Point GetLocation(int value)
        {
            CreateCopyGame();
            return _currentGame.GetLocation(value);
        }

        public new void Shift(int value)
        {
            _logList.Add(value);
        }
    }
}
