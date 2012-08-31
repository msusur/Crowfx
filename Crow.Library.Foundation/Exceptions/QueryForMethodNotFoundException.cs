using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Exceptions
{
    /// <summary>
    /// Represents the exception for method not found in the query storage.
    /// </summary>
    public class QueryForMethodNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="QueryForMethodNotFoundException"/>.
        /// </summary>
        public QueryForMethodNotFoundException(string interfaceName, string methodName)
            : base(@"Query for method '{0}' of '{1}' not found.  \r\n
                    Queries must be implemented in the related module and modules must be marked with Exports attribute."
            .FormatText(methodName, interfaceName))
        {

        }
    }
}
