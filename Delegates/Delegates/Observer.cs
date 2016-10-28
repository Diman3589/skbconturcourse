using System;

namespace Delegates
{
    public class Observer
    {
        public Action<DataModel> OnInsertRowHalder;
        public Action<DataModel> OnInsertColumnHandler;
        public Action<DataModel> OnInsertDataHandler;
        public Action<DataModel> OnGetDataHandler;

        public Observer(DataModel dataModel, Action<DataModel> insertRowAction,
            Action<DataModel> insertColumnAction, Action<DataModel> insertDataAction,
            Action<DataModel> getDataAction)
        {
            OnInsertRowHalder = insertRowAction;
            OnInsertColumnHandler = insertColumnAction;
            OnInsertDataHandler = insertDataAction;
            OnGetDataHandler = getDataAction;
            dataModel.AttachObserver(this);
        }
    }
}