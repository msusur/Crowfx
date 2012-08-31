using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Configuration;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;
using Crow.Library.Foundation.Conversion;

namespace Crow.Library.Common.Configuration
{
    internal class ApplicationConfigurationHelper : IConfigurationHelper
    {
        private NameValueCollection Configuration
        {
            get { return ConfigurationManager.AppSettings; }
        }
        public TConfigurationValue Get<TConfigurationValue>(string key)
        {
            return Get<TConfigurationValue>(key, default(TConfigurationValue));
        }

        public TConfigurationValue Get<TConfigurationValue>(string key, TConfigurationValue defaultValue)
        {
            return ConversionHelper.ChangeType(Configuration[key], defaultValue);
        }

        public string Get(string key)
        {
            return Get(key, string.Empty);
        }

        public string Get(string key, string defaultValue)
        {
            object value = Configuration[key];
            return value != null ? value.ToString() : defaultValue;
        }

        public void Set(string key, object value)
        {
            //TODO: settings.
        }
    }
}
