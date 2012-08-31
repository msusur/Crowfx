using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Bootstrappers;
using Crow.Library.InjectionContainer;
using System.ComponentModel.Composition;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.Common;

namespace Crow.Library.Tests.Aspects
{
    [Export(typeof(IModule))]
    public class MyAspectStartupInstaller : IModule
    {
        public void Install(IInjectionContainer container, IQueryStore queryStore)
        {
            container.Bind<ITestBusiness, TestBusiness>();
        }
    }
}
