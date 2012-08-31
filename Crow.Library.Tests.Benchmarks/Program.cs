using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Bootstrappers;
using Crow.Library.RepositoryStorage;
using System.ComponentModel.Composition;
using Crow.Library.DatabaseLayer.DataAttributes;
using Crow.Library.Tests.Benchmarks.Application;
using Crow.Library.InjectionContainer;
using Crow.Library.Common;
using Crow.Library.BusinessFactory;
using Crow.Library.Logger.PerformanceCounting;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.Foundation.Logger;

namespace Crow.Library.Tests.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper boot = new Bootstrapper();
            boot.Start();


            IBenchmarkBusiness business = Business.Get<IBenchmarkBusiness>();

            business.TruncateTable();

            using (Performance perf = new Performance("insert test"))
            {
                for (int i = 0; i < 5000; i++)
                {
                    business.SaveItem();
                }
            }

            IList<BenchmarkData> dataList;
            using (Performance perf = new Performance("Select Test"))
            {
                for (int i = 0; i < 5000; i++)
                {
                    dataList = business.GetItems();
                }
            }

            StringBuilder builder = new StringBuilder();
            Performance.WriteAndClear(builder);

            Console.WriteLine(builder.ToString());

            business.TruncateTable();

            Console.Read();
        }


    }

    [Export(typeof(IModule))]
    public class BenchmarkInstaller : IModule
    {
        public void Install(IInjectionContainer container, IQueryStore queryStore)
        {
            queryStore.ConnectionString = new Foundation.Common.ConnectionStringInformation("Data source=.;User Id=sa;Password=Aa123456;Initial Catalog=Test");
            queryStore.Expression<IBenchmarkRepository, BenchmarkData>("GetAllData").Select().AddToStore();
            queryStore.AddCommand<IBenchmarkRepository>("Truncate", "truncate table Test");
            container.Bind<IBenchmarkBusiness, BusinessClass>();
        }
    }
}