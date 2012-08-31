using System.Linq;
using Crow.Library.DatabaseLayer;
using Crow.Library.DatabaseLayer.DataAttributes;
using PetaPoco;
using Castle.DynamicProxy;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    internal class DeleteOperator : DbOperationBase
    {
        public override void Execute(IInvocation invocation, DbAttributeBase attribute)
        {
            Database db = DatabaseHelper.GetDatabase();
            db.Connection.Open();
            int result = db.Delete(invocation.Arguments.FirstOrDefault());
            base.SetInvocationResult(invocation, result);
        }
    }
}
