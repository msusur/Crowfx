using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Hosting;
using System.Web.Http.SelfHost;
using System.Net.Http;
using System.Threading.Tasks;
using Crow.Library.Host.Handler;
using System.Web.Http;
using Crow.Library.Host.Configuration;

namespace Crow.Library.Host
{
    internal class AspNetSelfHosting : IHttpHost
    {
        private HttpSelfHostServer _server;

        public void InitializeHost(HttpConfiguration configuration, HttpMessageHandler messageHandler, ITypeListHost typeList)
        {
            _server = new HttpSelfHostServer(configuration as HttpSelfHostConfiguration, messageHandler);
        }

        public Task OpenAsync()
        {
            return _server.OpenAsync();
        }

        public Task CloseAsync()
        {
            return _server.CloseAsync();
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
