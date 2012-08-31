using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.DatabaseLayer
{
    public class SqlDialectProvider : DialectProviderBase<SqlDialectProvider>
    {
        internal const string DefaultGuidDefinition = "VARCHAR(37)";

        public SqlDialectProvider()
        {
            base.BoolColumnDefinition = base.IntColumnDefinition;
            base.GuidColumnDefinition = DefaultGuidDefinition;
            base.AutoIncrementDefinition = string.Empty;
            base.DateTimeColumnDefinition = "TIMESTAMP";
            base.TimeColumnDefinition = "TIME";
            base.RealColumnDefinition = "FLOAT";
            base.DefaultStringLength = 128;
            base.InitColumnTypeMap();
        }

        public override string GetQuotedColumnName(string columnName)
        {
            return string.Format("[{0}]", columnName);
        }

        public override string GetQuotedValue(object value, Type fieldType)
        {
            if (value == null)
            {
                return "NULL";
            }

            //if (fieldType == typeof(Guid))
            //{
            //    if (CompactGuid)
            //        return "X'" + ((Guid)value).ToString("N") + "'";
            //    else
            //        return string.Format("CAST('{0}' AS {1})", (Guid)value, DefaultGuidDefinition);  // TODO : check this !!!
            //}
            if (fieldType == typeof(DateTime) || fieldType == typeof(DateTime?))
            {
                var dateValue = (DateTime)value;
                string iso8601Format = dateValue.ToString("yyyy-MM-dd HH:mm:ss.fff").EndsWith("00:00:00.000") ?
                        "yyyy-MM-dd"
                        : "yyyy-MM-dd HH:mm:ss.fff";
                return base.GetQuotedValue(dateValue.ToString(iso8601Format), typeof(string));
            }
            if (fieldType == typeof(bool?) || fieldType == typeof(bool))
            {
                var boolValue = (bool)value;
                return base.GetQuotedValue(boolValue ? "1" : "0", typeof(string));
            }

            if (fieldType == typeof(decimal?) || fieldType == typeof(decimal) ||
                fieldType == typeof(double?) || fieldType == typeof(double) ||
                fieldType == typeof(float?) || fieldType == typeof(float))
            {
                var s = base.GetQuotedValue(value, fieldType);
                if (s.Length > 20) s = s.Substring(0, 20);
                return "'" + s + "'"; // when quoted exception is more clear!
            }

            return base.GetQuotedValue(value, fieldType);
        }
    }
}
