using Crow.Library.Foundation.Common.Account;
using Crow.Library.Foundation.Common.Configuration;
using Crow.Library.Foundation.Common.Messages;
using Crow.Library.Foundation.Common.Templating;
using Crow.Library.Foundation.FileOperations;
using Crow.Library.Foundation.Hosting;
using Crow.Library.InjectionContainer;
using Crow.Library.Bootstrappers;
using Crow.Library.Foundation.Logger;
using System.IO;
using System;
using Crow.Library.Foundation.Common;

namespace Crow.Library.Common
{
    /// <summary>
    /// Core definition class for Crow Fx.
    /// </summary>
    public static class CrowCore
    {
        private static bool? _IsApplicationHosted = null;
        private static IItemManager _ItemManager;

        public static bool IsApplicationHosted
        {
            get
            {
                if (_IsApplicationHosted == null || !_IsApplicationHosted.HasValue)
                {
                    _IsApplicationHosted = GetIsApplicationHosted();
                }
                return _IsApplicationHosted.Value;
            }
        }

        /// <summary>
        /// Default Dependency Injection Container.
        /// </summary>
        public static IInjectionContainer Container
        {
            get { return DIContainer.DefaultContainer; }
        }

        /// <summary>
        /// Gets the Items from the local items manager.
        /// </summary>
        public static IItemManager Items
        {
            get
            {
                _ItemManager = _ItemManager ?? DIContainer.DefaultContainer.Resolve<IItemManager>();
                return _ItemManager;
            }
        }

        /// <summary>
        /// Gets the instance of IFileOperations.
        /// </summary>
        public static IFileOperations FileOperator
        {
            get { return Container.Resolve<IFileOperations>(); }
        }
        /// <summary>
        /// Gets the instance of IConfigurationHelper
        /// </summary>
        public static IConfigurationHelper Configuration
        {
            get { return Container.Resolve<IConfigurationHelper>(); }
        }
        /// <summary>
        /// Gets the instance of IMessageService
        /// </summary>
        public static IMessagingService MessageService
        {
            get { return Container.Resolve<IMessagingService>(); }
        }
        /// <summary>
        /// Gets the instance of IAccountService
        /// </summary>
        public static IAccountService AccountService
        {
            get { return Container.Resolve<IAccountService>(); }
        }
        /// <summary>
        /// Gets the instance of ITemplateEngine
        /// </summary>
        public static ITemplateEngine Templating
        {
            get { return Container.Resolve<ITemplateEngine>(); }
        }

        /// <summary>
        /// Gets a new instance of LogService.
        /// </summary>
        public static ILog LogService
        {
            get { return Container.Resolve<ILog>(); }
        }

        private static bool GetIsApplicationHosted()
        {
            return "web.config".Equals(Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile),
                StringComparison.OrdinalIgnoreCase);
        }

    }
}