using System;
using ConsoleApplication1.Businesses;
using ConsoleApplication1.Initializations;
using Crow.Library.Bootstrappers;
using Crow.Library.Foundation.Hosting;
using Crow.Library.Host;
using Crow.Library.Common;
using Crow.Library.BusinessFactory;
using Crow.Library.RepositoryFactory;
using ConsoleApplication1.Repositories;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper bootstrapper = new CustomBootstrapper();
            bootstrapper.Start(typeof(CustomBootstrapper).Assembly);

            using (ICrowHttpHost host = CrowHost.ConfigureHost(CrowCore.Container).Host<IDataBusiness>().Start())
            {
                Console.WriteLine("Press enter to stop application.");
                Console.Read();
            }
        }
    }
}