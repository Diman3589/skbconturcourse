using System;
using System.Collections.Generic;
using System.Linq;

namespace MysticSquare
{
    internal class Point
    {
        public int x;
        public int y;
        public int value { get; set; }
    }

    public class Game
    {
        internal List<Point> Square;

        public Game(params int[] squareElements)
        {
            var uniqueElems = squareElements.Distinct().Count() == squareElements.Length;
            if (!uniqueElems)
            {
                throw new ArgumentException("Elements is not unique");
            }

            var lenghtElems = squareElements.Length;
            int n;

            if (lenghtElems == 4 || lenghtElems == 9 || lenghtElems == 16)
            {
                n = Convert.ToInt32(Math.Sqrt(lenghtElems));
            }
            else
            {
                throw new ArgumentException("Incorrect params");
            }

            if (!CheckParams(squareElements))
            {
                throw new ArgumentException("Incorrect params");
            }

            Square = new List<Point>();
            for (var i = 0; i < n; ++i)
                for (var j = 0; j < n; ++j)
                {
                    var point = new Point
                    {
                        x = i,
                        y = j,
                        value = squareElements[i*n + j]
                    };
                    Square.Add(point);
                }
        }

        private static bool CheckParams(int[] array)
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

            var quantityOfZero = 0;
            foreach (var elem in array)
            {
                if (elem < 0)
                {
                    throw new ArgumentException("Incorrect params");
                }
                if (elem == 0)
                    quantityOfZero++;
                if (quantityOfZero > 1)
                    throw new ArgumentException("Incorrect params");
            }
            return true;
        }

        private bool CheckValue(int value)
        {
            if (value < 0)
                return false;

            var obj = Square.FirstOrDefault(elem => elem.value == value);
            if (obj == null)
                return false;
            return true;
        }

        public int GetValue(params int[] coordinates)
        {
            if (!CheckParams(coordinates))
                throw new ArgumentException("Incorrect coordinates");
            return Square.FirstOrDefault(elem => elem.x == coordinates[0] && elem.y == coordinates[1]).value;
        }

        public int[] GetLocation(int value)
        {
            if (!CheckValue(value))
                throw new ArgumentException("Incorrect value");
            var result = Square.FirstOrDefault(elem => elem.value == value);
            return new[] {result.x, result.y};
        }

        public void Shift(int value)
        {
            if (!CheckValue(value))
            {
                throw new ArgumentException("Incoorect value");
            }
            var currentObj = Square.FirstOrDefault(elem => elem.value == value);
            var zeroObj = Square.First(elem => elem.value == 0);

            var x = Math.Abs(currentObj.x - zeroObj.x);
            var y = Math.Abs(currentObj.y - zeroObj.y);
            if (x + y != 1)
            {
                throw new ArgumentException("Incorrect shift");
            }
            var newIndex = Square.IndexOf(currentObj);
            var oldIndex = Square.IndexOf(zeroObj);
            if (newIndex == -1 || oldIndex == -1)
            {
                throw new ArgumentException("Incorrect indexes of elements");
            }
            var tmp = Square[oldIndex].value;

            Square[oldIndex].value = Square[newIndex].value;
            Square[newIndex].value = tmp;
        }
    }
}