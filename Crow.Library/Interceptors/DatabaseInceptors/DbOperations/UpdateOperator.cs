using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.DatabaseLayer.DataAttributes;
using PetaPoco;
using Crow.Library.DatabaseLayer;
using System.Collections;
using Castle.DynamicProxy;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    internal class UpdateOperator : DbOperationBase
    {
        public override void Execute(IInvocation invocation, DbAttributeBase attribute)
        {
            Database db = DatabaseHelper.GetDatabase();
            db.Connection.Open();
            int result = 0;
            object arg = null;
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                arg = invocation.Arguments[i];
                if (arg is IEnumerable)
                {
                    foreach (var argItem in (arg as IEnumerable))
                    {
                        result = db.Update(argItem);
                    }
                }
                else
                {
                    result = db.Update(arg);
                }

            }

            base.SetInvocationResult(invocation, result);
        }
    }
}
