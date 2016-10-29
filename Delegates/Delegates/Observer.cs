using System;

namespace Delegates
{
    public class Observer
    {
        public Action<DataModel, int> OnInsertRowHalder;
        public Action<DataModel, int> OnInsertColumnHandler;
        public Action<DataModel, int, int> OnInsertDataHandler;
        public Action<DataModel, int, int> OnGetDataHandler;

        public Observer(DataModel dataModel, 
            Action<DataModel, int> insertRowAction,
            Action<DataModel, int> insertColumnAction,
            Action<DataModel, int, int> insertDataAction,
            Action<DataModel, int, int> getDataAction)
        {
            OnInsertRowHalder = insertRowAction;
            OnInsertColumnHandler = insertColumnAction;
            OnInsertDataHandler = insertDataAction;
            OnGetDataHandler = getDataAction;
            dataModel.AttachObserver(this);
        }
    }
}