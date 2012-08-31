using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.InjectionContainer;
using Crow.Library.RepositoryFactory;
using Crow.Library.DatabaseLayer;
using Crow.Library.RepositoryStorage;
using Crow.Library.Foundation.Logger;

namespace Crow.Library.Tests.Benchmarks.Application
{
    public class BusinessClass : IBenchmarkBusiness
    {
        private IBenchmarkRepository _repository;

        public BusinessClass()
            : this(Repository.Get<IBenchmarkRepository>())
        { }

        public BusinessClass(IBenchmarkRepository repository)
        {
            _repository = repository;
        }

        public virtual void SaveItem()
        {
            BenchmarkData data = new BenchmarkData
            {
                Name = "John",
                Lastname = "Snow",
                Birthdate = DateTime.Now.Subtract(TimeSpan.FromDays(459))
            };
            _repository.InsertBenchmarkData(data);
        }

        public IList<BenchmarkData> GetItems()
        {
            return _repository.GetAllData().ToList();
        }

        public void TruncateTable()
        {
            _repository.Truncate();
        }
    }
}
