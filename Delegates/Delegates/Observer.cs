using System;
using System.Collections.Generic;

namespace Delegates
{
    public class Observer
    {
        public Action<DataModel, Dictionary<string, int>> OnInsertRowHalder;
        public Action<DataModel, Dictionary<string, int>> OnInsertColumnHandler;
        public Action<DataModel, Dictionary<string, int>> OnInsertDataHandler;
        public Action<DataModel, Dictionary<string, int>> OnGetDataHandler;

        public Observer(DataModel dataModel, 
            Action<DataModel, Dictionary<string, int>> insertRowAction,
            Action<DataModel, Dictionary<string, int>> insertColumnAction,
            Action<DataModel, Dictionary<string, int>> insertDataAction,
            Action<DataModel, Dictionary<string, int>> getDataAction)
        {
            OnInsertRowHalder = insertRowAction;
            OnInsertColumnHandler = insertColumnAction;
            OnInsertDataHandler = insertDataAction;
            OnGetDataHandler = getDataAction;
            dataModel.AttachObserver(this);
        }
    }
}