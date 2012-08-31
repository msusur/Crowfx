using System;
using System.Collections.Generic;
using Crow.Library.CodeExecutionFlow.Attributes;
using Castle.DynamicProxy;
using Crow.Library.Logger;
using Crow.Library.Foundation.Common.Aspects;
using Crow.Library.Interceptors;

namespace Crow.Library.CodeExecutionFlow.Steps
{
    public class MethodExecutionStep : MachineWorkStep
    {
        public MethodExecutionStep(string stepName = "Method execution")
            : base(stepName)
        { }

        protected override void OnStepExecuted(IInvocation invocation, IEnumerable<AspectAttributeBase> attributes)
        {
            DateTime dt = DateTime.Now;
            IMethodInvocationContext invocationContext = new MethodInvocationContext(invocation);
            foreach (var attribute in attributes)
            {
                attribute.OnAttributeExecuted(invocationContext);
                if (invocationContext.Cancel)
                {
                    return;
                }
            }
            invocation.Proceed();
        }
    }
}