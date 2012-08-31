using Castle.DynamicProxy;
using Crow.Library.Aspects.DBOperationAttributes;
using Crow.Library.Common;
using Crow.Library.DatabaseLayer.DataAttributes;
using Crow.Library.Foundation.Common.Aspects;
using Crow.Library.Interceptors.DatabaseInceptors.DbOperations;

namespace Crow.Library.Interceptors.DatabaseInceptors
{
    public class DbInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            DbAttributeBase attribute = AttributesHelper.GetAttribute<DbAttributeBase>(invocation);

            if (attribute == null)
            {
                new SelectOperator().Execute(invocation, null);
                return;
            }

            //for db operation aspects
            MethodInvocationContext context = new MethodInvocationContext(invocation);
            var atts = AttributesHelper.GetAttributes(invocation, typeof(DBAspectAttributeBase));
            foreach (DBAspectAttributeBase aspect in atts)
            {
                aspect.Process(context);
                if (context.Cancel == true)
                    return;
            }

            DbOperationBase operation = DbOperationFactory.GetOperator(attribute);
            operation.Execute(invocation, attribute);
            //TODO: add aspect for db interceptor. Cache is highly needed. or does caching needs to be solved over business classes.
            //      think about this you !'=!^=!^)'+(^/)
        }
    }
}