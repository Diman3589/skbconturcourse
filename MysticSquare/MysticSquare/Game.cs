using System;
using System.Linq;

namespace MysticSquare
{
    public struct Point
    {
        public int X;
        public int Y;
    }

    public class Game
    {
        protected Point[] _arrayPoints;
        protected int[,] _arrayValues;

        public int this[int x, int y] => _arrayValues[x, y];

        public Game(params int[] squareElements)
        {
            var uniqueElems = squareElements.Distinct().Count() == squareElements.Length;
            if (!uniqueElems)
            {
                throw new ArgumentException("Elements is not unique");
            }

            var lenghtElems = squareElements.Length;
            var result = Math.Sqrt(lenghtElems);
            var isSquare = result%1 == 0;

            if (!isSquare)
            {
                throw new ArgumentException("Incorrect square");
            }

            var sizeSquare = Convert.ToInt32(Math.Sqrt(lenghtElems));

            if (!CheckParams(squareElements))
            {
                throw new ArgumentException("Incorrect params");
            }

            _arrayPoints = new Point[squareElements.Length];
            _arrayValues = new int[sizeSquare, sizeSquare];

            for (var i = 0; i < sizeSquare; ++i)
                for (var j = 0; j < sizeSquare; ++j)
                {
                    var point = new Point
                    {
                        X = i,
                        Y = j,
                    };

                    var squareElement = squareElements[i*sizeSquare + j];
                    _arrayValues[i, j] = squareElement;
                    try
                    {
                        _arrayPoints[squareElement] = point;
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException("Incorrect elements");
                    }
                }
        }

        protected bool CheckParams(int[] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException("No elements");
            }

            if (array.Length == 2)
            {
                if (array.Any(elem => elem < 0))
                {
                    throw new ArgumentException("Incorrect coordinates");
                }
            }

            var negativeElement = array.SingleOrDefault(x => x < 0);
            if (negativeElement != 0)
            {
                throw new ArgumentException("Incorrect params");
            }

            var zeroElement = array.SingleOrDefault(x => x == 0);
            if (zeroElement > 1)
            {
                throw new ArgumentException("Incorrect params");
            }

            return true;
        }

        protected bool CheckValue(int value)
        {
            if (value < 0)
                return false;

            if (value >= _arrayPoints.Length)
                return false;
            return true;
        }

        public Point GetLocation(int value)
        {
            if (!CheckValue(value))
                throw new ArgumentException("Incorrect value");
            return _arrayPoints[value];
        }

        protected bool CheckShift(int value)
        {
            var currentObj = _arrayPoints[value];
            var zeroObj = _arrayPoints[0];

            var x = Math.Abs(currentObj.X - zeroObj.X);
            var y = Math.Abs(currentObj.Y - zeroObj.Y);

            return x + y == 1;
        }

        public virtual Game Shift(int value)
        {
            if (!CheckValue(value))
            {
                throw new ArgumentException("Incorrect value");
            }
            if (!CheckShift(value))
            {
                throw new ArgumentException("Incorrect shift");
            }
            var currentObj = _arrayPoints[value];
            var zeroObj = _arrayPoints[0];

            _arrayValues[currentObj.X, currentObj.Y] = 0;
            _arrayValues[zeroObj.X, zeroObj.Y] = value;

            _arrayPoints[value].X = zeroObj.X;
            _arrayPoints[value].Y = zeroObj.Y;
            _arrayPoints[0].X = currentObj.X;
            _arrayPoints[0].Y = currentObj.Y;

            return this;
        }

        public Game(Game obj)
        {
            _arrayPoints = (Point[]) obj._arrayPoints.Clone();
            _arrayValues = (int[,]) obj._arrayValues.Clone();
        }
    }
}