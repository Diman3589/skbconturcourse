using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegates
{
    public class DataModel
    {
        private readonly List<Observer> _observers;

        public List<List<int>> Table { get; }

        public DataModel()
        {
            _observers = new List<Observer>();
            Table = new List<List<int>>();
        }

        private void Notify(string changes, int rowIndex = 0, int colIndex = 0)
        {
            foreach (var observer in _observers)
            {
                switch (changes)
                {
                    case "Put":
                        observer.OnInsertDataHandler(this, rowIndex, colIndex);
                        break;
                    case "Insert row":
                        observer.OnInsertRowHalder(this, rowIndex);
                        break;
                    case "Insert column":
                        observer.OnInsertColumnHandler(this, colIndex);
                        break;
                    case "Get":
                        observer.OnGetDataHandler(this, rowIndex, colIndex);
                        break;
                }
            }
        }

        public void AttachObserver(Observer obj) => _observers.Add(obj);

        public void UnattacheObjserver(Observer obj) => _observers.Remove(obj);

        private bool CheckIndex(int index, bool rowOrColumn)
        {
            if (!rowOrColumn) return Table[0].Count >= index;
            return Table.Count >= index;
        }

        private bool CheckCell(int rowIndex, int columnIndex)
        {
            if (Table.Count < rowIndex && rowIndex < 0)
            {
                return false;
            }
            return Table[0].Count > columnIndex;
        }

        public void Put(int row, int column, int value)
        {
            if (!CheckCell(row, column))
            {
                throw new ArgumentException("Cell not exist!");
            }
            Table[row][column] = value;
            Notify("Put", rowIndex: row, colIndex: column);
        }

        public void InsertRow(int rowIndex)
        {
            if (Table.Count > 0)
            {
                if (!CheckIndex(rowIndex, true))
                    throw new ArgumentException("Incorrect row index!");
                Table.Insert(rowIndex, new List<int>(new int[Table[0].Count]));
            }
            else
            {
                Table.Add(new List<int>());
            }

            Notify("Insert row", rowIndex: rowIndex);
        }

        public void InsertColumn(int columnIndex)
        {
            if (Table.Count > 0)
            {
                if (!CheckIndex(columnIndex, false))
                    throw new ArgumentException("Incorrect column index!");
            }
            else
            {
                Table.Add(new List<int>());
            }
            foreach (var row in Table)
            {
                row.Insert(columnIndex, 0);
            }
            Notify("Insert column", colIndex: columnIndex);
        }

        public int Get(int row, int column)
        {
            if (!CheckCell(row, column))
            {
                throw new ArgumentException("Cell not exist!");
            }
            Notify("Get", rowIndex: row, colIndex: column);
            return Table[row][column];
        }
    }
}