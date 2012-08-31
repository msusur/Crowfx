using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using Castle.DynamicProxy;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    internal class DynamicArgumentHolder : DynamicObject// IDynamicMetaObjectProvider
    {
        private Dictionary<string, object> _values = new Dictionary<string, object>();
        private static readonly ConcurrentDictionary<string, ParameterInfo[]> _ParametersList = new ConcurrentDictionary<string, ParameterInfo[]>();


        public DynamicArgumentHolder(IInvocation invocation)
        {
            string key = string.Format("{0}:{1}", invocation.TargetType.Name, invocation.Method.Name);
            ParameterInfo[] parameters =
                _ParametersList.GetOrAdd(key, invocation.InvocationTarget.GetType().GetInterfaces()[0].GetMethods()[0].GetParameters());


            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                object v = invocation.Arguments[i];
                _values.Add(parameters[i].Name, v);
            }
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = _values[indexes[0].ToString()];
            return true;
        }
    }
}