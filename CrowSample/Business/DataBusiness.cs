using ConsoleApplication1.Repositories;
using Crow.Library.RepositoryFactory;
using Crow.Library.DatabaseLayer;

namespace ConsoleApplication1.Businesses
{
    public class DataBusinessClass : IDataBusiness
    {
        public Data GetDataById(int id)
        {
            using (DbConnectionScope scope = new DbConnectionScope())
            {
                return Repository.Get<IDataRepository>().LoadDataById(id);
            }
        }
    }
}