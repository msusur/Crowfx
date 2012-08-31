using System;
using System.Collections.Generic;
using Crow.Library.Foundation.Common.Configuration;

namespace Crow.Library.Common.Configuration
{
    internal class InMemoryConfigurationHelper : IConfigurationHelper
    {
        private static Lazy<Dictionary<string, object>> m_LazyDictionary = new Lazy<Dictionary<string, object>>(
            () => new Dictionary<string, object>(), true);

        public TConfigurationValue Get<TConfigurationValue>(string key)
        {
            return Get<TConfigurationValue>(key, default(TConfigurationValue));
        }
        public string Get(string key)
        {
            return Get<string>(key);
        }

        public void Set(string key, object value)
        {
            if (m_LazyDictionary.Value.ContainsKey(key))
            {
                m_LazyDictionary.Value[key] = value;
            }
            else
            {
                m_LazyDictionary.Value.Add(key, value);
            }
        }


        public TConfigurationValue Get<TConfigurationValue>(string key, TConfigurationValue defaultValue)
        {
            if (m_LazyDictionary.Value.ContainsKey(key))
            {
                return (TConfigurationValue)m_LazyDictionary.Value[key];
            }
            return default(TConfigurationValue);
        }

        public string Get(string key, string defaultValue)
        {
           return Get<string>(key, defaultValue);
        }
    }
}