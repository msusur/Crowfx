using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.DatabaseLayer.DataAttributes;
using PetaPoco;
using Crow.Library.DatabaseLayer;
using System.Diagnostics.Contracts;
using System.Collections;
using Castle.DynamicProxy;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    internal class InsertOperator : DbOperationBase
    {
        public override void Execute(IInvocation invocation, DbAttributeBase attribute)
        {
            Contract.Requires(invocation.Arguments.Length > 0);
            Database db = DatabaseHelper.GetDatabase();
            db.Connection.Open();
            object result = null;

            object arg = null;
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                arg = invocation.Arguments[i];
                if (arg is IEnumerable)
                {
                    foreach (var argItem in (arg as IEnumerable))
                    {
                        result = db.Insert(argItem);
                    }
                }
                else
                {
                    result = db.Insert(arg);
                }

            }

            base.SetInvocationResult(invocation, result);
        }
    }
}