using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Crow.Library.Host.Handler;
using System.Web.Http;
using System.Threading.Tasks;
using Crow.Library.Host.Configuration;

namespace Crow.Library.Host.AspNet
{
    public class AspNetRoutedHost : IHttpHost
    {
        public static ITypeListHost HostList { get; private set; }

        public void InitializeHost(HttpConfiguration configuration, HttpMessageHandler messageHandler, ITypeListHost typeList)
        {
            HostList = typeList;
        }

        public Task OpenAsync()
        {
            return Task.Factory.StartNew(() => { });
        }

        public Task CloseAsync()
        {
            return Task.Factory.StartNew(() => { });
        }

        public void Dispose()
        {

        }
    }
}
