using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Common
{
    internal sealed class AttributeContext<TAttributeType>
        where TAttributeType : Attribute
    {
        public TAttributeType AttributeInstance { get; private set; }


        public bool HasAttribute { get; set; }

        public AttributeContext()
            : this(default(TAttributeType))
        {
        }
        public AttributeContext(TAttributeType attribute)
        {
            AttributeInstance = attribute;
        }
    }
}
