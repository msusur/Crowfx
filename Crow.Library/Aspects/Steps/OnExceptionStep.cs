using System.Collections.Generic;
using Crow.Library.CodeExecutionFlow.Attributes;
using System;
using Crow.Library.Common;
using Castle.DynamicProxy;

namespace Crow.Library.CodeExecutionFlow.Steps
{
    public class OnExceptionStep : MachineWorkStep
    {
        public OnExceptionStep(string stepName = "Method Exception")
            : base(stepName)
        {

        }
        protected override void OnStepExecuted(IInvocation invocation, IEnumerable<AspectAttributeBase> attribute)
        {
            Exception e = base.Context.GetByName<Exception>("Exception");
            if (e == null)
            {
                return;
            }
            CrowCore.MessageService.ShowErrorMessage(e.Message);
        }
    }
}