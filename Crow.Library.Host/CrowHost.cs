using System.Net.Http;
using System.Web.Http.SelfHost;
using Crow.Library.Foundation.Logger;
using Crow.Library.Host.Conventions;
using Crow.Library.Host.CrowController;
using Crow.Library.InjectionContainer;
using Crow.Library.Foundation.Hosting;
using Crow.Library.Foundation.Common.Configuration;
using Crow.Library.Foundation;

namespace Crow.Library.Host
{
    /// <summary>
    /// Host application for http api.
    /// </summary>
    public sealed class CrowHost : CrowHostingBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CrowHost"/>.
        /// </summary>
        /// <param name="log">Instance of logger.</param>
        /// <param name="convention">Naming convention instance.</param>
        /// <param name="container">Dependency injection container.</param>
        /// <param name="configuration">Configuration helper.</param>
        /// <param name="host">Http host instance.</param>
        public CrowHost(ILog log, INamingConvention convention,
                        IInjectionContainer container,
                        IConfigurationHelper configuration,
                        IHttpHost host)
            : base(log, convention, container, configuration, host) { }

        /// <summary>
        /// Initializes before the host starts.
        /// </summary>
        protected override void BeforeHostStart(IHttpHost host, HttpMessageHandler handler, HttpSelfHostConfiguration configuration)
        {
            base.Host<ICrow>(); //register default crow documentation interface.
            base._FilteredUrlList.Add("favicon.ico");
            base.BeforeHostStart(host, handler, configuration);
        }

        /// <summary>
        /// Configures the host with default configuration.
        /// Port: 80 and baseurl : localhost
        /// </summary>
        public static ICrowHttpHost ConfigureHost(IInjectionContainer container)
        {
            var configuration = container.Resolve<IConfigurationHelper>();
            var port = configuration.Get(Strings.Configuration.HostingPort, Strings.Defaults.DefaultPort);
            var host = configuration.Get(Strings.Configuration.HostingHostname, Strings.Defaults.DefaultHostname);
            var isHttps = configuration.Get(Strings.Configuration.HostingIsHttps, false);
            return ConfigureHost(container, host, port, isHttps);
        }
        /// <summary>
        /// Configures the host with default port 80.
        /// </summary>
        public static ICrowHttpHost ConfigureHost(IInjectionContainer container, string baseUrl)
        {
            var configuration = container.Resolve<IConfigurationHelper>();
            var port = configuration.Get<int>(Strings.Configuration.HostingPort, Strings.Defaults.DefaultPort);
            var host = baseUrl;
            var isHttps = configuration.Get<bool>(Strings.Configuration.HostingIsHttps, false);
            return ConfigureHost(container, host, port, isHttps);
        }
        /// <summary>
        /// Configures the host with the given baseurl and port.
        /// </summary>
        public static ICrowHttpHost ConfigureHost(IInjectionContainer container, string baseUrl, int port, bool isHttps = false)
        {
            var host = container.Resolve<ICrowHttpHost>();
            var configuration = container.Resolve<IHostConfiguration>();
            configuration.BaseUrl = baseUrl;
            configuration.IsHttps = isHttps;
            configuration.Port = port;
            host.Configure(configuration);
            return host;
        }
    }
}
