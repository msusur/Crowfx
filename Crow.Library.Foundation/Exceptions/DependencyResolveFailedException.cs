using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Exceptions
{
    public class DependencyResolveFailedException : Exception
    {
        public DependencyResolveFailedException(Type dependencyType)
            : base(string.Format("Dependency for type '{0}' not found.", dependencyType.FullName))
        {
        }
    }
}
