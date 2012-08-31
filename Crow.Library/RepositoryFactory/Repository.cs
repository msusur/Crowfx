using System;
using Crow.Library.Common;
using Crow.Library.Interceptors.DatabaseInceptors;

namespace Crow.Library.RepositoryFactory
{
    public static class Repository
    {
        public static TRepositoryType Get<TRepositoryType>()
        {
            return Get<TRepositoryType>(new DbInterceptor());
        }
        private static TRepositoryType Get<TRepositoryType>(params DbInterceptor[] interceptors)
        {
            TRepositoryType @object = Singleton.Get<TRepositoryType>();
            if (@object == null)
            {
                @object = ObjectInitializerFactory.InitializeClassForType<TRepositoryType>();
                Castle.DynamicProxy.ProxyGenerator generator = new Castle.DynamicProxy.ProxyGenerator();
                @object = (TRepositoryType)generator.CreateClassProxy(@object.GetType(), interceptors);
                Singleton.Put<TRepositoryType>(@object);
            }
            return @object;
        }
    }
}