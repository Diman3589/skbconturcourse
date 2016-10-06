using System.Collections.Generic;

namespace MysticSquare
{
    public class ImmutableGameDecorator
    {
        private Game _firstGame;
        private ImmutableGame _currentGame;
        private List<int> _logList;

        ImmutableGameDecorator(ImmutableGame immutableGame)
        {
            _firstGame = immutableGame;
            _currentGame = immutableGame;
            _logList = new List<int>();
        }

        public int this[int x, int y] => _currentGame[x, y];

        public Point GetLocation(int value)
        {
            return _currentGame.GetLocation(value);
        }

        public ImmutableGameDecorator Shift(int value)
        {
            _currentGame = (ImmutableGame) _currentGame.Shift(value);
            _logList.Add(value);

            return this;
        }
    }
}
