using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.CodeExecutionFlow.Attributes;
using Castle.DynamicProxy;

namespace Crow.Library.CodeExecutionFlow.Steps
{
    internal sealed class LogStep : MachineWorkStep
    {
        public override Type RequiresAttributeType
        {
            get { return typeof(LogAttribute); }
        }
        public LogStep(string stepName = "Log Step")
            : base(stepName)
        { }
        public LogStep(InvocationTypes type, string stepName = "Log Step")
            : base(stepName, type)
        {

        }

        protected override void OnStepExecuted(IInvocation invocation, IEnumerable<AspectAttributeBase> attribute)
        {
        }
    }
}
