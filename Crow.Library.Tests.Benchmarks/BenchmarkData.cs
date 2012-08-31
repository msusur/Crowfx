using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.DatabaseLayer;

namespace Crow.Library.Tests.Benchmarks
{
    [TableName("Benchmark")]
    [PrimaryKey("Id")]
    public class BenchmarkData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
