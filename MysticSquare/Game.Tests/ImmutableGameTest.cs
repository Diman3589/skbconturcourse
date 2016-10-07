using Microsoft.VisualStudio.TestTools.UnitTesting;
using MysticSquare;
using Game = MysticSquare.Game;

namespace GameTests
{
    [TestClass]
    public class ImmutableGameTest : GameTest
    {
        protected override Game CreateGame(params int[] elements)
        {
            return new ImmutableGame(elements);
        }

        [TestMethod]
        public void ImmutableShift_NeighborValue_ImmutableObject()
        {
            Game game = CreateGame(1, 2, 3, 4, 5, 6, 7, 8, 0);
            Game newGame = game.Shift(8);

            var newPoint = newGame.GetLocation(8);
            var newResult = new[] {newPoint.x, newPoint.y};

            var oldPoint = game.GetLocation(8);
            var oldResult = new[] {oldPoint.x, oldPoint.y};

            Assert.IsTrue(newResult[0] == oldResult[0]
                && newResult[1] != oldResult[1]);

        }
    }
}
