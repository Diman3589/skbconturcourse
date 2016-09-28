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
        private readonly double _angleOne;
        private readonly double _angleTwo;
        private double _angleThree;

        public Triangle(ThreeSides obj)
        {
            _sideA = obj.SideA;
            _sideB = obj.SideB;
            _sideC = obj.SideC;
            if (!CheckSide(ref _sideA) || !CheckSide(ref _sideB) || !CheckSide(ref _sideC))
            {
                throw new ArgumentException("Not valid side!");
            }
        }

        public Triangle(TwoSidesAndAngle obj)
        {
            _sideA = obj.SideA;
            _sideB = obj.SideB;
            if (!CheckSide(ref _sideA) || !CheckSide(ref _sideB))
            {
                throw new ArgumentException("Not valid side!");
            }

            _angleOne = obj.Angle;
            if (CheckAngle(new[] {_angleOne}))
            {
                _sideC = Math.Sqrt(Math.Pow(_sideA, 2) + Math.Pow(_sideB, 2) - 2*_sideA*_sideB*Math.Cos(ToRadian(_angleOne)));
            }
            else
            {
                throw new ArgumentException("Not valid angle!");
            }
        }

        public Triangle(OneSideAndTwoAngles obj)
        {
            _sideC = obj.Side;
            if (!CheckSide(ref _sideC))
            {
                throw new ArgumentException("Not valid side!");
            }

            _angleOne = obj.AngleOne;
            _angleTwo = obj.AngleTwo;
            if (CheckAngle(new[] {_angleOne, _angleTwo}))
            {
                _angleThree = 180 - (_angleOne + _angleTwo);
                _sideA = _sideC*Math.Sin(ToRadian(_angleOne))/Math.Sin(ToRadian(_angleThree));
                _sideB = _sideC*Math.Sin(ToRadian(_angleTwo))/Math.Sin(ToRadian(_angleThree));
            }
            else
            {
                throw new ArgumentException("Not valid angle!");
            }
        }

        private bool CheckAngle(double[] angles)
        {
            var sumAngles = 0.0;

            foreach (var angle in angles)
            {
                if (angle > 0 && angle < 180) sumAngles += angle;
                else return false;
            }
            return sumAngles < 180;
        }

        public bool CheckSide(ref double side)
        {
            return side > 0;
        }

        private double ToRadian(double degrees)
        {
            return degrees*Math.PI/180;
        }

        public double CalculateArea()
        {
            var p = (_sideA + _sideB + _sideC)/2;
            var area = Math.Sqrt(p*(p - _sideA)*(p - _sideB)*(p - _sideC));
            return area;
        }

        static void Main(string[] args)
        {
        }
    }
}