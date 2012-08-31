using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Crow.Library.Foundation.Common.Aspects
{
    /// <summary>
    /// Represents the method invocations.
    /// </summary>
    public interface IMethodInvocationContext
    {
        /// <summary>
        /// Type of the proxy object.
        /// </summary>
        Type ProxyType { get; }

        /// <summary>
        /// Method that is invoking.
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// Method arguments.
        /// </summary>
        object[] Args { get; }

        /// <summary>
        /// Generic arguments of the method.
        /// </summary>
        Type[] GenericArguments { get; }

        /// <summary>
        /// Sets or gets the return value of the current invoking method.
        /// </summary>
        object ReturnValue { get; set; }

        /// <summary>
        /// Current proxy instance.
        /// </summary>
        object Proxy { get; }

        /// <summary>
        /// Gets or sets if the invoking method can cancel method invocation.
        /// </summary>
        bool Cancel { get; set; }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <returns></returns>
        object Proceed();

        /// <summary>
        /// Exception that is occured while method invocation.
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets if the method is executed.
        /// </summary>
        bool IsMethodExecuted { get; set; }
    }
}
