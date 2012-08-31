using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Hosting
{
    public interface IHostConfiguration
    {
        string BaseUrl { get; set; }
        int Port { get; set; }
        bool IsHttps { get; set; }
    }
}
