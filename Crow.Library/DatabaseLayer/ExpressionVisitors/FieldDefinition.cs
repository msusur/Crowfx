using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.DatabaseLayer.ExpressionVisitors
{
    public class FieldDefinition
    {
        public string Name { get; set; }

        public string Alias { get; set; }

        public string FieldName
        {
            get { return this.Alias ?? this.Name; }
        }

        public Type FieldType { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool AutoIncrement { get; set; }

        public bool IsNullable { get; set; }

        public bool IsIndexed { get; set; }

        public bool IsUnique { get; set; }

        public int? FieldLength { get; set; }  // Precision for Decimal Type

        public int? Scale { get; set; }  //  for decimal type

        public string DefaultValue { get; set; }

        public Type ReferencesType { get; set; }


        //public object GetValue(object onInstance)
        //{
        //    return this.GetValueFn == null ? null : this.GetValueFn(onInstance);
        //}

        //public void SetValue(object onInstance, object withValue)
        //{
        //    if (this.SetValueFn == null) return;

        //    var convertedValue = DbConfig.DialectProvider.ConvertDbValue(withValue, this.FieldType);
        //    SetValueFn(onInstance, convertedValue);
        //}

        //public string GetQuotedValue(object fromInstance)
        //{
        //    var value = GetValue(fromInstance);
        //    return DbConfig.DialectProvider.GetQuotedValue(value, FieldType);
        //}

        public string Sequence { get; set; }

        public bool IsComputed { get; set; }

        public string ComputeExpression { get; set; }


        public System.Reflection.PropertyInfo PropertyInfo { get; set; }
    }
}
