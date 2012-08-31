using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.DatabaseLayer.DataAttributes;
using Crow.Library.Interceptors.DatabaseInceptors.DbOperations;

namespace Crow.Library.Tests.Benchmarks.Application
{
    public interface IBenchmarkRepository
    {
        IEnumerable<BenchmarkData> GetAllData();

        [Insert]
        void InsertBenchmarkData(BenchmarkData data);

        [Scalar]
        void Truncate();
    }
}
