using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.InjectionContainer;
using Crow.Library.Foundation.Common.Configuration;

namespace Crow.Library.Foundation.Common
{
    /// <summary>
    /// Represents the connection string definition for a connection
    /// </summary>
    public sealed class ConnectionStringInformation
    {
        /// <summary>
        /// Connection string for creating a new connection to the target database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Type of the provider for creating providers.
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// Initializes a new instance of connection string information with connectionstring and provider.
        /// </summary>
        public ConnectionStringInformation(string connectionString, string provider)
        {
            this.ConnectionString = connectionString;
            this.Provider = provider;
        }

        /// <summary>
        /// Initializes a new instance of connection string information with connectionstring and default provider as "System.Data.SqlClient".
        /// </summary>
        public ConnectionStringInformation(string connectionString)
            : this(connectionString, "System.Data.SqlClient")
        { }

        protected ConnectionStringInformation()
        {

        }

        /// <summary>
        /// Creates a new instance of ConnectionStringInformation from the Application Configuration.
        /// </summary>
        public static ConnectionStringInformation GetFromConfiguration()
        {
            //IConfigurationHelper configHelper = CrowCore.Container.Resolve<IConfigurationHelper>();
            string connectionString = ""; //configHelper.Get(Strings.Configuration.ConnectionString);
            string provider = "";//configHelper.Get(Strings.Configuration.Provider);
            return new ConnectionStringInformation(connectionString, provider);
        }

        /// <summary>
        /// Empty instance.
        /// </summary>
        public static readonly ConnectionStringInformation Empty = new ConnectionStringInformation();
    }
}
