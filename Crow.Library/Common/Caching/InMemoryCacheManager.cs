using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Caching;
using System.Collections.Concurrent;

namespace Crow.Library.Common.Caching
{
    public class InMemoryCacheManager : ICacheManager
    {
        private static readonly ConcurrentDictionary<string, object> _Cache = new ConcurrentDictionary<string, object>();

        private ConcurrentDictionary<string, object> m_Cache
        {
            get { return _Cache; }
        }

        public void AddOrUpdateCache(string key, object value)
        {
            m_Cache.AddOrUpdate(key, value, (k, o) => value);
        }

        public void AddOrUpdateCache(string key, object value, TimeSpan expirationTime)
        {
            AddOrUpdateCache(key, value);
        }

        public TReturnValue Get<TReturnValue>(string key)
        {
            object value;
            bool hasValue = m_Cache.TryGetValue(key, out value);
            if (hasValue)
            {
                return (TReturnValue)value;
            }
            return default(TReturnValue);
        }

        public TReturnValue GetOrAdd<TReturnValue>(string key, Func<TReturnValue> func)
        {
            return (TReturnValue)m_Cache.GetOrAdd(key, func());
        }

        public TReturnValue GetOrAdd<TReturnValue>(string key, Func<TReturnValue> func, TimeSpan expirationTime)
        {
            return GetOrAdd(key, func);
        }

        public TReturnValue GetOrDefault<TReturnValue>(string key, TReturnValue @default)
        {
            object value;
            bool hasValue = m_Cache.TryGetValue(key, out value);
            if (!hasValue)
            {
                return @default;
            }
            return (TReturnValue)value;
        }

        public void InvalidateCache()
        {
            m_Cache.Clear();
        }

        public object RemoveFromCache(string key)
        {
            object value;
            m_Cache.TryRemove(key, out value);
            return value;
        }
    }
}
