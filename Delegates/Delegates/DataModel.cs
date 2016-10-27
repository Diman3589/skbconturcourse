using System;
using System.Collections.Generic;

namespace Delegates
{
    public class DataModel
    {
        private List<Observer> _observers;

        private int[,] _table;

        public DataModel()
        {
            _observers = new List<Observer>();
            _table = new int[0, 0];
        }

        public void AttachObserver(Observer obj) => _observers.Add(obj);

        public void UnattacheObjserver(Observer obj) => _observers.Remove(obj);

        private void Notify() => _observers.ForEach(x => x.Update());

        private bool CheckIndex(int index, int dimensionIndex)
        {
            var length = _table.GetLength(dimensionIndex);
            return index > length - 1;
        }

        private void CopyTables(int[,] newTable)
        {
            for (var i = 0; i < _table.GetLength(0); i++)
            {
                for (var j = 0; j < _table.GetLength(1); j++)
                {
                    newTable[i, j] = _table[i, j];
                }
            }
            _table = newTable;
        }


        private bool CheckCell(int rowIndex, int columnIndex) =>
            rowIndex <= _table.GetLength(0) && columnIndex <= _table.GetLength(1);

        public void Put(int row, int column, int value)
        {
            if (!CheckCell(row, column))
            {
                throw new ArgumentException("Cell not exist!");
            }
            _table[row, column] = value;
            Notify();
        }

        public void InsertRow(int rowIndex)
        {
            if (!CheckIndex(rowIndex, 0))
            {
                throw new ArgumentException("Incorrect row index!");
            }
            var dimensionIndex = _table.GetLength(1);
            var newTable = new int[rowIndex + 1, dimensionIndex];
            CopyTables(newTable);
            Notify();
        }

        public void InsertColumn(int columnIndex)
        {
            if (!CheckIndex(columnIndex, 1))
            {
                throw new ArgumentException("Incorrect column index!");
            }
            var dimensionIndex = _table.GetLength(0);
            var newTable = new int[dimensionIndex, columnIndex + 1];
            CopyTables(newTable);
            Notify();
        }

        public int Get(int row, int column)
        {
            if (!CheckCell(row, column))
            {
                throw new ArgumentException("Cell not exist!");
            }
            return _table[row, column];
        }
    }
}