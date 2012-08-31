using System.Collections.Generic;
using System.Reflection;
using Crow.Library.Bootstrappers;

namespace ConsoleApplication1.Initializations
{
    public class CustomBootstrapper : Bootstrapper
    {
        public CustomBootstrapper()
        {

        }

        protected override void LoadModules(IEnumerable<Assembly> assemblies)
        {
            base.LoadModules(assemblies);
        }

        protected override void UnloadModules()
        {
            base.UnloadModules();
        }
    }
}
