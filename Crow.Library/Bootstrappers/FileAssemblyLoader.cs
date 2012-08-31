using System.Collections.Generic;
using System.Reflection;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.Foundation.Common.Helpers;

namespace Crow.Library.Bootstrappers
{
    internal class FileAssemblyLoader : IAssemblyLoader
    {
        public IEnumerable<Assembly> LoadAssemblies()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            return AssemblyHelper.LoadAssembliesFromPath(path);
        }
    }
}
