using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Crow.Library.InjectionContainer;
using Crow.Library.Host.AspNet.Handlers;
using Crow.Library.Host.Controllers;
using Crow.Library.Host.Conventions;

namespace Crow.Library.Host.AspNet
{
    public static class RouteExtensions
    {
        public static RouteBase MapBusiness(this RouteCollection routes, IInjectionContainer container)
        {
            return MapBusiness(routes, "~/crow", container);
        }
        public static RouteBase MapBusiness(this RouteCollection routes, string url, IInjectionContainer container)
        {
            INamingConvention convention = container.Resolve<INamingConvention>();
            BusinessInvoker invoker = new BusinessInvoker(AspNetRoutedHost.HostList, convention, container);
            string routeUrl = url;
            if (!routeUrl.EndsWith("/"))
            {
                routeUrl += "/{*operation}";
            }

            routeUrl = routeUrl.TrimStart('~').TrimStart('/');

            var route = new Route(routeUrl, new CrowRouteHandler(invoker));

            routes.Add("Crow", route);

            return route;
        }
    }
}
