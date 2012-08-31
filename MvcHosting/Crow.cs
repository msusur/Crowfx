using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Crow.Library.Foundation.Bootstrapper;
using System.ComponentModel.Composition;
using Crow.Library.InjectionContainer;
using Crow.Library.Common;
using Crow.Library.Host.AspNet;
using Crow.Library.RepositoryFactory;
using Crow.Library.DatabaseLayer.DataAttributes;
using Crow.Library.DatabaseLayer;

namespace MvcHosting
{
    [Export(typeof(IModule))]
    public class Module : IModule
    {
        public void Install(IInjectionContainer container, IQueryStore queryStore)
        {
            container.Bind<IBusiness, DataBusiness>();
            queryStore.ConnectionString = new Crow.Library.Foundation.Common.ConnectionStringInformation("Data Source=.;Initial Catalog=Test;User Id=sa;Password=Aa123456");

            queryStore.Expression<IDataRepository, Data>("GetDataById").Where<int>((m, id) => m.Id == id).AddToStore();
        }
    }

    [Export(typeof(IHostBusiness))]
    public class DataBusiness : IBusiness, IHostBusiness
    {
        private readonly IDataRepository _repository = Repository.Get<IDataRepository>();

        public Data GetDataById(int id)
        {
            using (DbConnectionScope scope = new DbConnectionScope())
            {
                return _repository.GetDataById(id);
            }
        }
    }

    public interface IBusiness
    {
        Data GetDataById(int id);
    }

    public interface IDataRepository
    {
        [Insert]
        void Save(Data dt);

        Data GetDataById(int id);
    }
    public class Data
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}