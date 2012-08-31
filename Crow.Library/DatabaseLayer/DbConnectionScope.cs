using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.DatabaseLayer
{
    /// <summary>
    /// Creates a new shared database connection.
    /// </summary>
    public class DbConnectionScope : IDisposable
    {
        private static readonly object _SyncRoot = new object();

        /// <summary>
        /// Starts a new connection thread-safe and allows every repository uses the same connection within same scope.
        /// </summary>
        public DbConnectionScope()
        {
            lock (_SyncRoot)
            {
                if (DatabaseHelper.CurrentConnection == null)
                {
                    DatabaseHelper.CurrentConnection = DatabaseHelper.CreateConnection();
                }
            }
        }

        /// <summary>
        /// Disposes the current connection.
        /// </summary>
        public void Dispose()
        {
            lock (_SyncRoot)
            {
                DatabaseHelper.CurrentConnection = null;
            }
        }
    }
}
