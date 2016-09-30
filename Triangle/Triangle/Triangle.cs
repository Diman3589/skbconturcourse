using System;

namespace Triangle
{
    public class Triangle
    {
        private readonly double _sideA;
        private readonly double _sideB;
        private readonly double _sideC;

        private Triangle(double sideA, double sideB, double sideC)
        {
            _sideA = sideA;
            _sideB = sideB;
            _sideC = sideC;
        }

        public static Triangle ThreeSides(double sideA, double sideB, double sideC)
        {
            if (!CheckSide(sideA) || !CheckSide(sideB) || !CheckSide(sideC))
                throw new ArgumentException("Invalid side!");

            return new Triangle(sideA, sideB, sideC);
        }

        public static Triangle TwoSidesAndAngle(double sideA, double sideB, double angle)
        {
            if (!CheckSide(sideA) || !CheckSide(sideB))
            {
                throw new ArgumentException("Invalid side!");
            }

            if (!CheckAngles(new[] {angle}))
                throw new ArgumentException("Invalid angle!");

            var sideC = Math.Sqrt(Math.Pow(sideA, 2) + Math.Pow(sideB, 2) - 2*sideA*sideB*Math.Cos(ToRadian(angle)));
            return new Triangle(sideA, sideB, sideC);
        }

        public static Triangle OneSideAndTwoAngles(double sideC, double angleOne, double angleTwo)
        {
            if (!CheckSide(sideC))
            {
                throw new ArgumentException("Invalid side!");
            }

            if (!CheckAngles(new[] {angleOne, angleTwo}))
                throw new ArgumentException("Invalid angle!");

            var angleThree = 180 - (angleOne + angleTwo);
            var sideA = sideC*Math.Sin(ToRadian(angleOne))/Math.Sin(ToRadian(angleThree));
            var sideB = sideC*Math.Sin(ToRadian(angleTwo))/Math.Sin(ToRadian(angleThree));
            return new Triangle(sideA, sideB, sideC);
        }

        private static bool CheckAngles(double[] angles)
        {
            var sumAngles = 0.0;

            foreach (var angle in angles)
            {
                if (angle > 0 && angle < 180)
                    sumAngles += angle;
                else return false;
            }
            return sumAngles < 180;
        }

        private static bool CheckSide(double side)
        {
            return side > 0;
        }

        private static double ToRadian(double degrees) => degrees*Math.PI/180;

        public double CalculateArea()
        {
            var p = (_sideA + _sideB + _sideC)/2;
            return Math.Sqrt(p*(p - _sideA)*(p - _sideB)*(p - _sideC));
        }
    }
}