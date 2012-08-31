using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Bootstrapper;
using System.Reflection;

namespace Crow.Library.Bootstrappers
{
    public class DefaultAssemblyLoader : IAssemblyLoader
    {
        public IEnumerable<System.Reflection.Assembly> LoadAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}
