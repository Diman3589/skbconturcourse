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
        public override void Shift_NeighborValue_CorrectlyShift()
        {
            var obj = CreateGame(1, 3, 2, 0);

            var oldPoint = obj.GetLocation(3);
            var oldZeroPoint = obj.GetLocation(0);

            obj.Shift(3);

            var zeroPoint = obj.GetLocation(0);
            var point = obj.GetLocation(3);

            Assert.AreEqual(point, oldPoint);
            Assert.AreEqual(oldZeroPoint, zeroPoint);
        }
    }
}
