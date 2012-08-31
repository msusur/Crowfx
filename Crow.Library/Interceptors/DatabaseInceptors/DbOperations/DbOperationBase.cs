using Crow.Library.DatabaseLayer.DataAttributes;
using Castle.DynamicProxy;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    internal abstract class DbOperationBase
    {
        public abstract void Execute(IInvocation invocation,DbAttributeBase attribute);

        protected void SetInvocationResult(IInvocation invocation, object result)
        {
            if (invocation.Method.ReturnType != typeof(void))
            {
                invocation.ReturnValue = result;
            }
        }
    }
}