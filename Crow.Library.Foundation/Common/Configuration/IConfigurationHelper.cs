using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Common.Configuration
{
    /// <summary>
    /// Represents the configuration helpers.
    /// </summary>
    public interface IConfigurationHelper
    {
        /// <summary>
        /// Gets the configuration with the key spesified.
        /// </summary>
        TConfigurationValue Get<TConfigurationValue>(string key);
        /// <summary>
        /// Gets the configuration with the key spesified.
        /// </summary>
        TConfigurationValue Get<TConfigurationValue>(string key, TConfigurationValue defaultValue);

        /// <summary>
        /// Gets the configuration with the key spesified.
        /// </summary>
        string Get(string key);

        /// <summary>
        /// Gets the configuration with the key spesified.
        /// </summary>
        string Get(string key, string defaultValue);

        /// <summary>
        /// Sets the configuration value with the key spesified.
        /// </summary>
        void Set(string key, object value);
    }
}
