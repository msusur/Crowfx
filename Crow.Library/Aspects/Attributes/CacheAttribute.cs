using System.Reflection;
using System.Linq;
using Crow.Library.Foundation.Common.Aspects;
using Crow.Library.Foundation.Common.Caching;
using Crow.Library.InjectionContainer;
using System;
using System.Text;

namespace Crow.Library.Aspects.Attributes
{
    /// <summary>
    /// Caches the return of the method that decorated with attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CacheAttribute : AspectAttributeBase
    {
        /// <summary>
        /// Gets or sets if the cache is active.
        /// </summary>
        public bool AllowCache { get; set; }
        /// <summary>
        /// Gets or sets the cache key pattern.
        /// </summary>
        public string CacheKeyPattern { get; set; }
        /// <summary>
        /// Gets or sets the duration of the key stay in cache.
        /// </summary>
        public int CacheDuration { get; set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CacheAttribute()
            : base()
        {
        }

        /// <summary>
        /// Method that is going to be executed before the method is executed.
        /// "Dynamically use only do not call this method."
        /// </summary>
        /// <param name="context"></param>
        [WorksBefore, Obsolete("Dynamically use only do not call this method.", true)]
        public void BeforeMethodExecuted(IMethodInvocationContext context)
        {
            ICacheManager manager = DIContainer.DefaultContainer.Resolve<ICacheManager>();
            string key = GetCacheKey(context);
            context.ReturnValue = manager.GetOrAdd<object>(key, () => context.Proceed(), TimeSpan.FromMinutes(CacheDuration));
            context.Cancel = true;
            context.IsMethodExecuted = true;
        }

        private string GetCacheKey(IMethodInvocationContext context)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < context.Args.Length; i++)
            {
                builder.AppendFormat("{0};", context.Args[i].ToString());
            }
            return string.Format("{0}:{1}:{2}", context.ProxyType, context.Method.Name, builder.ToString());
        }
    }
}
