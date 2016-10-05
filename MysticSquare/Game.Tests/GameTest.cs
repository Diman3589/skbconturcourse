using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game = MysticSquare.Game;

namespace GameTests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Shift_ValidValues_CorrectlyShift()
        {
            var obj = new Game(1, 2, 5, 0);
            obj.Shift(5);
            var coord = obj.GetLocation(5);
            CollectionAssert.AreEqual(coord, new[] {1,1});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Shift_NotUniqueValues_ArgumentException()
        {
            new Game(1, 2, 3, 3, 5, 6, 7, 8, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckParams_NegativeValues_ArgumentException()
        {
            new Game(1, 2, 3, 4, 5, 6, -2, 8, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckParams_NoValues_ArgumentException()
        {
            new Game();
        }

        [TestMethod]
        public void GetLocation_ValidValue_CoordinatesOfElement()
        {
            var obj = new Game(1, 2, 5, 4, 6, 7, 12, 9, 0);
            var coord = obj.GetLocation(4);
            CollectionAssert.AreEqual(coord, new[] {1, 0});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLocation_NotValidValue_ArgumentException()
        {
            var obj = new Game(1, 2, 5, 4, 6, 7, 12, 9, 0);
            var coord = obj.GetLocation(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIndex_NotValidIndex_ArgumentException()
        {
            var obj = new Game(1, 2, 5, 4, 6, 7, 12, 9, 0);
            obj.GetValue(-1, 0);
        }

        [TestMethod]
        public void GetIndex_ValidIndex_ReturnCoordinates()
        {
            var obj = new Game(1, 2, 5, 4, 6, 7, 12, 9, 0);
            var value = obj.GetValue(1, 1);
            Assert.AreEqual(value, 6);
        }

    }
}
