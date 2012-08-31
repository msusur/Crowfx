using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Crow.Library.Logger.PerformanceCounting
{
    public sealed class Performance : IDisposable
    {
        private Stopwatch watch = new Stopwatch();
        private string _key;

        public static readonly List<Counter> Counters = new List<Counter>();

        public Performance(string key)
        {
            watch.Start();
            _key = key;
        }
        public void Dispose()
        {
            watch.Stop();
            Counters.Add(new Counter
            {
                Duration = watch.ElapsedMilliseconds,
                Key = _key
            });
        }

        public static void WriteAndClear(StringBuilder builder)
        {
            foreach (var item in Counters)
            {
                builder.AppendFormat("{0} : {1} ms.", item.Key , item.Duration);
            }
            Counters.Clear();
        }
    }
}
