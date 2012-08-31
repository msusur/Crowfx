using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Tests.Benchmarks.Application
{
    public interface IBenchmarkBusiness
    {
        void SaveItem();

        IList<BenchmarkData> GetItems();

        void TruncateTable();
    }
}
