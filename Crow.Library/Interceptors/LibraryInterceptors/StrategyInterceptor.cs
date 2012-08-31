using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Crow.Library.Aspects.Attributes;
using Crow.Library.Common;
using Crow.Library.Foundation.Common.Aspects;

namespace Crow.Library.Interceptors.LibraryInterceptors
{
    internal class StrategyInterceptor : IInterceptor
    {
        private static readonly ConcurrentDictionary<string, List<MethodInfo>> _methodList = new ConcurrentDictionary<string, List<MethodInfo>>();

        public void Intercept(IInvocation invocation)
        {
            try
            {
                OnMethodExecuting(invocation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void OnMethodExecuting(IInvocation invocation)
        {
            var attributes = AttributesHelper.GetAttributes(invocation, typeof(AspectAttributeBase)).Cast<AspectAttributeBase>().OrderBy(i => i.Order);
            IMethodInvocationContext context = new MethodInvocationContext(invocation);
            try
            {
                bool cancelExecution = IterateAttributesAndReturnTrueIfCancel(context, attributes, ExecutionOrder.Before);
                if (!cancelExecution)
                {
                    invocation.Proceed();
                }
                IterateAttributesAndReturnTrueIfCancel(context, attributes, ExecutionOrder.After);
            }
            catch (Exception ex)
            {
                IterateAttributesAndReturnTrueIfCancel(context, attributes, ExecutionOrder.Exception, ex);
            }
        }

        private bool IterateAttributesAndReturnTrueIfCancel(IMethodInvocationContext context, IEnumerable<AspectAttributeBase> attributes, ExecutionOrder order, Exception ex = null)
        {
            List<MethodInfo> methods = GetAllMethodsInType(context, attributes, order);
            context.Exception = ex;
            bool cancelExecution = false;
            foreach (MethodInfo method in methods)
            {
                method.Invoke(Activator.CreateInstance(method.ReflectedType), new object[] { context });
                cancelExecution = context.Cancel;
            }
            return cancelExecution;
        }

        private List<MethodInfo> GetAllMethodsInType(IMethodInvocationContext context, IEnumerable<AspectAttributeBase> attributes, ExecutionOrder order)
        {
            string key = string.Format("{0}:{1}:{2}", context.ProxyType.Name, context.Method.Name, order);

            Type attributeExecutionOrderType;
            switch (order)
            {
                default:
                case ExecutionOrder.Before:
                    attributeExecutionOrderType = typeof(WorksBeforeAttribute);
                    break;
                case ExecutionOrder.After:
                    attributeExecutionOrderType = typeof(WorksAfterAttribute);
                    break;
                case ExecutionOrder.Exception:
                    attributeExecutionOrderType = typeof(WorksOnExceptionAttribute);
                    break;
            }
            List<MethodInfo> methods = new List<MethodInfo>();
            foreach (var attribute in attributes)
            {
                foreach (var m in attribute.GetType().GetMethods().Where(m => m.GetCustomAttributes(attributeExecutionOrderType, true).Length > 0).ToList())
                {
                    methods.Add(m);
                }
            }
            return methods;
        }
    }
}