using System;
using System.Collections.Generic;

namespace Generics
{
    public class Storage
    {
        private Dictionary<Guid, object> _dbStorage;
        private Dictionary<object, List<Guid>> _invertedDbStorage;

        public Storage()
        {
            _dbStorage = new Dictionary<Guid, object>();
            _invertedDbStorage = new Dictionary<object, List<Guid>>();
        }

        public T AddObject<T>()
            where T : new()
        {
            var guid = Guid.NewGuid();

            _dbStorage.Add(guid, typeof(T));

            if (!_invertedDbStorage.ContainsKey(typeof(T)))
            {
                _invertedDbStorage.Add(typeof(T), new List<Guid> {guid});
            }
            else
            {
                _invertedDbStorage[typeof(T)].Add(guid);
            }

            return default(T);
        }

        public List<Dictionary<object, Guid>> GetKeyValuePair<T>()
        {
            var result = new List<Dictionary<object, Guid>>();
            if (!_invertedDbStorage.ContainsKey(typeof(T))) return null;
            var values = _invertedDbStorage[typeof(T)];
            foreach (var guid in values)
            {
                result.Add(new Dictionary<object, Guid> {{typeof(T), guid}});
            }
            return result;
        }

        public object GetObject(Guid guid) => _dbStorage.ContainsKey(guid) ? _dbStorage[guid] : null;
    }
}