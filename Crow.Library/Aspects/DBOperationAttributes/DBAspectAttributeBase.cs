using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Aspects;

namespace Crow.Library.Aspects.DBOperationAttributes
{
    /// <summary>
    /// Base class for database related attributes.
    /// </summary>
    public abstract class DBAspectAttributeBase : Attribute
    {
        /// <summary>
        /// Executes the aspect.
        /// </summary>
        /// <param name="context"></param>
        public abstract void Process(IMethodInvocationContext context);
    }
}