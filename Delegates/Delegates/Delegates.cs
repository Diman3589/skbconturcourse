using System;

namespace Delegates
{
    public class TransactionProcessor<TOperation, TRequest>
    {
        private readonly Func<TRequest, TOperation> _registerDelegate;
        private readonly Action<TOperation> _saveDelegate;
        private readonly Func<TRequest, bool> _checkDelegate;

        public TransactionProcessor(Func<TRequest, bool> checkDel,
            Func<TRequest, TOperation> registerDel, Action<TOperation> saveDel)
        {
            _registerDelegate = registerDel;
            _saveDelegate = saveDel;
            _checkDelegate = checkDel;
        }

        public TOperation Process(TRequest request)
        {
            if (!_checkDelegate(request))
                throw new ArgumentException();
            var result = _registerDelegate(request);
            _saveDelegate(result);
            return result;
        }

        protected bool Check(TRequest request)
        {
            return _checkDelegate(request);
        }

        protected TOperation Register(TRequest request)
        {
            return _registerDelegate(request);
        }

        protected void Save(TOperation transaction)
        {
            _saveDelegate(transaction);
        }
    }
}
