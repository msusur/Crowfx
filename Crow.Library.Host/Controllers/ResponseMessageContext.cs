
namespace Crow.Library.Host.Controllers
{
    internal class ResponseMessageContext
    {
        public JsonContent Content { get; set; }

        public System.Net.HttpStatusCode StatusCode { get; set; }

        internal static ResponseMessageContext GetContext(BusinessControllerBase controller, RequestParameters parameter, System.Net.Http.HttpRequestMessage request)
        {
            return new ResponseMessageContext
            {

            };
        }

    }
}