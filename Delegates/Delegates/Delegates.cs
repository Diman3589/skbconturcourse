using System;

namespace Delegates
{
    public class TransactionProcessor<TOperation, TRequest>
    {
        public TransactionProcessor(Func<TRequest, bool> checkDel,
            Func<TRequest, TOperation> registerDel, Action<TOperation> saveDel)
        {
            Check = checkDel;
            Register = registerDel;
            Save = saveDel;
        }

        public TOperation Process(TRequest request)
        {
            if (!Check(request))
                throw new ArgumentException();
            var result = Register(request);
            Save(result);
            return result;
        }

        protected Func<TRequest, bool> Check;

        protected Func<TRequest, TOperation> Register;

        protected Action<TOperation> Save;
    }
}
