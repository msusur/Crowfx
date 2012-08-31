using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Hosting
{
    /// <summary>
    /// Represents the host class for Http Requests.
    /// </summary>
    public interface ICrowHttpHost : IDisposable
    {
        /// <summary>
        /// Hosts the spesified business contract as a Http REST Service.
        /// </summary>
        ICrowHttpHost Host<TBusinessType>();

        /// <summary>
        /// Hosts the spesified business contract as a Http REST Service.
        /// </summary>
        ICrowHttpHost Host(Type businessContractType);

        /// <summary>
        /// Allows the documentation.
        /// </summary>
        /// <returns></returns>
        ICrowHttpHost AllowDocumentation();

        /// <summary>
        /// Adds a request filter that will interfere the incoming requests.
        /// </summary>
        ICrowHttpHost AddRequestFilter<TRequestFilter>()
             where TRequestFilter : IRequestFilter;

        /// <summary>
        /// Starts the multi-threaded host for Crow.
        /// </summary>
        /// <returns></returns>
        ICrowHttpHost Start();

        /// <summary>
        /// Configures the host with the given configuration.
        /// </summary>
        void Configure(IHostConfiguration configuration);
    }
}
