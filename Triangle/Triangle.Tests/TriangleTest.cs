using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;

namespace TriangleTests
{
    [TestClass]
    public class TriangleTest
    {


        [TestMethod]
        public void CheckTheAreaOfTriangleWithThreeSidesEqualToSix()
        {
            ThreeSides obj = new ThreeSides(3, 4, 5);
            Triangle triangle = new Triangle(obj);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void CheckTheAreaOfTriangleWithTwoSidesAndOneAngleEqualToTwentyFour()
        {
            TwoSidesAndAngle obj = new TwoSidesAndAngle(6, 8, 90);
            Triangle triangle = new Triangle(obj);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 24);
        }

        [TestMethod]
        public void CheckTheAreaOfTriangleWithOneSideAndTwoAnglesEqualToTwentyOne()
        {
            OneSideAndTwoAngles obj = new OneSideAndTwoAngles(10, 60, 30);
            Triangle triangle = new Triangle(obj);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 21.65);
        }
            
    }
}
