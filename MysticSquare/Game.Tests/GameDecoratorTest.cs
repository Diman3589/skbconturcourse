using System;
using GameTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MysticSquare;

namespace GameTests
{
    [TestClass]
    public class GameDecoratorTest : GameTest
    {
        protected GameDecorator CreateGame(Game game)
        {
            return new GameDecorator(game);
        }

        [TestMethod]
        public override void Shift_NeighborValue_CorrectlyShift()
        {
            var game = new Game(1, 3, 2, 4, 7, 6, 8, 5, 0);
            var decoratorGame = CreateGame(game);

            decoratorGame.Shift(6);
            decoratorGame.Shift(7);
            decoratorGame.Shift(3);

            var oldPoint = game.GetLocation(0);
            var newPoint = decoratorGame.GetLocation(6);

            var oldGame = decoratorGame._firstGame;
            Assert.AreEqual(oldPoint, newPoint);
            Assert.AreEqual(game, oldGame);
        }
    }
}
