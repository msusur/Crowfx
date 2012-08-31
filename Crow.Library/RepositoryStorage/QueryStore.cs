using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using Crow.Library.Common;
using Crow.Library.DatabaseLayer.ExpressionVisitors;
using Crow.Library.DatabaseLayer;
using Crow.Library.Foundation.Common;

namespace Crow.Library.RepositoryStorage
{
    internal static class QueryStore
    {
        private static readonly Lazy<Dictionary<string, QueryCommand>> s_CommandsLazy =
            new Lazy<Dictionary<string, QueryCommand>>(() => new Dictionary<string, QueryCommand>(), true);

        private static ConnectionStringInformation _connectionString;

        public static ConnectionStringInformation ConnectionInformation
        {
            get
            {
                _connectionString = _connectionString ?? ConnectionStringInformation.Empty;
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        public static Dictionary<string, QueryCommand> Commands
        {
            get { return s_CommandsLazy.Value; }
        }
        public delegate TReturn OperationDelegate<TReturn>(dynamic d);

        public static SqlExpressionVisitor<TReturnType> Expression<TRepository, TReturnType>(string methodName)
        {
            var visitor = ExpressionVisitorFactory.Create<TReturnType>();
            visitor.MethodName = methodName;
            visitor.RepositoryType = typeof(TRepository);
            return visitor;
        }

        public static void AddCommand<TRepository>(string methodName, string query)
        {
            AddCommand(typeof(TRepository), methodName, query);
        }

        internal static void AddCommand(Type repositoryType, string methodName, string query)
        {
            string name = repositoryType.Name;
            if (!repositoryType.IsInterface)
            {
                throw new ArgumentException(string.Format("Type : '{0}' is not an interface.", name));
            }
            if (repositoryType.GetMethod(methodName) == null)
            {
                throw new ArgumentException(string.Format("Method: '{0}' is not a member of interface '{1}'.", methodName, name));
            }
            name = string.Format("{0}.{1}", name, methodName);
            AddCommand(name, query);
        }

        private static void AddCommand(string key, string query)
        {
            QueryStore.Commands.Add(key, new QueryCommand(query));
        }
    }


    internal class QueryStoreWrapper : IQueryStore
    {
        public ConnectionStringInformation ConnectionString
        {
            get { return QueryStore.ConnectionInformation; }
            set { QueryStore.ConnectionInformation = value; }
        }

        internal QueryStoreWrapper()
        {
        }

        public ISqlExpressionVisitor<TReturnType> Expression<TRepository, TReturnType>(string methodName)
        {
            return new ExpressionVisitor<TReturnType>(QueryStore.Expression<TRepository, TReturnType>(methodName));
        }

        public void AddCommand<TRepository>(string methodName, string query)
        {
            AddCommand<TRepository>(methodName, query);
        }
    }
}