using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Aspects;
using Crow.Library.Logger.PerformanceCounting;
using Crow.Library.Foundation.Logger;
using Crow.Library.InjectionContainer;

namespace Crow.Library.Aspects.Attributes
{
    /// <summary>
    /// Aspect that adds a tracing on the marked method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TraceAttribute : AspectAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the TraceAttribute.
        /// </summary>
        public TraceAttribute()
        {
        }

        /// <summary>
        /// Method that is going to be executed before the method is executed.
        /// "Dynamically use only do not call this method."
        /// </summary>
        /// <param name="context"></param>
        [WorksBefore, Obsolete("Dynamically use only do not call this method.", true)]
        public void TraceMethodExecutionDuration(IMethodInvocationContext context)
        {
            using (Performance perf = new Performance(string.Format("{0}.{1}", context.ProxyType.Name, context.Method.Name)))
            {
                context.Proceed();
                context.IsMethodExecuted = true;
            }
            ILog log = DIContainer.DefaultContainer.Resolve<ILog>();
            StringBuilder builder = new StringBuilder();
            //TODO: Thread safe!
            Performance.WriteAndClear(builder);
            log.Debug(builder.ToString());
            context.Cancel = true;
        }
    }
}
