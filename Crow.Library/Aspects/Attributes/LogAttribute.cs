using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Aspects;
using Castle.Core.Logging;
using Crow.Library.InjectionContainer;
using Crow.Library.Foundation.Logger;

namespace Crow.Library.Aspects.Attributes
{
    /// <summary>
    /// Aspect that logs the method invocations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LogAttribute : AspectAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of LogAttribute.
        /// </summary>
        public LogAttribute()
        {

        }

        /// <summary>
        /// Method that is going to be executed before the method is executed.
        /// </summary>
        [WorksBefore, Obsolete("Dynamically use only do not call this method.", true)]
        public void OnMethodExecuting(IMethodInvocationContext context)
        {
            ILog log = DIContainer.DefaultContainer.Resolve<ILog>();
            log.DebugFormat("Method: {0} is executing...", context.Method.Name);
            context.ReturnValue = context.Proceed();
            context.Cancel = true;
            log.DebugFormat("Method: {0} done executing.", context.Method.Name);
        }
    }
}
