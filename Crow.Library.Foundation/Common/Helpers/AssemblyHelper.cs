using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Crow.Library.Foundation.Common.Helpers
{
    public class AssemblyHelper
    {
        public static IEnumerable<Assembly> LoadAssembliesFromPath(string path)
        {
            path = Path.GetDirectoryName(path);
            string[] files = Directory.GetFiles(path, "*.dll");

            foreach (var file in files)
            {
                yield return Assembly.LoadFile(file);
            }
        }
    }
}
