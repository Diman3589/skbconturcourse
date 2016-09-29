using System;

namespace Program
{

    public class Triangle
    {
        private double _sideA;
        private double _sideB;
        private double _sideC;

        private Triangle()
        {
        }

        public static Triangle ThreeSides(double sideA, double sideB, double sideC)
        {
            var obj = new Triangle
            {
                _sideA = sideA,
                _sideB = sideB,
                _sideC = sideC
            };

            if (!CheckSide(obj._sideA) || !CheckSide(sideB) || !CheckSide(sideC))
            {
                throw new ArgumentException("Invalid side!");
            }

            return obj;
        }

        public static Triangle TwoSidesAndAngle(double sideA, double sideB, double angle)
        {
            var obj = new Triangle();
            if (!CheckSide(sideA) || !CheckSide(sideB))
            {
                throw new ArgumentException("Invalid side!");
            }

            if (CheckAngle(new[] {angle}))
            {
                obj._sideA = sideA;
                obj._sideB = sideB;
                obj._sideC =
                    Math.Sqrt(Math.Pow(sideA, 2) + Math.Pow(sideB, 2) -
                              2*sideA*sideB*Math.Cos(ToRadian(angle)));
            }
            else
            {
                throw new ArgumentException("Invalid angle!");
            }
            return obj;
        }

        public static Triangle OneSideAndTwoAngles(double sideC, double angleOne, double angleTwo)
        {
            var obj = new Triangle();

            if (!CheckSide(sideC))
            {
                throw new ArgumentException("Invalid side!");
            }

            if (CheckAngle(new[] {angleOne, angleTwo}))
            {
                obj._sideC = sideC;
                var angleThree = 180 - (angleOne + angleTwo);
                obj._sideA = sideC*Math.Sin(ToRadian(angleOne))/Math.Sin(ToRadian(angleThree));
                obj._sideB = sideC*Math.Sin(ToRadian(angleTwo))/Math.Sin(ToRadian(angleThree));
            }
            else
            {
                throw new ArgumentException("Invalid angle!");
            }
            return obj;
        }

        public static bool CheckAngle(double[] angles)
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

        public static bool CheckSide(double side)
        {
            return side > 0;
        }

        public static double ToRadian(double degrees) => degrees*Math.PI/180;

        public double CalculateArea()
        {
            var p = (_sideA + _sideB + _sideC)/2;
            return Math.Sqrt(p*(p - _sideA)*(p - _sideB)*(p - _sideC));
        }

        public static void Main(string[] args)
        {
        }
    }
}