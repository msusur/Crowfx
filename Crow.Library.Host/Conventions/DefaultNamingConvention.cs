using System;
using System.Reflection;

namespace Crow.Library.Host.Conventions
{
    internal class DefaultNamingConvention : INamingConvention
    {
        public string GetClassNameByType(Type type)
        {
            type.ThrowIfNull("type");
            return type.Name;
        }

        public string GetMethodNameByMethodInfo(MethodInfo method)
        {
            method.ThrowIfNull("method");
            return method.Name;
        }


        public string GetMethodInfoByMethodNameFromInstance(object instance, string methodName)
        {
            return methodName;
        }
    }
}