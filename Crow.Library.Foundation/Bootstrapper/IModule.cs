using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.InjectionContainer;
using Crow.Library.Common;

namespace Crow.Library.Foundation.Bootstrapper
{
    /// <summary>
    /// Represents the interface for StartupInstallers
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Starts the initialization for the current startup installer.
        /// </summary>
        void Install(IInjectionContainer container, IQueryStore queryStore);
    }
}
