using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Crow.Library.DatabaseLayer.DataAttributes;
using Crow.Library.DatabaseLayer;
using Crow.Library.RepositoryStorage;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    internal class ScalarOperator : DbOperationBase
    {
        public override void Execute(IInvocation invocation, DbAttributeBase attribute)
        {
            Database db = DatabaseHelper.GetDatabase();
            QueryCommand command = QueryStore.Commands[string.Format("{0}.{1}", invocation.TargetType.GetInterfaces()[0].Name, invocation.Method.Name)];
            db.Connection.Open();
            int result = db.Execute(command.Query);
            base.SetInvocationResult(invocation, result);
        }
    }
}
