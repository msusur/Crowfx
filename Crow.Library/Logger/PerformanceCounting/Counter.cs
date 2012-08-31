using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Logger.PerformanceCounting
{
    public sealed class Counter
    {
        public long Duration { get; set; }
        public string Key { get; set; }
    }
}
