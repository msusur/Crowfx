using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Crow.Library.Host.Conventions
{
    /// <summary>
    /// Manages all naming conventions over service infrastructure.
    /// </summary>
    public interface INamingConvention
    {
        /// <summary>
        /// Gets the class name by given interface business type.
        /// </summary>
        string GetClassNameByType(Type businessType);

        /// <summary>
        /// Gets the method name by given method info.
        /// </summary>
        string GetMethodNameByMethodInfo(MethodInfo methodInfo);

        /// <summary>
        /// Gets the method name from the instance
        /// </summary>
        /// <param name="_BusinessInstance"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        string GetMethodInfoByMethodNameFromInstance(object instance, string methodName);
    }
}