using System;
using Castle.DynamicProxy;
using Crow.Library.Interceptors.LibraryInterceptors;
using Crow.Library.InjectionContainer;
using System.ComponentModel;

namespace Crow.Library.BusinessFactory
{
    /// <summary>
    /// Business class factory.
    /// </summary>
    public static class Business
    {
        /// <summary>
        /// Gets the proxy for the depended concrete class. 
        /// </summary>
        /// <typeparam name="TBusinessType">This parameter must be an interface in order to resolve from the dependency container.</typeparam>
        /// <returns>Returns the custom proxy generated from the instance get from the dependency injection container.</returns>
        public static TBusinessType Get<TBusinessType>()
        {
            TBusinessType instance = DIContainer.DefaultContainer.Resolve<TBusinessType>();
            Type[] interfaces = instance.GetType().GetInterfaces();
            if (interfaces.Length == 0)
            {
                throw new Exception();//must implement at least one interface.
            }
            ProxyGenerator generator = new ProxyGenerator();
            return (TBusinessType)generator.CreateClassProxy(instance.GetType(), Business.GetInterceptors());
        }

        private static IInterceptor[] GetInterceptors()
        {
            return new IInterceptor[] 
            { 
                new StrategyInterceptor()
            };
        }
    }
}