using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Logger;
using Crow.Library.CodeExecutionFlow.Attributes;
using Castle.DynamicProxy;
using Castle.Core.Logging;
using Crow.Library.InjectionContainer;

namespace Crow.Library.CodeExecutionFlow
{
    public enum InvocationTypes { In, Out }
    public abstract class MachineWorkStep
    {
        public virtual Type RequiresAttributeType
        {
            get { return null; }
        }
        private readonly ILogger _Logger;

        public string StepName { get; private set; }
        public InvocationTypes InvocationType { get; protected set; }

        public MachineWorkStep(string stepName)
        {
            _Logger = DIContainer.DefaultContainer.GetType<ILogger>();
            this.StepName = stepName;
        }
        public MachineWorkStep(string stepName, InvocationTypes invocationType)
            : this(stepName)
        {
            this.InvocationType = invocationType;
        }

        public void ExecuteStep(IInvocation invocation, IEnumerable<AspectAttributeBase> attributes)
        {
            AddTrace("Executing operation");
            OnStepExecuted(invocation, attributes);
            AddTrace("Execution finished");
        }

        protected abstract void OnStepExecuted(IInvocation invocation, IEnumerable<AspectAttributeBase> attribute);

        private void AddTrace(string message)
        {
            _Logger.DebugFormat("{0}: {1}", message, StepName);
        }
    }
}