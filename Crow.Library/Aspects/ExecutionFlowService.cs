using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Common;
using Crow.Library.CodeExecutionFlow.Attributes;
using Castle.DynamicProxy;
using Crow.Library.Foundation.Logger;
using Crow.Library.InjectionContainer;

namespace Crow.Library.CodeExecutionFlow
{
    internal class ExecutionFlowService
    {
        public static ExecutionFlowService Current
        {
            get { return Singleton.Get<ExecutionFlowService>(); }
        }

        static ExecutionFlowService()
        {
            Singleton.Put<ExecutionFlowService>(new ExecutionFlowService());
        }

        //private readonly MachineFlowDefinitionCollection _Definitions;

        private readonly ILog _logger;

        public ExecutionFlowService()
        {
            //_Definitions = new MachineFlowDefinitionCollection();
            _logger = DIContainer.DefaultContainer.GetType<ILog>();
        }

        internal void StartChain(string flowName, StepExecutionContext context)
        {
            var definitions = from definition in _Definitions
                              where definition.FlowCode.Equals(flowName, StringComparison.InvariantCultureIgnoreCase)
                              select definition;
            foreach (var definition in definitions)
            {
                definition.Steps.ForEach((step) => ProceedStep(step, context));
            }
        }
        internal void StartChain(string flowName, IInvocation invocation)
        {
            StepExecutionContext context = new StepExecutionContext { Invocation = invocation };
            StartChain(flowName, context);
        }

        private void ProceedStep(MachineWorkStep step, StepExecutionContext context)
        {
            IInvocation invocation = context.Invocation;
            step.Context = context;
            if (step.RequiresAttributeType == null)
            {
                step.ExecuteStep(invocation, null);
                _logger.DebugFormat("'{0}' has executed. No Aspect is required.", step.StepName);
                return;
            }
            IEnumerable<object> attribute = AttributesHelper.GetAttributes(invocation, step.RequiresAttributeType);
            if (attribute == null || attribute.Count() == 0)
            {
                _logger.DebugFormat("'{0}' can not be executed. No aspect was applied.", step.StepName);
                return;
            }
            step.ExecuteStep(invocation, attribute.Cast<AspectAttributeBase>());
            _logger.DebugFormat("'{0}' has executed. Aspects are applied.", step.StepName);
        }
    }
}