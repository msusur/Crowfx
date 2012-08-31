using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Crow.Library.Foundation.Common.Aspects;

namespace Crow.Library.Aspects.DBOperationAttributes
{
    public class ColumnLengthRangeAttribute : DBAspectAttributeBase
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public string PropertyName { get; set; }

        public ColumnLengthRangeAttribute(int minLength, int maxLength, string propertyName)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            PropertyName = propertyName;
        }
        public ColumnLengthRangeAttribute(int maxLength, string propertyName)
            : this(0, maxLength, propertyName)
        { }

        public override void Process(IMethodInvocationContext context)
        {
            PropertyInfo pi = context.Args[0].GetType().GetProperty(PropertyName);
            object value = pi.GetValue(context.Args[0], null);

            int dataLength = value.ToString().Length;
            if (dataLength < MinLength)
            {
                context.Cancel = true;
            }
            else if (dataLength > MaxLength)
            {
                context.Cancel = true;
            }
        }
    }
}
