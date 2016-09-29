using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;

namespace TriangleTests
{
    [TestClass]
    public class TriangleTest
    {
        [TestMethod]
        public void CalculateArea_ValidThreeSides_EqualSix()
        {
            var obj = new ThreeSides(3, 4, 5);
            var triangle = new Triangle(obj);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void CalculateArea_ValidTwoSidesAndAngle_EqualTwentyFour()
        {
            var obj = new TwoSidesAndAngle(6, 8, 90);
            var triangle = new Triangle(obj);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 24);
        }

        [TestMethod]
        public void CalculateArea_ValidOneSideAndTwoAngles_EqualTwentyOneDotSixtyFive()
        {
            var obj = new OneSideAndTwoAngles(10, 60, 30);
            var triangle = new Triangle(obj);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 21.65);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckSide_InvalidSideThreeSides_ThrowArgumentException()
        {
            var obj = new ThreeSides(-5, 4, -2);
            new Triangle(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckAngle_InvalidAngleTwoSidesAndAngle_ThrowArgumentException()
        {
            var obj = new TwoSidesAndAngle(5, 3, 900);
            new Triangle(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckSide_InvalidSideOneSideAndTwoAngles_ThrowArgumentException()
        {
            var obj = new OneSideAndTwoAngles(-5, 12, 30);
            new Triangle(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckAngle_InvalidAngleOneSideAndTwoAngles_ThrowArgumentException()
        {
            var obj = new OneSideAndTwoAngles(13, 135, 80);
            new Triangle(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckSide_InvalidSideTwoSidesAndOneAngle_ThrowArgumentException()
        {
            var obj = new TwoSidesAndAngle(0, 15, 60);
            new Triangle(obj);
        }

    }
}