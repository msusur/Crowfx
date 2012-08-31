using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Hosting;

namespace Crow.Library.Host.Configuration
{
    internal class HostConfiguration : IHostConfiguration
    {
        public string BaseUrl { get; set; }

        public int Port { get; set; }

        public bool IsHttps { get; set; }
    }
}
