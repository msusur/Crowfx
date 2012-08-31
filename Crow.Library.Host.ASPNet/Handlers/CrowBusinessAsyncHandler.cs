using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Crow.Library.Host.Handler;
using System.Net.Http;
using Crow.Library.Host.Controllers;

namespace Crow.Library.Host.AspNet.Handlers
{
    internal sealed class CrowBusinessAsyncHandler : AsyncHandlerBase
    {
        private readonly BusinessInvoker _Handler;

        public CrowBusinessAsyncHandler(BusinessInvoker handler)
        {
            _Handler = handler;
        }

        public override Task ProcessRequestAsync(HttpContextBase context)
        {
            return _Handler.Invoke(context.Request);
                //SendAsync(context, new System.Threading.CancellationToken(false));
        }
    }
}
