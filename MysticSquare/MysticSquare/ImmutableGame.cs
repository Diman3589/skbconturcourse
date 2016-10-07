using System;

namespace MysticSquare
{
    public class ImmutableGame : Game
    {
        public ImmutableGame(params int[] elements) : base(elements)
        {
        }

        public ImmutableGame(ImmutableGame obj) : base(obj)
        {
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
