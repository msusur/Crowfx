using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Common.Caching
{
    public interface ICacheManager
    {
        void AddOrUpdateCache(string key, object value);

        void AddOrUpdateCache(string key, object value, TimeSpan expirationTime);

        TReturnValue Get<TReturnValue>(string key);

        TReturnValue GetOrAdd<TReturnValue>(string key, Func<TReturnValue> func);

        TReturnValue GetOrAdd<TReturnValue>(string key, Func<TReturnValue> func, TimeSpan expirationTime);

        TReturnValue GetOrDefault<TReturnValue>(string key, TReturnValue @default);

        void InvalidateCache();

        object RemoveFromCache(string key);
    }
}
