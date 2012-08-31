using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Crow.Library.Host.Controllers
{
    internal class RequestParameters : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly Dictionary<string, object> _innerDictionary = new Dictionary<string, object>();

        public object this[string name]
        {
            get { return _innerDictionary[name]; }
            set { _innerDictionary[name] = value; }
        }

        public RequestParameters()
        {

        }

        public void AddParameter(string name, object value)
        {
            _innerDictionary[name] = value;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

        internal object[] ToArray()
        {
            return _innerDictionary.Values.ToArray<object>();
        }
    }
}
