using System;
using System.Collections.Generic;
using System.Linq;

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

            List<Guid> guids;
            if (!_invertedDbStorage.TryGetValue(typeof(T), out guids))
            {
                _invertedDbStorage.Add(typeof(T), new List<Guid>());
            }
            _invertedDbStorage[typeof(T)].Add(guid);

            return default(T);
        }

        public Dictionary<Guid, object> GetPairs<T>()
            where T : new()
        {
            if (!_invertedDbStorage.ContainsKey(typeof(T))) return null;

            var values = _invertedDbStorage[typeof(T)];

            return values.ToDictionary(guid => guid, guid => _dbStorage[guid]);
        }

        public object GetObject(Guid guid) => _dbStorage.ContainsKey(guid) ? _dbStorage[guid] : null;
    }
}