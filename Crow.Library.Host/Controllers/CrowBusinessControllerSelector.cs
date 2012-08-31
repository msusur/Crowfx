using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Hosting;
using System.Net.Http;
using Crow.Library.Host.Controllers.BusinessController;
using Crow.Library.Host.Configuration;
using Crow.Library.Foundation.Common.Helpers;
using Crow.Library.InjectionContainer;
using Crow.Library.Host.Conventions;

namespace Crow.Library.Host.Controllers
{
    internal class CrowBusinessControllerSelector : IBusinessControllerSelector
    {
        public BusinessControllerBase SelectBusinessController(HttpRequestMessage request, ITypeListHost host, INamingConvention convention, IInjectionContainer container)
        {
            UrlParser url = new UrlParser(request.RequestUri.ToString());
            Type controllerType = host.BusinessTypeList[url.BusinessClass];
            var instance = container.Resolve(controllerType);
            return new CrowBusinessController(instance, url, convention);
        }
    }
}
