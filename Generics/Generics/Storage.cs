using System;
using System.Collections.Generic;

namespace Generics
{
    public class Storage
    {
        private Dictionary<Guid, object> _dbStorage;
        private Dictionary<Type, List<Guid>> _invertedDbStorage;

        public Storage()
        {
            _dbStorage = new Dictionary<Guid, object>();
            _invertedDbStorage = new Dictionary<Type, List<Guid>>();
        }

        public T AddObject<T>()
            where T : new()
        {
            var guid = Guid.NewGuid();
            var obj = new T();

            _dbStorage.Add(guid, obj);

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

        public List<KeyValuePair<Type, Guid>> GetKeyValuePair<T>()
            where T : new()
        {
            var result = new List<KeyValuePair<Type, Guid>>();
            if (!_invertedDbStorage.ContainsKey(typeof(T))) return null;

            var values = _invertedDbStorage[typeof(T)];
            foreach (var guid in values)
            {
                result.Add(new KeyValuePair<Type, Guid> (typeof(T), guid));
            }
            return result;
        }

        public object GetObject(Guid guid) => _dbStorage.ContainsKey(guid) ? _dbStorage[guid] : null;
    }

}