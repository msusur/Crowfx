using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Crow.Library.Bootstrappers;

namespace Crow.Library.Common
{
    internal static class AttributesHelper
    {
        internal static TAttributeType GetAttribute<TAttributeType>(IInvocation invocation)
        {
            return (TAttributeType)GetAttribute(invocation, typeof(TAttributeType));
        }
        internal static object GetAttribute(IInvocation invocation, Type attributeType)
        {
            var attribute = GetAttributes(invocation, attributeType).FirstOrDefault();
            return attribute;
        }
        internal static IEnumerable<object> GetAttributes(IInvocation invocation, Type attributeType)
        {
            return invocation.InvocationTarget.GetType().GetInterfaces()[0].GetMethod(invocation.Method.Name).GetCustomAttributes(attributeType, true);
        }

        internal static AttributeContext<TAttributeType> HasAttribute<TAttributeType>(object instance)
            where TAttributeType : Attribute
        {
            object[] attributes = instance.GetType().GetCustomAttributes(typeof(TAttributeType), true);
            if (attributes == null || attributes.Length == 0)
            {
                return new AttributeContext<TAttributeType>();
            }
            return new AttributeContext<TAttributeType>(attributes.Cast<TAttributeType>().FirstOrDefault<TAttributeType>());
        }
    }
}
