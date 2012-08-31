using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.CodeExecutionFlow.Attributes;
using Castle.DynamicProxy;

namespace Crow.Library.CodeExecutionFlow.Steps
{
    internal sealed class CacheStep : MachineWorkStep
    {
        public override Type RequiresAttributeType
        {
            get { return typeof(CacheAttribute); }
        }

        public CacheStep(string stepName = "Cache Step")
            : base(stepName)
        {

        }
        public CacheStep(InvocationTypes type, string stepName = "Log Step")
            : base(stepName, type)
        {

        }

        protected override void OnStepExecuted(IInvocation invocation, IEnumerable<AspectAttributeBase> attribute)
        {

        }
    }
}
