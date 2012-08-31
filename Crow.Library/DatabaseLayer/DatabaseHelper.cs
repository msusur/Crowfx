using System;
using System.Data.Common;
using Crow.Library.RepositoryStorage;
using Crow.Library.Common;

namespace Crow.Library.DatabaseLayer
{
    internal static class DatabaseHelper
    {
        internal static Database CurrentConnection
        {
            get { return CrowCore.Items.Get<Database>("CurrentConnection"); }
            set { CrowCore.Items.Add("CurrentConnection", value); }
        }

        public static Database GetDatabase()
        {
            if (CurrentConnection == null)
            {
                CurrentConnection = CreateConnection();
            }
            return CurrentConnection;
        }

        internal static Database CreateConnection()
        {
            QueryStore.ConnectionInformation.ConnectionString.ThrowIfNullOrEmpty();
            QueryStore.ConnectionInformation.Provider.ThrowIfNullOrEmpty();

            DbConnection connection = DbProviderFactories.GetFactory(QueryStore.ConnectionInformation.Provider).CreateConnection();
            connection.ConnectionString = QueryStore.ConnectionInformation.ConnectionString;
            Database db = new Database(connection);
            return db;
        }
    }
}