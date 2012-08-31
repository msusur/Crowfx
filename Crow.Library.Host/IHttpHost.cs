using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Net.Http;
using Crow.Library.Host.Handler;
using System.Web.Http;
using Crow.Library.Host.Configuration;

namespace Crow.Library.Host
{
    /// <summary>
    /// Represents the Http Host object for the REST Service.
    /// </summary>
    public interface IHttpHost : IDisposable
    {
        /// <summary>
        /// Initializes the self hosting environment.
        /// </summary>
        void InitializeHost(HttpConfiguration configuration, HttpMessageHandler messageHandler, ITypeListHost typeHost);

        /// <summary>
        /// Starts the host.
        /// </summary>
        Task OpenAsync();

        /// <summary>
        /// Stops and closes the host.
        /// </summary>
        Task CloseAsync();
    }
}
