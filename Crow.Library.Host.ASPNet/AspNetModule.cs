using System.ComponentModel.Composition;
using Crow.Library.Bootstrappers;
using Crow.Library.Common;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.Foundation.Common;
using Crow.Library.Host.Bootstrapper;
using Crow.Library.Foundation.Hosting;

namespace Crow.Library.Host.AspNet
{
    [Export(typeof(IModule))]
    [DependsOn(typeof(HostModule))]
    public class AspNetModule : IModule
    {
        public void Install(InjectionContainer.IInjectionContainer container, Common.IQueryStore queryStore)
        {
            container.Bind<IHttpHost, AspNetRoutedHost>();
            container.Bind<IItemManager, ThreadItemManager>();
        }
    }
}