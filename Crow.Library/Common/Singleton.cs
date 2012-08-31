using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.InjectionContainer;

namespace Crow.Library.Common
{
    internal class Singleton
    {
        private static readonly Lazy<Singleton> s_Lazy = new Lazy<Singleton>(() => new Singleton(), true);
        private static object s_LockObject = new object();
        protected Singleton()
        {
        }

        public static TResolvedType Get<TResolvedType>()
        {
            if (s_Lazy.Value.ContainsType(typeof(TResolvedType)))
            {
                return (TResolvedType)s_Lazy.Value.GetType(typeof(TResolvedType));
            }
            else
            {
                return default(TResolvedType);
            }
        }
        internal static void Put<TObjectType>(object @object)
        {
            s_Lazy.Value.SetType(typeof(TObjectType), @object);
        }
        internal static TObjectType PutFromDIContainer<TObjectType>()
        {
            TObjectType @object = DIContainer.DefaultContainer.Resolve<TObjectType>();
            Put<TObjectType>(@object);
            return @object;
        }

        private Dictionary<Type, object> m_List = new Dictionary<Type, object>();

        private bool ContainsType(Type type)
        {
            return this.m_List.ContainsKey(type);
        }
        private object GetType(Type type)
        {
            if (this.ContainsType(type))
            {
                return this.m_List[type];
            }
            return null;
        }
        private void SetType(Type type, object @object)
        {
            bool hasType = ContainsType(type);
            lock (s_LockObject)
            {//TODO: more thread-safe
                if (hasType)
                {
                    this.m_List[type] = @object;
                }
                else
                {
                    this.m_List.Add(type, @object);
                }
            }
        }
    }
}