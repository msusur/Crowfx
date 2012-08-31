using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Host.Configuration
{
    public interface ITypeListHost
    {
        IDictionary<string, Type> BusinessTypeList { get; set; }
    }
}
