using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Common
{
    /// <summary>
    /// Represents the local storage. 
    /// </summary>
    public interface IItemManager
    {
        /// <summary>
        /// Gets the object with specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object Get(object key);

        /// <summary>
        /// Gets the @object with specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Get<T>(object key);

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        T Get<T>(object key, T defaultValue);

        /// <summary>
        /// Gets the object or adds using object creator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="objectCreator">The object creator.</param>
        /// <returns></returns>
        T GetOrAdd<T>(object key, Func<T> objectCreator);

        /// <summary>
        /// Adds the object with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="object">The @object.</param>
        void Add(object key, object @object);

        /// <summary>
        /// Removes the object with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(object key);

        /// <summary>
        /// Clears all the local states.
        /// </summary>
        void Clear();
    }
}
