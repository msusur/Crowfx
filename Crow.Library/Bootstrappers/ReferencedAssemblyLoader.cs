using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Bootstrapper;
using System.Reflection;

namespace Crow.Library.Bootstrappers
{
    internal class ReferencedAssemblyLoader : IAssemblyLoader
    {
        public IEnumerable<Assembly> LoadAssemblies()
        {
            AssemblyName[] referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (AssemblyName asm in referencedAssemblies)
            {
                yield return AppDomain.CurrentDomain.GetAssemblies().Where((a) => a.FullName == asm.FullName).First();
            }
        }
    }
}
