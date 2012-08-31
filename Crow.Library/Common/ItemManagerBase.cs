using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common;

namespace Crow.Library.Common
{
    public abstract class ItemManagerBase : IItemManager
    {
        public abstract object Get(object key);
        public abstract void Add(object key, object @object);
        public abstract void Remove(object key);
        public abstract void Clear();

        /// <summary>
        /// Gets the specified key from Http Local State.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual T Get<T>(object key)
        {
            return (T)Get(key);
        }

        /// <summary>
        /// Gets the specified key from Http Local State.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public virtual T Get<T>(object key, T defaultValue)
        {
            object value = Get(key);
            if (value == null)
            {
                return defaultValue;
            }
            return (T)value;
        }

        private static readonly object s_LockObject = new object();
        /// <summary>
        /// Gets the or add.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="objectCreator">The object creator.</param>
        /// <returns></returns>
        public T GetOrAdd<T>(object key, Func<T> objectCreator)
        {
            object value = Get(key);
            if (value == null)
            {
                lock (s_LockObject)
                {
                    value = Get(key);
                    if (value == null)
                    {
                        value = objectCreator();
                        Add(key, value);
                    }
                }

            }
            return (T)value;
        }
    }
}
