using System;

namespace Delegates
{
    public class Observer
    {
        public DataModel _model;
        public Observer(Action updateAction, DataModel dataModel)
        {
            _model = dataModel;
            Update = updateAction;
            _model.AttachObserver(this);
        }

        public Action Update;
    }
}
