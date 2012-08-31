using Crow.Library.Bootstrappers;
using Crow.Library.Foundation.Common.Account;
using Crow.Library.Foundation.Common.Configuration;
using Crow.Library.Foundation.Common.Messages;
using Crow.Library.Foundation.FileOperations;
using Crow.Library.InjectionContainer;

namespace Grail.Library.Common
{
    public static class GrailCore
    {
        public static IFileOperations FileOperator
        {
            get { return DIContainer.DefaultContainer.Resolve<IFileOperations>(); }
        }
        public static IConfigurationHelper Configuration
        {
            get { return DIContainer.DefaultContainer.Resolve<IConfigurationHelper>(); }
        }
        public static IMessagingService MessageService
        {
            get { return DIContainer.DefaultContainer.Resolve<IMessagingService>(); }
        }
        public static IAccountService AccountService
        {
            get { return DIContainer.DefaultContainer.Resolve<IAccountService>(); }
        }

        public static void RegisterStartup(IStartupInstaller installer)
        {
            Bootstrapper.Application.RegisterStartup(installer);
        }
        public static void Start()
        {
            Bootstrapper.Application.Start();
        }

        public static void RegisterOnDemand(IStartupInstaller installer)
        {
            Bootstrapper.Application.RegisterOnDemand(installer);
        }
    }
}