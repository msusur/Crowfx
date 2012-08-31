using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.Common;

namespace Crow.Library.Bootstrappers
{
    internal static class AssemblyLoaderFactory
    {
        internal static IAssemblyLoader GetLoader()
        {
            if (CrowCore.IsApplicationHosted)
            {
                return new WebAssemblyLoader();
            }
            return new FileAssemblyLoader();
        }
    }
}
