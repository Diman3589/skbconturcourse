using System;

namespace MysticSquare
{
    public class ImmutableGame : Game
    {
        public ImmutableGame(params int[] elements) : base(elements)
        {
        }

        private ImmutableGame(ImmutableGame obj)
        {
            _arrayPoints = new Point[obj._arrayPoints.Length];

            var n = Convert.ToInt32(Math.Sqrt(obj._arrayValues.Length));
            _arrayValues = new int[n,n];
            _arrayPoints = (Point[]) obj._arrayPoints.Clone();
            _arrayValues = (int[,]) obj._arrayValues.Clone();
        }

        public override Game Shift(int value)
        {
            base.Shift(value);
            Game newGame = new ImmutableGame(this);
            base.Shift(value);
            return newGame;
        }
    }
}
