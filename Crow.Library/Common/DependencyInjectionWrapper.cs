using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.InjectionContainer;

namespace Crow.Library.Common
{
    public class DependencyInjectionWrapper : IInjectionContainer
    {
        private readonly IInjectionContainer _container;

        public DependencyInjectionWrapper(IInjectionContainer container)
        {
            _container = container;
        }

        public void Bind<TInterfaceType, TClassType>()
            where TClassType : TInterfaceType
        {
            _container.Bind<TInterfaceType, TClassType>();
        }

        public void Bind<TInterface>(Func<object> factoryMethod)
        {
            _container.Bind<TInterface>(factoryMethod);
        }

        public void Bind<TInterfaceType>(object instance)
        {
            _container.Bind<TInterfaceType>(instance);
        }

        public TType Resolve<TType>()
        {
            return _container.Resolve<TType>();
        }

        public object Resolve(Type controllerType)
        {
            return _container.Resolve(controllerType);
        }

        public void TeardownType<TType>()
        {
            _container.TeardownType<TType>();
        }

        public void TeardownContainer()
        {
            _container.TeardownContainer();
        }
    }
}
