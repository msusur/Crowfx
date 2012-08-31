using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.DatabaseLayer.ExpressionVisitors;

namespace Crow.Library.RepositoryStorage
{
    /// <summary>
    /// Expression Visitor wrapper.
    /// </summary>
    /// <typeparam name="TReturnType"></typeparam>
    public class ExpressionVisitor<TReturnType> : ISqlExpressionVisitor<TReturnType>
    {
        private readonly SqlExpressionVisitor<TReturnType> _visitor;

        internal ExpressionVisitor(SqlExpressionVisitor<TReturnType> visitor)
        {
            _visitor = visitor;
        }

        public void AddToStore()
        {
            _visitor.AddToStore();
        }

        public IList<string> GetAllFields()
        {
            return _visitor.GetAllFields();
        }

        public ISqlExpressionVisitor<TReturnType> GroupBy()
        {
            return _visitor.GroupBy();
        }

        public ISqlExpressionVisitor<TReturnType> GroupBy(string groupBy)
        {
            return _visitor.GroupBy(groupBy);
        }

        public ISqlExpressionVisitor<TReturnType> GroupBy<TKey>(System.Linq.Expressions.Expression<Func<TReturnType, TKey>> keySelector)
        {
            return _visitor.GroupBy<TKey>(keySelector);
        }

        public string GroupByExpression
        {
            get { return _visitor.GroupByExpression; }
            set { _visitor.GroupByExpression = value; }
        }

        public ISqlExpressionVisitor<TReturnType> Having()
        {
            return _visitor.Having();
        }

        public ISqlExpressionVisitor<TReturnType> Having(System.Linq.Expressions.Expression<Func<TReturnType, bool>> predicate)
        {
            return _visitor.Having(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Having(string sqlFilter, params object[] filterParams)
        {
            return _visitor.Having(sqlFilter, filterParams);
        }

        public string HavingExpression
        {
            get { return _visitor.HavingExpression; }
            set { _visitor.HavingExpression = value; }
        }

        public ISqlExpressionVisitor<TReturnType> Limit()
        {
            return _visitor.Limit();
        }

        public ISqlExpressionVisitor<TReturnType> Limit(int rows)
        {
            return _visitor.Limit(rows);
        }

        public ISqlExpressionVisitor<TReturnType> Limit(int skip, int rows)
        {
            return _visitor.Limit(skip, rows);
        }

        public string LimitExpression
        {
            get { return _visitor.LimitExpression; }
        }

        public string MethodName
        {
            get { return _visitor.MethodName; }
            set { _visitor.MethodName = value; }
        }

        public ISqlExpressionVisitor<TReturnType> OrderBy()
        {
            return _visitor.OrderBy();
        }

        public ISqlExpressionVisitor<TReturnType> OrderBy(string orderBy)
        {
            return _visitor.OrderBy(orderBy);
        }

        public ISqlExpressionVisitor<TReturnType> OrderBy<TKey>(System.Linq.Expressions.Expression<Func<TReturnType, TKey>> keySelector)
        {
            return _visitor.OrderBy<TKey>(keySelector);
        }

        public ISqlExpressionVisitor<TReturnType> OrderByDescending<TKey>(System.Linq.Expressions.Expression<Func<TReturnType, TKey>> keySelector)
        {
            return _visitor.OrderByDescending<TKey>(keySelector);
        }

        public string OrderByExpression
        {
            get { return _visitor.OrderByExpression; }
            set { _visitor.OrderByExpression = value; }
        }

        public Type RepositoryType
        {
            get { return _visitor.RepositoryType; }
            set { _visitor.RepositoryType = value; }
        }

        public int? Rows
        {
            get { return _visitor.Rows; }
            set { _visitor.Rows = value; }
        }

        public ISqlExpressionVisitor<TReturnType> Select()
        {
            return _visitor.Select();
        }

        public ISqlExpressionVisitor<TReturnType> Select(string selectExpression)
        {
            return _visitor.Select(selectExpression);
        }

        public ISqlExpressionVisitor<TReturnType> Select<TKey>(System.Linq.Expressions.Expression<Func<TReturnType, TKey>> fields)
        {
            return _visitor.Select<TKey>(fields);
        }

        public ISqlExpressionVisitor<TReturnType> SelectDistinct<TKey>(System.Linq.Expressions.Expression<Func<TReturnType, TKey>> fields)
        {
            return _visitor.SelectDistinct(fields);
        }

        public string SelectExpression
        {
            get { return _visitor.SelectExpression; }
            set { _visitor.SelectExpression = value; }
        }

        public int? Skip
        {
            get { return _visitor.Skip; }
            set { _visitor.Skip = value; }
        }

        public string ToSelectStatement()
        {
            return _visitor.ToSelectStatement();
        }

        public ISqlExpressionVisitor<TReturnType> Where()
        {
            return _visitor.Where();
        }

        public ISqlExpressionVisitor<TReturnType> Where(System.Linq.Expressions.Expression<Func<TReturnType, bool>> predicate)
        {
            return _visitor.Where(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Where(string sqlFilter, params object[] filterParams)
        {
            return _visitor.Where(sqlFilter, filterParams);
        }

        public ISqlExpressionVisitor<TReturnType> Where<T1, T2, T3, T4, T5, T6, T7, T8>(System.Linq.Expressions.Expression<Func<TReturnType, T1, T2, T3, T4, T5, T6, T7, T8, bool>> predicate)
        {
            return _visitor.Where<T1, T2, T3, T4, T5, T6, T7, T8>(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Where<T1, T2, T3, T4, T5, T6, T7>(System.Linq.Expressions.Expression<Func<TReturnType, T1, T2, T3, T4, T5, T6, T7, bool>> predicate)
        {
            return _visitor.Where<T1, T2, T3, T4, T5, T6, T7>(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Where<T1, T2, T3, T4, T5, T6>(System.Linq.Expressions.Expression<Func<TReturnType, T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            return _visitor.Where<T1, T2, T3, T4, T5, T6>(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Where<T1, T2, T3, T4, T5>(System.Linq.Expressions.Expression<Func<TReturnType, T1, T2, T3, T4, T5, bool>> predicate)
        {
            return _visitor.Where<T1, T2, T3, T4, T5>(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Where<T1, T2, T3, T4>(System.Linq.Expressions.Expression<Func<TReturnType, T1, T2, T3, T4, bool>> predicate)
        {
            return _visitor.Where<T1, T2, T3, T4>(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Where<T1, T2, T3>(System.Linq.Expressions.Expression<Func<TReturnType, T1, T2, T3, bool>> predicate)
        {
            return _visitor.Where<T1, T2, T3>(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Where<T1, T2>(System.Linq.Expressions.Expression<Func<TReturnType, T1, T2, bool>> predicate)
        {
            return _visitor.Where<T1, T2>(predicate);
        }

        public ISqlExpressionVisitor<TReturnType> Where<T1>(System.Linq.Expressions.Expression<Func<TReturnType, T1, bool>> predicate)
        {
            return _visitor.Where<T1>(predicate);
        }

        public string WhereExpression
        {
            get { return _visitor.WhereExpression; }
            set { _visitor.WhereExpression = value; }
        }
    }
}
