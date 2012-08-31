using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections.Generic;
using System.Web;
using System.Web.Hosting;
using Crow.Library.Foundation;

namespace Crow.Library.Host.Handler
{
    internal class HttpCrowDispatcher : HttpMessageHandler, ICrowHttpHandler
    {
        private readonly HttpConfiguration _configuration;
        private readonly HttpMessageInvoker _messageInvoker;
        private readonly IEnumerable<string> _filteredUrl;



        public HttpCrowDispatcher(HttpConfiguration configuration, HttpMessageHandler handler, IEnumerable<string> filteredUrl)
            : base()
        {
            _configuration = configuration;
            _messageInvoker = new HttpMessageInvoker(handler);
            _filteredUrl = filteredUrl ?? new List<string>();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool canContinue = CanContinue(request, _filteredUrl);
            if (!canContinue)
            {
                return new Task<HttpResponseMessage>(() =>
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                });
            }
            //request.Properties.Add(Strings.Configuration.RequestContext, new RequestContext { Request = request });
            return _messageInvoker.SendAsync(request, cancellationToken);
        }

        private bool CanContinue(HttpRequestMessage request, IEnumerable<string> filteredUrl)
        {
            //TODO: this operation must be improved.
            foreach (var item in filteredUrl)
            {
                if (request.RequestUri.AbsolutePath.EndsWith(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
