using System;
using System.Linq;

namespace MysticSquare
{
    public struct Point
    {
        public int x;
        public int y;
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
            double result = Math.Sqrt(lenghtElems);
            bool isSquare = result%1 == 0;

            if (!isSquare)
            {
                throw new ArgumentException("Incorrect square");
            }

            var n = Convert.ToInt32(Math.Sqrt(lenghtElems));

            if (!CheckParams(squareElements))
            {
                throw new ArgumentException("Incorrect params");
            }

            _arrayPoints = new Point[squareElements.Length];
            _arrayValues = new int[n, n];

            for (var i = 0; i < n; ++i)
                for (var j = 0; j < n; ++j)
                {
                    var point = new Point
                    {
                        x = i,
                        y = j,
                    };

                    var squareElement = squareElements[i*n + j];
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

            var x = Math.Abs(currentObj.x - zeroObj.x);
            var y = Math.Abs(currentObj.y - zeroObj.y);

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

            _arrayValues[currentObj.x, currentObj.y] = 0;
            _arrayValues[zeroObj.x, zeroObj.y] = value;

            _arrayPoints[value].x = zeroObj.x;
            _arrayPoints[value].y = zeroObj.y;
            _arrayPoints[0].x = currentObj.x;
            _arrayPoints[0].y = currentObj.y;

            return this;
        }

        public Game(Game obj)
        {
            _arrayPoints = (Point[]) obj._arrayPoints.Clone();
            _arrayValues = (int[,]) obj._arrayValues.Clone();
        }
    }
}