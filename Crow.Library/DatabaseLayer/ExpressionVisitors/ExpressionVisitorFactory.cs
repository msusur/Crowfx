using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.DatabaseLayer.ExpressionVisitors
{
    class ExpressionVisitorFactory
    {
        internal static SqlExpressionVisitor<TReturnType> Create<TReturnType>()
        {
            return new DefaultSqlExpressionVisitor<TReturnType>();
        }
    }
}
