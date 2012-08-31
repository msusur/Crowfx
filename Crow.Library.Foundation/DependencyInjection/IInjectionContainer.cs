
using System;
namespace Crow.Library.InjectionContainer
{
    /// <summary>
    /// Dependency Injection interface.
    /// </summary>
    public interface IInjectionContainer
    {
        /// <summary>
        /// Binds the interface to the type given.
        /// </summary>
        /// <typeparam name="TInterfaceType"></typeparam>
        /// <typeparam name="TClassType"></typeparam>
        void Bind<TInterfaceType, TClassType>() where TClassType : TInterfaceType;

        /// <summary>
        /// Binds the interface with the factory method.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="factoryMethod"></param>
        void Bind<TInterface>(Func<object> factoryMethod);

        /// <summary>
        /// Binds the interface to the instance.
        /// </summary>
        /// <typeparam name="TInterfaceType"></typeparam>
        /// <param name="instance"></param>
        void Bind<TInterfaceType>(object instance);

        /// <summary>
        /// Resolves the instance and parameters if the constructor has some.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        TType Resolve<TType>();

        /// <summary>
        /// Resolves the given type and returns an instance for it.
        /// </summary>
        object Resolve(Type controllerType);

        /// <summary>
        /// Tearsdown the given type.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        void TeardownType<TType>();

        /// <summary>
        /// Removes all the definitions.
        /// </summary>
        void TeardownContainer();


    }
}
