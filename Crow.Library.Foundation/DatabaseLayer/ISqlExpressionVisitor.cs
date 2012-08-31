using System;
using System.Linq.Expressions;
namespace Crow.Library.DatabaseLayer.ExpressionVisitors
{
    public interface ISqlExpressionVisitor<T>
    {
        void AddToStore();
        System.Collections.Generic.IList<string> GetAllFields();
        ISqlExpressionVisitor<T> GroupBy();
        ISqlExpressionVisitor<T> GroupBy(string groupBy);
        ISqlExpressionVisitor<T> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector);
        string GroupByExpression { get; set; }
        ISqlExpressionVisitor<T> Having();
        ISqlExpressionVisitor<T> Having(Expression<Func<T, bool>> predicate);
        ISqlExpressionVisitor<T> Having(string sqlFilter, params object[] filterParams);
        string HavingExpression { get; set; }
        ISqlExpressionVisitor<T> Limit();
        ISqlExpressionVisitor<T> Limit(int rows);
        ISqlExpressionVisitor<T> Limit(int skip, int rows);
        string LimitExpression { get; }
        string MethodName { get; set; }
        ISqlExpressionVisitor<T> OrderBy();
        ISqlExpressionVisitor<T> OrderBy(string orderBy);
        ISqlExpressionVisitor<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);
        ISqlExpressionVisitor<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
        string OrderByExpression { get; set; }
        Type RepositoryType { get; set; }
        int? Rows { get; set; }
        ISqlExpressionVisitor<T> Select();
        ISqlExpressionVisitor<T> Select(string selectExpression);
        ISqlExpressionVisitor<T> Select<TKey>(Expression<Func<T, TKey>> fields);
        ISqlExpressionVisitor<T> SelectDistinct<TKey>(Expression<Func<T, TKey>> fields);
        string SelectExpression { get; set; }
        int? Skip { get; set; }
        string ToSelectStatement();
        ISqlExpressionVisitor<T> Where();
        ISqlExpressionVisitor<T> Where(Expression<Func<T, bool>> predicate);
        ISqlExpressionVisitor<T> Where(string sqlFilter, params object[] filterParams);
        ISqlExpressionVisitor<T> Where<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, T8, bool>> predicate);
        ISqlExpressionVisitor<T> Where<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, bool>> predicate);
        ISqlExpressionVisitor<T> Where<T1, T2, T3, T4, T5, T6>(Expression<Func<T, T1, T2, T3, T4, T5, T6, bool>> predicate);
        ISqlExpressionVisitor<T> Where<T1, T2, T3, T4, T5>(Expression<Func<T, T1, T2, T3, T4, T5, bool>> predicate);
        ISqlExpressionVisitor<T> Where<T1, T2, T3, T4>(Expression<Func<T, T1, T2, T3, T4, bool>> predicate);
        ISqlExpressionVisitor<T> Where<T1, T2, T3>(Expression<Func<T, T1, T2, T3, bool>> predicate);
        ISqlExpressionVisitor<T> Where<T1, T2>(Expression<Func<T, T1, T2, bool>> predicate);
        ISqlExpressionVisitor<T> Where<T1>(Expression<Func<T, T1, bool>> predicate);
        string WhereExpression { get; set; }
    }
}
