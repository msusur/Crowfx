using System.ComponentModel.Composition;
using Crow.Library.Common;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.Foundation.Common;
using Crow.Library.InjectionContainer;
using ConsoleApplication1.Businesses;
using ConsoleApplication1.Repositories;
using Crow.Library.Bootstrappers;

namespace ConsoleApplication1.Initializations
{
    [Export(typeof(IModule))]
    //[DependsOn(typeof(CrowModule))]
    public class SampleModule : IModule
    {
        public void Install(IInjectionContainer container, IQueryStore queryStore)
        {
            string provider = "System.Data.SqlClient";
            string connectionString = "Data Source=.;Initial Catalog=Test;User Id=sa;Password=Aa123456";
            queryStore.ConnectionString = new ConnectionStringInformation(connectionString, provider);
            queryStore.Expression<IDataRepository, Data>("LoadDataById")
                         .Select()
                         .Where<int>((data, id) => data.Id == id)
                         .OrderBy((d) => d.Id)
                         .AddToStore();
            container.Bind<IDataBusiness, DataBusinessClass>();
        }
    }
}
