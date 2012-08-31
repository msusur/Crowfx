using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.SelfHost;
using Crow.Library.Foundation.Hosting;
using Crow.Library.Foundation.Logger;
using Crow.Library.Host.Configuration;
using Crow.Library.Host.Conventions;
using Crow.Library.Host.Handler;
using Crow.Library.InjectionContainer;
using Crow.Library.Foundation.Common.Configuration;

namespace Crow.Library.Host
{
    /// <summary>
    /// Base class for hosting.
    /// </summary>
    public abstract class CrowHostingBase : ICrowHttpHost, ITypeListHost
    {
        private HttpSelfHostConfiguration _configuration;

        private readonly IHttpHost _host;
        private HttpMessageHandler _handler;
        private bool _disposed;


        private readonly ILog _log;
        private readonly INamingConvention _namingConvention;
        private readonly Dictionary<string, Type> _TypeList = new Dictionary<string, Type>();
        protected readonly IInjectionContainer _container;
        protected readonly List<string> _FilteredUrlList = new List<string>();
        protected readonly IConfigurationHelper _Configuration;

        /// <summary>
        /// List of business types.
        /// </summary>
        public IDictionary<string, Type> BusinessTypeList
        {
            get { return _TypeList; }
            set { /* TODO remove description*/}
        }

        /// <summary>
        /// Initializer for CrowHostingBase.
        /// </summary>
        public CrowHostingBase(ILog log, INamingConvention convention,
                                IInjectionContainer container,
                                IConfigurationHelper configurationHelper, IHttpHost host)
        {
            _log = log;
            _log.ThrowIfNull("log");

            _namingConvention = convention;
            _namingConvention.ThrowIfNull("convention");

            _container = container;
            _container.ThrowIfNull("container");

            _Configuration = configurationHelper;
            _Configuration.ThrowIfNull("configurationHelper");

            _host = host;
            _host.ThrowIfNull("host");
        }

        /// <summary>
        /// Adds a new Business definition for host.
        /// </summary>
        /// <typeparam name="TBusinessType">Business interface.</typeparam>
        public ICrowHttpHost Host<TBusinessType>()
        {
            Type type = typeof(TBusinessType);
            return Host(type);
        }

        /// <summary>
        /// Adds a new Business definition for host.
        /// </summary>
        public ICrowHttpHost Host(Type type)
        {
            string typeName = _namingConvention.GetClassNameByType(type);
            BusinessTypeList[typeName] = type;
            return this;
        }

        /// <summary>
        /// Opens crow the documentation business.
        /// </summary>
        /// <returns></returns>
        public ICrowHttpHost AllowDocumentation()
        {
            return this;
        }

        /// <summary>
        /// Adds a request filter for intercepting requests.
        /// </summary>
        /// <typeparam name="TRequestFilter">Filter type.</typeparam>
        public ICrowHttpHost AddRequestFilter<TRequestFilter>()
            where TRequestFilter : IRequestFilter
        {
            return this;
        }

        /// <summary>
        /// Initializes the Crow http host.
        /// </summary>
        public ICrowHttpHost Start()
        {
            _configuration.ThrowIfNull("Configuration");
            CrowBusinessHandler businessHandler = new CrowBusinessHandler(this, _namingConvention, _container);
            _handler = new HttpCrowDispatcher(_configuration, businessHandler, _FilteredUrlList);
            _host.InitializeHost(_configuration, _handler, this);
            BeforeHostStart(_host, _handler, _configuration);
            ConfigureHost(_host);
            _log.Debug("Starting the http host.");
            _host.OpenAsync().Wait();
            _log.Debug("Host server started.");
            return this;
        }

        /// <summary>
        /// Closes the current http host.
        /// </summary>
        /// <returns></returns>
        public ICrowHttpHost Close()
        {
            _log.Debug("Closing the http host.");
            _host.CloseAsync().Wait();
            _log.Debug("Host closed.");
            return this;
        }

        /// <summary>
        /// Configures host with the given configuration.
        /// </summary>
        /// <param name="configuration">Host configuration.</param>
        public void Configure(IHostConfiguration configuration)
        {
            configuration.ThrowIfNull();
            _configuration = new HttpSelfHostConfiguration(CreateUrl(configuration));
        }

        /// <summary>
        /// Closes the host instance and releases any unmanaged resources used by the http host.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _host.ThrowIfNull();
                if (disposing)
                {
                    this.Close();
                }
                _host.Dispose();
            }
            _disposed = true;
        }

        ~CrowHostingBase()
        {
            Dispose(false);
        }

        protected virtual void BeforeHostStart(IHttpHost host, HttpMessageHandler handler, HttpSelfHostConfiguration configuration)
        {
        }

        private Uri CreateUrl(IHostConfiguration configuration)
        {
            string url = string.Format("http{0}://{1}:{2}",
                                            configuration.IsHttps ? "s" : "",
                                            configuration.BaseUrl,
                                            configuration.Port);
            return new Uri(url, UriKind.RelativeOrAbsolute);
        }

        private void ConfigureHost(IHttpHost _host)
        {

        }
    }
}