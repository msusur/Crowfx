using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Host.Controllers;
using System.Net.Http;
using Crow.Library.Host.Configuration;
using Crow.Library.Host.Conventions;
using Crow.Library.InjectionContainer;

namespace Crow.Library.Foundation.Hosting
{
    public interface IBusinessControllerSelector
    {
        BusinessControllerBase SelectBusinessController(HttpRequestMessage request, ITypeListHost host, INamingConvention convention, IInjectionContainer container);
    }
}
