using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Crow.Library.Foundation.Hosting;
using Crow.Library.Host.Controllers;
using Crow.Library.InjectionContainer;
using Crow.Library.Host.Configuration;
using Crow.Library.Host.Conventions;
using Crow.Library.Foundation;
using System.Web;

namespace Crow.Library.Host.Handler
{
    /// <summary>
    /// Handler that invokes the business methods.
    /// </summary>
    public class CrowBusinessHandler : HttpMessageHandler
    {
        private readonly INamingConvention _convention;
        private readonly ITypeListHost _host;
        private readonly IInjectionContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="CrowBusinessHandler"/>.
        /// </summary>
        public CrowBusinessHandler(ITypeListHost host,
                                    INamingConvention convention,
                                    IInjectionContainer container)
        {
            _host = host;
            _host.ThrowIfNull("host");

            _convention = convention;
            _convention.ThrowIfNull("convention");

            _container = container;
            _container.ThrowIfNull("container");
        }

        /// <summary>
        ///  Send an HTTP request as an asynchronous operation.
        /// </summary>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            BusinessInvoker invoker = new BusinessInvoker(_host, _convention, _container);
            return invoker.Invoke(request);
        }

        /// <summary>
        ///  Send an HTTP request as an asynchronous operation.
        /// </summary>
        public Task SendAsync(HttpContextBase context, CancellationToken cancellationToken)
        {
            HttpMethod method = new HttpMethod(context.Request.HttpMethod);
            HttpRequestMessage message = new HttpRequestMessage(method, context.Request.Url);
            return SendAsync(message, cancellationToken);
        }
    }
}
