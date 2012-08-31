using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Common;

namespace Crow.Library.EventHandlers
{
    public delegate void OnEventExecutedHandler(object sender, EventExecutionEventArgs args);
    public class EventsService
    {
        private class EventCall
        {
            public string EventKey { get; set; }
            public OnEventExecutedHandler Handler { get; set; }

            public EventCall(string key, OnEventExecutedHandler method)
            {
                this.EventKey = key;
                this.Handler = method;
            }
        }

        static EventsService()
        {
            Singleton.Put<EventsService>(new EventsService());
        }
        public static EventsService Current
        {
            get { return Singleton.Get<EventsService>(); }
        }

        private List<EventCall> m_Events = new List<EventCall>();

        public void CallEvent(object sender, string eventName, params Argument[] parameters)
        {
            foreach (var item in this.m_Events.Where((e) => e.EventKey.Equals(eventName, StringComparison.InvariantCultureIgnoreCase)))
            {
                item.Handler(sender, new EventExecutionEventArgs { Arguments = parameters.ToList() });
            }
        }
        public void AttachEvent(string key, OnEventExecutedHandler method)
        {
            this.m_Events.Add(new EventCall(key, method));
        }

        private IEnumerable<EventCall> FindEventByName(string name)
        {
            return (from e in this.m_Events
                    where e.EventKey.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                    select e);
        }
    }
}