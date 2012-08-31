using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.EventHandlers
{
    public class EventExecutionEventArgs : EventArgs
    {
        private List<Argument> m_Arguments;

        public List<Argument> Arguments
        {
            get
            {
                m_Arguments = m_Arguments ?? new List<Argument>();
                return m_Arguments;
            }
            set { m_Arguments = value; }
        }

        public TArgumentType GetArgumentByName<TArgumentType>(string key)
        {
            return (TArgumentType)(from arg in Arguments
                                   where arg.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)
                                   select arg.Value).FirstOrDefault();
        }

        public EventExecutionEventArgs()
            : base()
        { }
    }
}