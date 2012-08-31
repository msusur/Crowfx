using System;
using System.Reflection;
using Castle.DynamicProxy;
using Crow.Library.Foundation.Common.Aspects;

namespace Crow.Library.Interceptors
{
    public sealed class MethodInvocationContext : IMethodInvocationContext
    {
        private IInvocation _Invocation;

        public MethodInfo Method
        {
            get { return _Invocation.Method; }
        }

        public object[] Args
        {
            get { return _Invocation.Arguments; }
        }

        public Type[] GenericArguments
        {
            get { return _Invocation.GenericArguments; }
        }

        public bool Cancel { get; set; }

        public object ReturnValue
        {
            get { return _Invocation.ReturnValue; }
            set { _Invocation.ReturnValue = value; }
        }

        public object Proxy
        {
            get { return _Invocation.InvocationTarget; }
        }

        public Type ProxyType
        {
            get { return _Invocation.InvocationTarget.GetType(); }
        }

        internal MethodInvocationContext(IInvocation invocation)
        {
            this._Invocation = invocation;
        }

        private MethodInvocationContext()
        {
        }

        public object Proceed()
        {
            if (IsMethodExecuted)
                return this.ReturnValue;
            _Invocation.Proceed();
            return _Invocation.ReturnValue;
        }


        public Exception Exception { get; set; }

        public bool IsMethodExecuted { get; set; }
    }
}