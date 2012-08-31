using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Bootstrapper;
using System.Reflection;
using System.Web.Compilation;

namespace Crow.Library.Host.AspNet
{
    internal class AspnetAssemblyLoader : IAssemblyLoader
    {
        public IEnumerable<Assembly> LoadAssemblies()
        {
            return BuildManager.GetReferencedAssemblies().Cast<Assembly>();
        }
    }
}
