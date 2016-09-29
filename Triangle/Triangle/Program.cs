using System;

namespace Program
{
    public struct ThreeSides
    {
        public double SideA;
        public double SideB;
        public double SideC;

        public ThreeSides(double a, double b, double c)
        {
            SideA = a;
            SideB = b;
            SideC = c;
        }
    }

    public struct TwoSidesAndAngle
    {
        public double SideA;
        public double SideB;
        public double Angle;

        public TwoSidesAndAngle(double a, double b, double angle)
        {
            SideA = a;
            SideB = b;
            Angle = angle;
        }
    }

    public struct OneSideAndTwoAngles
    {
        public double Side;
        public double AngleOne;
        public double AngleTwo;

        public OneSideAndTwoAngles(double a, double angleOne, double angleTwo)
        {
            Side = a;
            AngleOne = angleOne;
            AngleTwo = angleTwo;
        }
    }

    public class Triangle
    {
        private readonly double _sideA;
        private readonly double _sideB;
        private readonly double _sideC;

        public Triangle(ThreeSides obj)
        {
            _sideA = obj.SideA;
            _sideB = obj.SideB;
            _sideC = obj.SideC;
            if (!CheckSide(_sideA) || !CheckSide(_sideB) || !CheckSide(_sideC))
            {
                throw new ArgumentException("Invalid side!");
            }
        }

        public Triangle(TwoSidesAndAngle obj)
        {
            _sideA = obj.SideA;
            _sideB = obj.SideB;
            if (!CheckSide(_sideA) || !CheckSide(_sideB))
            {
                throw new ArgumentException("Invalid side!");
            }

            var angleOne = obj.Angle;
            if (CheckAngle(new[] {angleOne}))
            {
                _sideC = Math.Sqrt(Math.Pow(_sideA, 2) + Math.Pow(_sideB, 2) - 2*_sideA*_sideB*Math.Cos(ToRadian(angleOne)));
            }
            else
            {
                throw new ArgumentException("Invalid angle!");
            }
        }

        public Triangle(OneSideAndTwoAngles obj)
        {
            _sideC = obj.Side;
            if (!CheckSide(_sideC))
            {
                throw new ArgumentException("Invalid side!");
            }

            var angleOne = obj.AngleOne;
            var angleTwo = obj.AngleTwo;
            if (CheckAngle(new[] {angleOne, angleTwo}))
            {
                var angleThree = 180 - (angleOne + angleTwo);
                _sideA = _sideC*Math.Sin(ToRadian(angleOne))/Math.Sin(ToRadian(angleThree));
                _sideB = _sideC*Math.Sin(ToRadian(angleTwo))/Math.Sin(ToRadian(angleThree));
            }
            else
            {
                throw new ArgumentException("Invalid angle!");
            }
        }

        private bool CheckAngle(double[] angles)
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

        public bool CheckSide(double side)
        {
            return side > 0;
        }

        private double ToRadian(double degrees) => degrees*Math.PI/180;

        public double CalculateArea()
        {
            var p = (_sideA + _sideB + _sideC)/2;
            return Math.Sqrt(p*(p - _sideA)*(p - _sideB)*(p - _sideC));
        }

        static void Main(string[] args)
        {
        }
    }
}