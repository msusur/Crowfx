using Crow.Library.Foundation.Hosting;
using System.Collections.Generic;
using System.Reflection;
using Crow.Library.Common;
using System;
using Crow.Library.Bootstrappers;
using System.Linq;
using Crow.Library.Host.Conventions;
using Crow.Library.Foundation;

namespace Crow.Library.Host.AspNet
{
    public class CrowHostBootstrapper : Bootstrappers.Bootstrapper
    {
        public static ICrowHttpHost Host { get; private set; }

        public CrowHostBootstrapper()
            : base(new DefaultAssemblyLoader())
        {

        }

        protected override void LoadModules(IEnumerable<Assembly> assemblies)
        {
            base.LoadModules(assemblies);

            bool startHost = CrowCore.Configuration.Get<bool>(Strings.Configuration.StartHost);
            if (startHost)
            {
                StartHost();
            }
        }

        private void StartHost()
        {
            Host = CrowHost.ConfigureHost(CrowCore.Container);
            IEnumerable<Lazy<IHostBusiness>> business = _container.GetExports<IHostBusiness>();
            Type type;
            foreach (var item in business)
            {
                type = item.Value.GetType().GetInterfaces().
                    Where((t) => t != typeof(IHostBusiness))
                    .FirstOrDefault<Type>();
                type.ThrowIfNull("type");

                Host.Host(type);
            }
            Host.Start();
        }

        protected override void UnloadModules()
        {
            if (Host != null)
            {
                Host.Dispose();
            }
        }
    }
}
