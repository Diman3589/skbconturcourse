using System;
using System.Collections.Generic;

namespace Delegates
{
    public class DataModel
    {
        private readonly List<Observer> _observers;

        public int[,] Table { private set; get; }

        public DataModel()
        {
            _observers = new List<Observer>();
            Table = new int[0, 0];
        }

        private void Notify(string changes)
        {
            foreach (var observer in _observers)
            {
                switch (changes)
                {
                    case "Put":
                        observer.OnInsertDataHandler(this);
                        break;
                    case "Insert row":
                        observer.OnInsertRowHalder(this);
                        break;
                    case "Insert column":
                        observer.OnInsertColumnHandler(this);
                        break;
                    case "Get":
                        observer.OnGetDataHandler(this);
                        break;
                }
            }
        }

        public void AttachObserver(Observer obj) => _observers.Add(obj);

        public void UnattacheObjserver(Observer obj) => _observers.Remove(obj);

        private bool CheckIndex(int index, int dimensionIndex)
        {
            var length = Table.GetLength(dimensionIndex);
            return index > length - 1;
        }

        private void CopyTables(int[,] newTable)
        {
            for (var i = 0; i < Table.GetLength(0); i++)
            {
                for (var j = 0; j < Table.GetLength(1); j++)
                {
                    newTable[i, j] = Table[i, j];
                }
            }
            Table = newTable;
        }


        private bool CheckCell(int rowIndex, int columnIndex) =>
            rowIndex <= Table.GetLength(0) && columnIndex <= Table.GetLength(1);

        public void Put(int row, int column, int value)
        {
            if (!CheckCell(row, column))
            {
                throw new ArgumentException("Cell not exist!");
            }
            Table[row, column] = value;
            Notify("Put");
        }

        public void InsertRow(int rowIndex)
        {
            if (!CheckIndex(rowIndex, 0))
            {
                throw new ArgumentException("Incorrect row index!");
            }
            var dimensionIndex = Table.GetLength(1);
            var newTable = new int[rowIndex + 1, dimensionIndex];
            CopyTables(newTable);
            Notify("Insert row");
        }

        public void InsertColumn(int columnIndex)
        {
            if (!CheckIndex(columnIndex, 1))
            {
                throw new ArgumentException("Incorrect column index!");
            }
            var dimensionIndex = Table.GetLength(0);
            var newTable = new int[dimensionIndex, columnIndex + 1];
            CopyTables(newTable);
            Notify("Insert column");
        }

        public int Get(int row, int column)
        {
            if (!CheckCell(row, column))
            {
                throw new ArgumentException("Cell not exist!");
            }
            Notify("Get");
            return Table[row, column];
        }
    }
}