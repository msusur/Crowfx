using System;
using System.Collections;
using Crow.Library.Common;

namespace Crow.Library.Common
{
    public sealed class ThreadItemManager : ItemManagerBase
    {
        [ThreadStatic]
        private static Hashtable _State;

        private static Hashtable State
        {
            get
            {
                if (_State == null)
                {
                    _State = new Hashtable();
                }
                return _State;
            }
        }

        /// <summary>
        /// Gets the specified key from Http Local State.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override object Get(object key)
        {
            return State[key];
        }

        /// <summary>
        /// Puts the specified key to the Http Local State.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="object">The object.</param>
        public override void Add(object key, object @object)
        {
            State[key] = @object;
        }

        /// <summary>
        /// Removes the specified key from Http Local State.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void Remove(object key)
        {
            State.Remove(key);
        }

        /// <summary>
        /// Clears all the states.
        /// </summary>
        public override void Clear()
        {
            State.Clear();
        }
    }
}