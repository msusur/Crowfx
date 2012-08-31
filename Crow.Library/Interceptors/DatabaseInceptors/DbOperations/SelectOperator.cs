using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using Castle.DynamicProxy;
using Crow.Library.DatabaseLayer;
using Crow.Library.DatabaseLayer.DataAttributes;
using Crow.Library.Foundation.Exceptions;
using Crow.Library.RepositoryStorage;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    internal class SelectOperator : DbOperationBase
    {
        private static readonly MethodInfo _genericQueryMethod = typeof(Database).GetMethod("QueryGeneric");

        public override void Execute(IInvocation invocation, DbAttributeBase attribute)
        {
            string key = string.Format("{0}.{1}", invocation.TargetType.GetInterfaces()[0].Name, invocation.Method.Name);
            bool hasKey = QueryStore.Commands.ContainsKey(key);
            if (!hasKey)
            {
                string interfaceName = invocation.TargetType.GetInterfaces()[0].Name;
                string methodName = invocation.Method.Name;
                throw new QueryForMethodNotFoundException(interfaceName, methodName);
            }
            QueryCommand command = QueryStore.Commands[key];
            string query = command.Query;

            Database db = DatabaseHelper.GetDatabase();

            Type genericParameter = invocation.Method.ReturnType;

            if (genericParameter.IsGenericType)
            {
                genericParameter = genericParameter.GetGenericArguments()[0];
            }

            MethodInfo info = _genericQueryMethod.MakeGenericMethod(genericParameter);
            Debug.Assert(info != null);
            dynamic expando = new DynamicArgumentHolder(invocation);
            db.Connection.Open();

            IEnumerable xx = (IEnumerable)info.Invoke(db, new object[] { query, new object[] { expando } });
            if (typeof(IList).IsAssignableFrom(invocation.Method.ReturnType))
            {
                IList collectionInstance = (IList)Activator.CreateInstance(invocation.Method.ReturnType);
                foreach (var item in xx)
                {
                    collectionInstance.Add(item);
                }
                invocation.ReturnValue = collectionInstance;
            }
            else
            {
                foreach (var item in xx)
                {
                    invocation.ReturnValue = item;
                    break;
                }
            }
        }
    }
}