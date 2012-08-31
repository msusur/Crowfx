using System.Collections.Generic;
using System.Reflection;

namespace Crow.Library.Foundation.Bootstrapper
{
    public interface IAssemblyLoader
    {
        IEnumerable<Assembly> LoadAssemblies();
    }
}
