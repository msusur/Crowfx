using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Crow.Library.Foundation.Common;
using Crow.Library.DatabaseLayer.ExpressionVisitors;

namespace Crow.Library.DatabaseLayer
{
    public static class UtilExtensions
    {
        public static string GetColumnNames(this ModelDefinition modelDef)
        {
            var sqlColumns = new StringBuilder();
            modelDef.FieldDefinitions.ForEach(x =>
                sqlColumns.AppendFormat("{0}{1} ", sqlColumns.Length > 0 ? "," : "",
                  DbConfig.DialectProvider.GetQuotedColumnName(x.FieldName)));

            return sqlColumns.ToString();
        }

        internal static string GetIdsInSql(this IEnumerable idValues)
        {
            var sql = new StringBuilder();
            foreach (var idValue in idValues)
            {
                if (sql.Length > 0) sql.Append(",");
                sql.AppendFormat("{0}".SqlFormat(idValue));
            }
            return sql.Length == 0 ? null : sql.ToString();
        }

        public static string Params(this string sqlText, params object[] sqlParams)
        {
            return SqlFormat(sqlText, sqlParams);
        }

        public static string SqlFormat(this string sqlText, params object[] sqlParams)
        {
            var escapedParams = new List<string>();
            foreach (var sqlParam in sqlParams)
            {
                if (sqlParam == null)
                {
                    escapedParams.Add("NULL");
                }
                else
                {
                    var sqlInValues = sqlParam as SqlInValues;
                    if (sqlInValues != null)
                    {
                        escapedParams.Add(sqlInValues.ToSqlInString());
                    }
                    else
                    {
                        escapedParams.Add(DbConfig.DialectProvider.GetQuotedValue(sqlParam, sqlParam.GetType()));
                    }
                }
            }
            return string.Format(sqlText, escapedParams.ToArray());
        }

        public static string SqlJoin<T>(this List<T> values)
        {
            var sb = new StringBuilder();
            foreach (var value in values)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(DbConfig.DialectProvider.GetQuotedValue(value, value.GetType()));
            }

            return sb.ToString();
        }

        public static string SqlJoin(IEnumerable values)
        {
            var sb = new StringBuilder();
            foreach (var value in values)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(DbConfig.DialectProvider.GetQuotedValue(value, value.GetType()));
            }

            return sb.ToString();
        }

        public static SqlInValues SqlInValues<T>(this List<T> values)
        {
            return new SqlInValues(values);
        }

        public static SqlInValues SqlInValues<T>(this T[] values)
        {
            return new SqlInValues(values);
        }

        public static TableInfo GetModelDefinition(this Type modelType)
        {
            return new Database.PocoData(modelType).TableInfo;
        }
    }
}
