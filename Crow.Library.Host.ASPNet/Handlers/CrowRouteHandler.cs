using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using Crow.Library.Host.Handler;
using Crow.Library.Host.Controllers;

namespace Crow.Library.Host.AspNet.Handlers
{
    public class CrowRouteHandler : IRouteHandler
    {
        private readonly BusinessInvoker _Handler;

        public CrowRouteHandler(BusinessInvoker handler) 
        {
            _Handler = handler;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new CrowBusinessAsyncHandler(_Handler);
        }
    }
}
