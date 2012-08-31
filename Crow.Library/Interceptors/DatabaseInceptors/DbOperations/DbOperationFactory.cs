using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.DatabaseLayer.DataAttributes;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    internal static class DbOperationFactory
    {
        private static readonly Dictionary<Type, DbOperationBase> _operator =
            new Dictionary<Type, DbOperationBase>
            {
                { typeof(DeleteAttribute) , new DeleteOperator()},
                { typeof(InsertAttribute) , new InsertOperator()},
                { typeof(UpdateAttribute) , new UpdateOperator()},
                { typeof(ScalarAttribute) , new ScalarOperator()},

            };

        internal static DbOperationBase GetOperator(DbAttributeBase dbAttribute)
        {
            DbOperationBase operationBase = null;

            bool hasValue = _operator.ContainsKey(dbAttribute.GetType());
            if (!hasValue)
            {
                operationBase = new SelectOperator();
            }
            else
            {
                operationBase = _operator[dbAttribute.GetType()];
            }

            return operationBase;
        }
    }
}