using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.EventHandlers
{
    public class Argument
    {
        public Argument(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
