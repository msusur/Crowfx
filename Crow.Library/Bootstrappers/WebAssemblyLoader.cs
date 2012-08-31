using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Bootstrapper;
using System.Reflection;
using Crow.Library.Foundation.Common.Helpers;
using System.IO;

namespace Crow.Library.Bootstrappers
{
    public class WebAssemblyLoader : IAssemblyLoader
    {
        public IEnumerable<Assembly> LoadAssemblies()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\");
            return AssemblyHelper.LoadAssembliesFromPath(path);
        }
    }
}
