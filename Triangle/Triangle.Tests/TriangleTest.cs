using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Triangle.Tests
{
    [TestClass]
    public class TriangleTest
    {
        [TestMethod]
        public void CalculateArea_ValidThreeSides_EqualSix()
        {
            var triangle =  Triangle.ThreeSides(3, 4, 5);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void CalculateArea_ValidTwoSidesAndAngle_EqualTwentyFour()
        {
            var triangle = Triangle.TwoSidesAndAngle(6, 8, 90);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 24);
        }

        [TestMethod]
        public void CalculateArea_ValidOneSideAndTwoAngles_EqualTwentyOneDotSixtyFive()
        {
            var triangle = Triangle.OneSideAndTwoAngles(10, 60, 30);

            var result = triangle.CalculateArea();
            result = Math.Round(result, 2);

            Assert.AreEqual(result, 21.65);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckSide_InvalidSideThreeSides_ThrowArgumentException()
        {
            Triangle.ThreeSides(-5, 4, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckAngle_InvalidAngleTwoSidesAndAngle_ThrowArgumentException()
        {
            Triangle.TwoSidesAndAngle(5, 3, 900);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckSide_InvalidSideOneSideAndTwoAngles_ThrowArgumentException()
        {
            Triangle.OneSideAndTwoAngles(-5, 12, 30);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckAngle_InvalidAngleOneSideAndTwoAngles_ThrowArgumentException()
        {
            Triangle.OneSideAndTwoAngles(13, 135, 80);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckSide_InvalidSideTwoSidesAndOneAngle_ThrowArgumentException()
        {
            Triangle.TwoSidesAndAngle(0, 15, 60);
        }
    }
}