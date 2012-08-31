using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.RepositoryStorage
{
    public class QueryCommand
    {
        public string Query { get; set; }

        public QueryCommand(string query)
        {
            this.Query = query;
        }
    }
}
