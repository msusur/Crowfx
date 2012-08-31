using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.InjectionContainer;

namespace Crow.Library.DatabaseLayer
{
    internal static class DbConfig
    {
        public static IDialectProvider DialectProvider
        {
            get { return DIContainer.DefaultContainer.Resolve<IDialectProvider>(); }
        }
    }
}
