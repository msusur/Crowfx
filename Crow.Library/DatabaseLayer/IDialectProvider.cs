using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common;

namespace Crow.Library.DatabaseLayer
{
    public interface IDialectProvider
    {
        string GetQuotedColumnName(string columnName);

        string GetQuotedValue(object value, Type fieldType);

        string GetQuotedTableName(TableInfo modelDef);

        string GetColumnNames(Type type);

        string GetParameterString();
    }
}
