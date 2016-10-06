using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MysticSquare;
using Game = MysticSquare.Game;

namespace GameTests
{
    [TestClass]
    public class GameTest
    {
        protected virtual Game CreateGame(params int[] elements)
        {
            return new Game(elements);
        }

        [TestMethod]
        public void Shift_NeighborValue_CorrectlyShift()
        {
            var obj = CreateGame(1, 3, 2, 0);

            obj.Shift(3);

            var zeroPoint = obj.GetLocation(0);
            var actualRes = new[] {0, 1};
            var expectedRes = new[] {zeroPoint.x, zeroPoint.y};

            var point = obj.GetLocation(3);
            var expectedPointRes = new[] {point.x, point.y};
            var actualPointRes = new[] {1, 1};

            Assert.IsTrue(actualRes[0] == expectedRes[0]
                && actualRes[1] == expectedRes[1]
                && expectedPointRes[0] == actualPointRes[0]
                && expectedPointRes[1] == actualPointRes[1]);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Shift_NotNeighborValue_CorrectlyShift()
        {
            var obj = CreateGame(1, 3, 2, 0);
            obj.Shift(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Game_NotUniqueValues_ArgumentException()
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
            var obj = new Game(1, 2, 5, 4, 6, 7, 3, 8, 0);
            var point = obj.GetLocation(4);

            var expectedCoords = new[] {point.x, point.y};
            var actualCoords = new[] {1, 0};

            CollectionAssert.AreEqual(expectedCoords, actualCoords);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLocation_NotValidValue_ArgumentException()
        {
            var obj = new Game(1, 2, 5, 4, 6, 7, 8, 3, 0);
            var coord = obj.GetLocation(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetIndex_NotValidIndex_NullReferenceException()
        {
            var obj = new Game(1, 2, 5, 4, 6, 7, 12, 9, 0);
            Assert.AreEqual(obj[-1, 0], null);
        }

        [TestMethod]
        public void GetIndex_ValidIndex_ReturnCoordinates()
        {
            var obj = new Game(1, 2, 5, 4, 6, 7, 8, 3, 0);
            Assert.AreEqual(obj[1, 1], 6);
        }
    }

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