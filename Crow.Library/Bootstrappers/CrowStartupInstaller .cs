using Crow.Library.Common;
using Crow.Library.Common.Account;
using Crow.Library.Common.Caching;
using Crow.Library.Common.Configuration;
using Crow.Library.Common.Messages;
using Crow.Library.DatabaseLayer;
using Crow.Library.FileOperations;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.Foundation.Common;
using Crow.Library.Foundation.Common.Account;
using Crow.Library.Foundation.Common.Caching;
using Crow.Library.Foundation.Common.Configuration;
using Crow.Library.Foundation.Common.Messages;
using Crow.Library.Foundation.Common.Templating;
using Crow.Library.Foundation.FileOperations;
using Crow.Library.Foundation.Logger;
using Crow.Library.InjectionContainer;
using Crow.Library.Logger;
using Crow.Library.Templating;

namespace Crow.Library.Bootstrappers
{
    /// <summary>
    /// Module for crow.
    /// </summary>
    public class CrowModule : IModule
    {
        /// <summary>
        /// Installs the crow components.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="queryStore"></param>
        public void Install(IInjectionContainer container, IQueryStore queryStore)
        {
            
            container.Bind<ILog, DefaultLogger>();
            container.Bind<IFileOperations, FSFileOperations>();
            container.Bind<IConfigurationHelper, ApplicationConfigurationHelper>();
            container.Bind<IMessagingService, DebugMessagesService>();
            container.Bind<IAccountService, UnsafeAccountService>();
            container.Bind<IDialectProvider, SqlDialectProvider>();
            container.Bind<ICacheManager, InMemoryCacheManager>();
            container.Bind<ITemplateEngine, PoorMansTemplating>();
            container.Bind<IAssemblyLoader, FileAssemblyLoader>();
            container.Bind<IItemManager, ThreadItemManager>();
        }
    }
}