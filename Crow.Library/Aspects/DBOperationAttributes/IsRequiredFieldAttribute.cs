using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Crow.Library.Aspects.DBOperationAttributes
{
    public class IsRequiredFieldAttribute : DBAspectAttributeBase
    {
        public string PropertyName { get; set; }

        public IsRequiredFieldAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public override void Process(Foundation.Common.Aspects.IMethodInvocationContext context)
        {
            PropertyInfo pi = context.Args[0].GetType().GetProperty(PropertyName);
            object value = pi.GetValue(context.Args[0], null);

            if (value == null)
                context.Cancel = true;
        }
    }
}
