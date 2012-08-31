using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Crow.Library.InjectionContainer;
using Crow.Library.Host.Configuration;
using Crow.Library.Host.Conventions;
using Crow.Library.Foundation.Hosting;
using System.Web;

namespace Crow.Library.Host.Controllers
{
    public class BusinessInvoker
    {
        private readonly ITypeListHost _host;
        private readonly INamingConvention _convention;
        private readonly IInjectionContainer _container;

        private IBusinessControllerSelector _selector;

        /// <summary>
        /// Gets the binded Business Controller Selector.
        /// <seealso cref="IBusinessControlSelector"/>.
        /// </summary>
        protected IBusinessControllerSelector Selector
        {
            get
            {
                if (_selector == null)
                {
                    _selector = _container.Resolve<IBusinessControllerSelector>();
                }
                return _selector;
            }
        }

        public BusinessInvoker(ITypeListHost host, INamingConvention convention, IInjectionContainer container)
        {
            _host = host;
            _convention = convention;
            _container = container;
        }

        public Task<HttpResponseMessage> Invoke(HttpRequestMessage request)
        {
            BusinessControllerBase controller = Selector.SelectBusinessController(request, _host, _convention, _container);

            RequestParameters parameter = BusinessParameterBuilder.CreateParameters(controller, request);

            return Invoke(controller, parameter, request);
        }

        private Task<HttpResponseMessage> Invoke(BusinessControllerBase controller, RequestParameters parameter, HttpRequestMessage request)
        {
            controller.ThrowIfNull("controller");
            parameter.ThrowIfNull("parameter");

            return Task.Factory.StartNew<HttpResponseMessage>(() =>
            {
                //TODO: read content type from the given model's attribute.
                ResponseMessageContext context = ResponseMessageContext.GetContext(controller, parameter, request);
                try
                {
                    object result = controller.InvokeMethod(parameter);
                    context.Content = new JsonContent(result); // TODO: HttpContent's  should be get from the factory.
                    context.StatusCode = HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    context.Content = new JsonContent(new { Error = ex.Message });
                    context.StatusCode = HttpStatusCode.InternalServerError;
                }
                HttpResponseMessage response = new HttpResponseMessage
                {
                    Content = context.Content,
                    RequestMessage = request,
                    StatusCode = context.StatusCode
                };
                return response;
            });
        }

        public Task Invoke(HttpRequestBase httpRequestBase)
        {
            HttpMethod method = new HttpMethod(httpRequestBase.HttpMethod);
            HttpRequestMessage message = new HttpRequestMessage(method, httpRequestBase.Url);
            return Invoke(message);
        }
    }
}