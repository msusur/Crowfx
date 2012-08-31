namespace Crow.Library.InjectionContainer
{

    /* NInject swaped with a custom injection container due to some performance issues. */
    //internal class NInjectDependencyContainer : IInjector
    //{
    //    private StandardKernel m_Kernel = new StandardKernel();

    //    public void Bind<TInterfaceType, TClassType>()
    //        where TClassType : TInterfaceType
    //    {
    //        if (!typeof(TInterfaceType).IsInterface)
    //        {
    //            throw new InvalidOperationException(string.Format("Type '{0}' must be an interface.", typeof(TInterfaceType).Name));
    //        }
    //        m_Kernel.Bind<TInterfaceType>().To<TClassType>().InSingletonScope();
    //    }
    //    public void Bind<TInterfaceType>(object defaultType)
    //    {
    //        if (!typeof(TInterfaceType).IsInterface)
    //        {
    //            throw new InvalidOperationException(string.Format("Type '{0}' must be an interface.", typeof(TInterfaceType).Name));
    //        }
    //        m_Kernel.Bind<TInterfaceType>().To(defaultType.GetType()).InSingletonScope();
    //    }

    //    public TType Resolve<TType>()
    //    {
    //        return ResolveByTypeOrDefault<TType>(default(TType));
    //    }
    //    public TType ResolveByTypeOrDefault<TType>(TType defaultType)
    //    {
    //        try
    //        {
    //            return m_Kernel.Get<TType>();
    //        }
    //        catch
    //        {
    //            Bind<TType>(defaultType);
    //            return m_Kernel.Get<TType>();
    //        }
    //    }

    //    public void TeardownType<TType>()
    //    {
    //        IBinding type = m_Kernel.GetBindings(typeof(TType)).FirstOrDefault();
    //        if (type == null)
    //        {
    //            return;
    //        }
    //        m_Kernel.RemoveBinding(type);
    //    }


    //    public void TeardownContainer()
    //    {
    //        m_Kernel = new StandardKernel();
    //    }
    //}
}