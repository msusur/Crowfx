using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Crow.Library.DatabaseLayer
{
    public class SqlInValues
    {
        private readonly IEnumerable values;

        public SqlInValues(IEnumerable values)
        {
            this.values = values;
        }

        public string ToSqlInString()
        {
            return UtilExtensions.SqlJoin(values);
        }
    }
}
