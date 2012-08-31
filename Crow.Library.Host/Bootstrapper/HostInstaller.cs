using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.InjectionContainer;
using Crow.Library.Foundation.Hosting;
using System.ComponentModel.Composition;
using Crow.Library.Host.Configuration;
using Crow.Library.Host.Handler;
using Crow.Library.Host.Controllers;
using Crow.Library.Host.Conventions;
using Crow.Library.Host.CrowController;
using Crow.Library.Common;

namespace Crow.Library.Host.Bootstrapper
{
    [Export(typeof(IModule))]
    public class HostModule : IModule
    {
        public void Install(IInjectionContainer container, IQueryStore queryStore)
        {
            container.Bind<IHttpHost, AspNetSelfHosting>();
            container.Bind<ICrowHttpHandler, HttpCrowDispatcher>();
            container.Bind<INamingConvention, DefaultNamingConvention>();
            container.Bind<ICrow, CrowDocumentationController>();
            container.Bind<ICrowHttpHost, CrowHost>();
            container.Bind<IHostConfiguration, HostConfiguration>();
            container.Bind<IBusinessControllerSelector, CrowBusinessControllerSelector>();
        }
    }
}
