using Crow.Library.Foundation.Common.Helpers;
using System.Reflection;
using Crow.Library.Host.Conventions;
using System;

namespace Crow.Library.Host.Controllers
{
    /// <summary>
    /// Base class for business controllers.
    /// </summary>
    public abstract class BusinessControllerBase
    {
        protected readonly object _BusinessInstance;
        protected readonly INamingConvention _namingConvention;

        private readonly UrlParser _UrlInformation;

        private MethodInfo _requestedMethod;

        public UrlParser Url
        {
            get { return _UrlInformation; }
        }

        public BusinessControllerBase(object instance, UrlParser url, INamingConvention convention)
        {
            _BusinessInstance = instance;
            _BusinessInstance.ThrowIfNull("instance");
            _UrlInformation = url;
            _UrlInformation.ThrowIfNull("url");
            _namingConvention = convention;
            _namingConvention.ThrowIfNull("convention");
        }

        public virtual MethodInfo GetControllerMethod(string methodName)
        {
            methodName = _namingConvention.GetMethodInfoByMethodNameFromInstance(_BusinessInstance, methodName);
            if (_requestedMethod == null)
            {
                _requestedMethod = _BusinessInstance.GetType().GetMethod(methodName);
                _requestedMethod.ThrowIfNull("methodInfo");
            }
            return _requestedMethod;
        }

        internal object InvokeMethod(RequestParameters parameter)
        {
            _requestedMethod.ThrowIfNull("RequestedMethod");
            parameter.ThrowIfNull("parameter");

            return _requestedMethod.Invoke(_BusinessInstance, parameter.ToArray());
        }
    }
}
