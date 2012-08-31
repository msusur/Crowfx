using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common;
using Crow.Library.DatabaseLayer.ExpressionVisitors;

namespace Crow.Library.Common
{
    /// <summary>
    /// Represents the concrete QueryStore interface.
    /// </summary>
    public interface IQueryStore
    {
        /// <summary>
        /// Gets or sets the connection string of the default connection.
        /// </summary>
        ConnectionStringInformation ConnectionString{ get; set; }

        /// <summary>
        /// Returns an sql expression visitor for generating queries.
        /// </summary>
        /// <typeparam name="TRepository">Interface reference of repository.</typeparam>
        /// <typeparam name="TReturnType">Return type of the method that is referenced in the method of the interface.</typeparam>
        /// <param name="methodName">Name of the method that is going to be matched with the expression.</param>
        ISqlExpressionVisitor<TReturnType> Expression<TRepository, TReturnType>(string methodName);

        /// <summary>
        /// Adds a SQL command to the spesified method.
        /// </summary>
        /// <typeparam name="TRepository">Interface reference of the repository.</typeparam>
        /// <param name="methodName">Name of the method that is going to be matched with the sql query.</param>
        /// <param name="query">Query.</param>
        void AddCommand<TRepository>(string methodName, string query);
    }
}
