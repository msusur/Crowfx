﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using Crow.Library.DatabaseLayer;
using Crow.Library.Foundation.Common;
using Crow.Library.RepositoryStorage;

namespace Crow.Library.DatabaseLayer.ExpressionVisitors
{
    public abstract class SqlExpressionVisitor<T> : ISqlExpressionVisitor<T>
    {

        private string selectExpression = string.Empty;
        private string whereExpression;
        private string groupBy = string.Empty;
        private string havingExpression;
        private string orderBy = string.Empty;

        IList<string> updateFields = new List<string>();
        IList<string> insertFields = new List<string>();

        private string sep = string.Empty;
        private bool useFieldName = false;
        private TableInfo modelDef;


        public SqlExpressionVisitor()
        {
            modelDef = typeof(T).GetModelDefinition();
        }

        /// <summary>
        /// Clear select expression. All properties will be selected.
        /// </summary>
        public virtual ISqlExpressionVisitor<T> Select()
        {
            return Select(string.Empty);
        }

        /// <summary>
        /// set the specified selectExpression.
        /// </summary>
        /// <param name='selectExpression'>
        /// raw Select expression: "Select SomeField1, SomeField2 from SomeTable"
        /// </param>
        public virtual ISqlExpressionVisitor<T> Select(string selectExpression)
        {

            if (string.IsNullOrEmpty(selectExpression))
            {
                BuildSelectExpression(string.Empty, false);
            }
            else
            {
                this.selectExpression = selectExpression;
            }
            return this;
        }

        /// <summary>
        /// Fields to be selected.
        /// </summary>
        /// <param name='fields'>
        /// x=> x.SomeProperty1 or x=> new{ x.SomeProperty1, x.SomeProperty2}
        /// </param>
        /// <typeparam name='TKey'>
        /// objectWithProperties
        /// </typeparam>
        public virtual ISqlExpressionVisitor<T> Select<TKey>(Expression<Func<T, TKey>> fields)
        {
            sep = string.Empty;
            useFieldName = true;
            BuildSelectExpression(Visit(fields), false);
            return this;
        }

        public virtual ISqlExpressionVisitor<T> SelectDistinct<TKey>(Expression<Func<T, TKey>> fields)
        {
            sep = string.Empty;
            useFieldName = true;
            BuildSelectExpression(Visit(fields), true);
            return this;
        }

        public virtual ISqlExpressionVisitor<T> Where()
        {
            return Where(string.Empty);
        }

        public virtual ISqlExpressionVisitor<T> Where(string sqlFilter, params object[] filterParams)
        {
            whereExpression = !string.IsNullOrEmpty(sqlFilter) ? sqlFilter.SqlFormat(filterParams) : string.Empty;
            if (!string.IsNullOrEmpty(whereExpression))
            {
                whereExpression = "WHERE " + whereExpression;
            }
            return this;
        }

        public virtual ISqlExpressionVisitor<T> Where<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, T8, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }
        public virtual ISqlExpressionVisitor<T> Where<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }
        public virtual ISqlExpressionVisitor<T> Where<T1, T2, T3, T4, T5, T6>(Expression<Func<T, T1, T2, T3, T4, T5, T6, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }
        public virtual ISqlExpressionVisitor<T> Where<T1, T2, T3, T4, T5>(Expression<Func<T, T1, T2, T3, T4, T5, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }
        public virtual ISqlExpressionVisitor<T> Where<T1, T2, T3, T4>(Expression<Func<T, T1, T2, T3, T4, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }
        public virtual ISqlExpressionVisitor<T> Where<T1, T2, T3>(Expression<Func<T, T1, T2, T3, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }
        public virtual ISqlExpressionVisitor<T> Where<T1, T2>(Expression<Func<T, T1, T2, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }
        public virtual ISqlExpressionVisitor<T> Where<T1>(Expression<Func<T, T1, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }
        public virtual ISqlExpressionVisitor<T> Where(Expression<Func<T, bool>> predicate)
        {
            WhereInternal(predicate);
            return this;
        }

        private void WhereInternal(Expression predicate)
        {
            if (predicate != null)
            {
                useFieldName = true;
                sep = " ";
                whereExpression = Visit(predicate);
                if (!string.IsNullOrEmpty(whereExpression))
                {
                    whereExpression = "WHERE " + whereExpression;
                }
            }
            else
            {
                whereExpression = string.Empty;
            }
        }


        public virtual ISqlExpressionVisitor<T> GroupBy()
        {
            return GroupBy(string.Empty);
        }

        public virtual ISqlExpressionVisitor<T> GroupBy(string groupBy)
        {
            this.groupBy = groupBy;
            return this;
        }

        public virtual ISqlExpressionVisitor<T> GroupBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            sep = string.Empty;
            useFieldName = true;
            groupBy = Visit(keySelector);
            if (!string.IsNullOrEmpty(groupBy)) groupBy = string.Format("GROUP BY {0}", groupBy);
            return this;
        }


        public virtual ISqlExpressionVisitor<T> Having()
        {
            return Having(string.Empty);
        }

        public virtual ISqlExpressionVisitor<T> Having(string sqlFilter, params object[] filterParams)
        {
            havingExpression = !string.IsNullOrEmpty(sqlFilter) ? sqlFilter.SqlFormat(filterParams) : string.Empty;
            if (!string.IsNullOrEmpty(havingExpression)) havingExpression = "HAVING " + havingExpression;
            return this;
        }

        public virtual ISqlExpressionVisitor<T> Having(Expression<Func<T, bool>> predicate)
        {

            if (predicate != null)
            {
                useFieldName = true;
                sep = " ";
                havingExpression = Visit(predicate);
                if (!string.IsNullOrEmpty(havingExpression)) havingExpression = "HAVING " + havingExpression;
            }
            else
                havingExpression = string.Empty;

            return this;
        }



        public virtual ISqlExpressionVisitor<T> OrderBy()
        {
            return OrderBy(string.Empty);
        }

        public virtual ISqlExpressionVisitor<T> OrderBy(string orderBy)
        {
            this.orderBy = orderBy;
            return this;
        }

        public virtual ISqlExpressionVisitor<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            sep = string.Empty;
            useFieldName = true;
            orderBy = Visit(keySelector);
            if (!string.IsNullOrEmpty(orderBy)) orderBy = string.Format("ORDER BY {0}", orderBy);
            return this;
        }

        public virtual ISqlExpressionVisitor<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            sep = string.Empty;
            useFieldName = true;
            orderBy = Visit(keySelector);
            if (!string.IsNullOrEmpty(orderBy)) orderBy = string.Format("ORDER BY {0} DESC", orderBy);
            return this;
        }


        /// <summary>
        /// Set the specified offset and rows for SQL Limit clause.
        /// </summary>
        /// <param name='skip'>
        /// Offset of the first row to return. The offset of the initial row is 0
        /// </param>
        /// <param name='rows'>
        /// Number of rows returned by a SELECT statement
        /// </param>	
        public virtual ISqlExpressionVisitor<T> Limit(int skip, int rows)
        {
            Rows = rows;
            Skip = skip;
            return this;
        }

        /// <summary>
        /// Set the specified rows for Sql Limit clause.
        /// </summary>
        /// <param name='rows'>
        /// Number of rows returned by a SELECT statement
        /// </param>
        public virtual ISqlExpressionVisitor<T> Limit(int rows)
        {
            Rows = rows;
            Skip = 0;
            return this;
        }

        /// <summary>
        /// Clear Sql Limit clause
        /// </summary>
        public virtual ISqlExpressionVisitor<T> Limit()
        {
            Skip = null;
            Rows = null;
            return this;
        }


        public virtual string ToSelectStatement()
        {
            var sql = new StringBuilder();

            sql.Append(SelectExpression);
            sql.Append(string.IsNullOrEmpty(WhereExpression) ?
                       "" :
                       "\n" + WhereExpression);
            sql.Append(string.IsNullOrEmpty(GroupByExpression) ?
                       "" :
                       "\n" + GroupByExpression);
            sql.Append(string.IsNullOrEmpty(HavingExpression) ?
                       "" :
                       "\n" + HavingExpression);
            sql.Append(string.IsNullOrEmpty(OrderByExpression) ?
                       "" :
                       "\n" + OrderByExpression);
            sql.Append(string.IsNullOrEmpty(LimitExpression) ?
                       "" :
                       "\n" + LimitExpression);
            return sql.ToString();

        }

        public string SelectExpression
        {
            get
            {
                if (string.IsNullOrEmpty(selectExpression))
                    BuildSelectExpression(string.Empty, false);
                return selectExpression;
            }
            set
            {
                selectExpression = value;
            }
        }

        public string WhereExpression
        {
            get { return whereExpression; }
            set { whereExpression = value; }
        }

        public string GroupByExpression
        {
            get { return groupBy; }
            set { groupBy = value; }
        }

        public string HavingExpression
        {
            get { return havingExpression; }
            set { havingExpression = value; }
        }

        public string OrderByExpression
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        public virtual string LimitExpression
        {
            get
            {
                if (!Skip.HasValue) return "";
                string rows;
                if (Rows.HasValue)
                {
                    rows = string.Format(",{0}", Rows.Value);
                }
                else
                {
                    rows = string.Empty;
                }
                return string.Format("LIMIT {0}{1}", Skip.Value, rows);
            }
        }

        public int? Rows { get; set; }
        public int? Skip { get; set; }

        protected internal TableInfo ModelDef
        {
            get { return modelDef; }
            set { modelDef = value; }
        }

        protected internal virtual string Visit(Expression exp)
        {

            if (exp == null) return string.Empty;
            switch (exp.NodeType)
            {
                case ExpressionType.Lambda:
                    return VisitLambda(exp as LambdaExpression);
                case ExpressionType.MemberAccess:
                    return VisitMemberAccess(exp as MemberExpression);
                case ExpressionType.Constant:
                    return VisitConstant(exp as ConstantExpression);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    return "(" + VisitBinary(exp as BinaryExpression) + ")";
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    return VisitUnary(exp as UnaryExpression);
                case ExpressionType.Parameter:
                    return VisitParameter(exp as ParameterExpression);
                case ExpressionType.Call:
                    return VisitMethodCall(exp as MethodCallExpression);
                case ExpressionType.New:
                    return VisitNew(exp as NewExpression);
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    return VisitNewArray(exp as NewArrayExpression);
                default:
                    return exp.ToString();
            }
        }

        protected virtual string VisitLambda(LambdaExpression lambda)
        {
            if (lambda.Body.NodeType == ExpressionType.MemberAccess && sep == " ")
            {
                MemberExpression m = lambda.Body as MemberExpression;

                if (m.Expression != null)
                {
                    string r = VisitMemberAccess(m);
                    return string.Format("{0}={1}", r, GetQuotedTrueValue());
                }

            }
            return Visit(lambda.Body);
        }

        protected virtual string VisitBinary(BinaryExpression b)
        {
            string left, right;
            var operandParameterPrefix = BindOperantOperandPrefix(b);
            var operand = BindOperant(b.NodeType);   //sep= " " ??
            if (operand == "AND" || operand == "OR")
            {
                MemberExpression m = b.Left as MemberExpression;
                if (m != null && m.Expression != null)
                {
                    string r = VisitMemberAccess(m);
                    left = string.Format("{0}={1}", r, GetQuotedTrueValue());
                }
                else
                {
                    left = Visit(b.Left);
                }
                m = b.Right as MemberExpression;
                if (m != null && m.Expression != null)
                {
                    string r = VisitMemberAccess(m);
                    right = string.Format("{0}={1}", r, GetQuotedTrueValue());
                }
                else
                {
                    right = Visit(b.Right);
                }
            }
            else
            {
                left = Visit(b.Left);
                right = Visit(b.Right);
            }

            if (operand == "=" && right == "null")
            {
                operand = "is";
            }
            else if (operand == "<>" && right == "null")
            {
                operand = "is not";
            }
            else if (operand == "=" || operand == "<>")
            {
                if (IsTrueExpression(right))
                {
                    right = GetQuotedTrueValue();
                }
                else if (IsFalseExpression(right))
                {
                    right = GetQuotedFalseValue();
                }

                if (IsTrueExpression(left))
                {
                    left = GetQuotedTrueValue();
                }
                else if (IsFalseExpression(left))
                {
                    left = GetQuotedFalseValue();
                }

            }
            switch (operand)
            {
                case "MOD":
                case "COALESCE":
                    return string.Format("{0}({1},{2})", operand, left, right);
                default:
                    return left + sep + operand + sep + operandParameterPrefix + right;
            }
        }

        protected virtual object BindOperantOperandPrefix(BinaryExpression b)
        {
            string parameterString = DbConfig.DialectProvider.GetParameterString();
            return b.Right.NodeType == ExpressionType.Parameter ? parameterString : string.Empty;
        }

        protected virtual string VisitMemberAccess(MemberExpression m)
        {
            if (m.Expression != null &&
               m.Expression.NodeType == ExpressionType.Parameter
               && m.Expression.Type == typeof(T))
            {
                string o = GetFieldName(m.Member.Name);
                return o;
            }
            else
            {
                var member = Expression.Convert(m, typeof(object));
                var lambda = Expression.Lambda<Func<object>>(member);
                var getter = lambda.Compile();
                object o = getter();
                return DbConfig.DialectProvider.GetQuotedValue(o, o != null ? o.GetType() : null);
            }
        }

        protected virtual string VisitNew(NewExpression nex)
        {
            // TODO : check !
            var member = Expression.Convert(nex, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            try
            {
                var getter = lambda.Compile();
                object o = getter();
                return DbConfig.DialectProvider.GetQuotedValue(o, o.GetType());
            }
            catch (System.InvalidOperationException)
            { // FieldName ?
                List<Object> exprs = VisitExpressionList(nex.Arguments);
                StringBuilder r = new StringBuilder();
                foreach (Object e in exprs)
                {
                    r.AppendFormat("{0}{1}",
                                   r.Length > 0 ? "," : "",
                                   e);
                }
                return r.ToString();
            }

        }

        protected virtual string VisitParameter(ParameterExpression p)
        {
            return p.Name;
        }

        protected virtual string VisitConstant(ConstantExpression c)
        {
            if (c.Value == null)
                return "null";
            else if (c.Value.GetType() == typeof(bool))
            {
                object o = DbConfig.DialectProvider.GetQuotedValue(c.Value, c.Value.GetType());
                return string.Format("({0}={1})", GetQuotedTrueValue(), o);
            }
            else
                return DbConfig.DialectProvider.GetQuotedValue(c.Value, c.Value.GetType());
        }

        protected virtual string VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    string o = Visit(u.Operand);
                    if (IsFieldName(o)) o = o + "=" + DbConfig.DialectProvider.GetQuotedValue(true, typeof(bool));
                    return "NOT (" + o + ")";
                default:
                    return Visit(u.Operand);

            }

        }

        protected virtual string VisitMethodCall(MethodCallExpression m)
        {
            List<Object> args = this.VisitExpressionList(m.Arguments);

            Object r;
            if (m.Object != null)
                r = Visit(m.Object);
            else
            {
                r = args[0];
                args.RemoveAt(0);
            }

            switch (m.Method.Name)
            {
                case "ToUpper":
                    return string.Format("upper({0})", r);
                case "ToLower":
                    return string.Format("lower({0})", r);
                case "StartsWith":
                    return string.Format("upper({0}) like '{1}%' ", r, RemoveQuote(args[0].ToString()).ToUpper());
                case "EndsWith":
                    return string.Format("upper({0}) like '%{1}'", r, RemoveQuote(args[0].ToString()).ToUpper());
                case "Contains":
                    return string.Format("upper({0}) like '%{1}%'", r, RemoveQuote(args[0].ToString()).ToUpper());
                case "Substring":
                    var startIndex = Int32.Parse(args[0].ToString()) + 1;
                    if (args.Count == 2)
                    {
                        var length = Int32.Parse(args[1].ToString());
                        return string.Format("substring({0} from {1} for {2})",
                                         r,
                                         startIndex,
                                         length);
                    }
                    else
                        return string.Format("substring({0} from {1})",
                                         r,
                                         startIndex);
                case "Round":
                case "Floor":
                case "Ceiling":
                case "Coalesce":
                case "Abs":
                case "Sum":
                    return string.Format("{0}({1}{2})",
                                         m.Method.Name,
                                         r,
                                         args.Count == 1 ? string.Format(",{0}", args[0]) : "");
                case "Concat":
                    StringBuilder s = new StringBuilder();
                    foreach (Object e in args)
                    {
                        s.AppendFormat(" || {0}", e);
                    }
                    return string.Format("{0}{1}", r, s.ToString());

                case "In":

                    var member = Expression.Convert(m.Arguments[1], typeof(object));
                    var lambda = Expression.Lambda<Func<object>>(member);
                    var getter = lambda.Compile();

                    var inArgs = getter() as object[];

                    StringBuilder sIn = new StringBuilder();
                    foreach (Object e in inArgs)
                    {
                        if (e.GetType().ToString() != "System.Collections.Generic.List`1[System.Object]")
                        {
                            sIn.AppendFormat("{0}{1}",
                                         sIn.Length > 0 ? "," : "",
                                         DbConfig.DialectProvider.GetQuotedValue(e, e.GetType()));
                        }
                        else
                        {
                            var listArgs = e as IList<Object>;
                            foreach (Object el in listArgs)
                            {
                                sIn.AppendFormat("{0}{1}",
                                         sIn.Length > 0 ? "," : "",
                                         DbConfig.DialectProvider.GetQuotedValue(el, el.GetType()));
                            }
                        }
                    }

                    return string.Format("{0} {1} ({2})", r, m.Method.Name, sIn.ToString());
                case "Desc":
                    return string.Format("{0} DESC", r);
                case "Alias":
                case "As":
                    return string.Format("{0} As {1}", r,
                        DbConfig.DialectProvider.GetQuotedColumnName(RemoveQuoteFromAlias(RemoveQuote(args[0].ToString()))));
                case "ToString":
                    return r.ToString();
                default:
                    StringBuilder s2 = new StringBuilder();
                    foreach (Object e in args)
                    {
                        s2.AppendFormat(",{0}",
                                        DbConfig.DialectProvider.GetQuotedValue(e, e.GetType()));
                    }
                    return string.Format("{0}({1}{2})", m.Method.Name, r, s2.ToString());
            }
        }

        protected virtual List<Object> VisitExpressionList(ReadOnlyCollection<Expression> original)
        {
            List<Object> list = new List<Object>();
            for (int i = 0, n = original.Count; i < n; i++)
            {
                if (original[i].NodeType == ExpressionType.NewArrayInit ||
                 original[i].NodeType == ExpressionType.NewArrayBounds)
                {

                    list.AddRange(VisitNewArrayFromExpressionList(original[i] as NewArrayExpression));
                }
                else
                    list.Add(Visit(original[i]));

            }
            return list;
        }

        protected virtual string VisitNewArray(NewArrayExpression na)
        {

            List<Object> exprs = VisitExpressionList(na.Expressions);
            StringBuilder r = new StringBuilder();
            foreach (Object e in exprs)
            {
                r.Append(r.Length > 0 ? "," + e : e);
            }

            return r.ToString();
        }

        protected virtual List<Object> VisitNewArrayFromExpressionList(NewArrayExpression na)
        {

            List<Object> exprs = VisitExpressionList(na.Expressions);
            return exprs;
        }


        protected virtual string BindOperant(ExpressionType e)
        {

            switch (e)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.Subtract:
                    return "-";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Modulo:
                    return "MOD";
                case ExpressionType.Coalesce:
                    return "COALESCE";
                default:
                    return e.ToString();
            }
        }

        protected virtual string GetFieldName(string name)
        {
            if (useFieldName)
            {
                //FieldDefinition fd = modelDef.FirstOrDefault(x => x.Name == name);
                //string fn = fd != default(FieldDefinition) ? fd.FieldName : name;
                string fn = name;
                return DbConfig.DialectProvider.GetQuotedColumnName(fn);
            }
            else
            {
                return name;
            }
        }

        protected string RemoveQuote(string exp)
        {

            if (exp.StartsWith("'") && exp.EndsWith("'"))
            {
                exp = exp.Remove(0, 1);
                exp = exp.Remove(exp.Length - 1, 1);
            }
            return exp;
        }

        protected string RemoveQuoteFromAlias(string exp)
        {

            if ((exp.StartsWith("\"") || exp.StartsWith("`") || exp.StartsWith("'"))
                &&
                (exp.EndsWith("\"") || exp.EndsWith("`") || exp.EndsWith("'")))
            {
                exp = exp.Remove(0, 1);
                exp = exp.Remove(exp.Length - 1, 1);
            }
            return exp;
        }

        protected bool IsFieldName(string quotedExp)
        {
            //FieldDefinition fd =
            //    modelDef.FieldDefinitions.
            //        FirstOrDefault(x =>
            //            DbConfig.DialectProvider.
            //            GetQuotedColumnName(x.FieldName) == quotedExp);

            return true;
        }

        private string GetTrueExpression()
        {
            object o = GetQuotedTrueValue();
            return string.Format("({0}={1})", o, o);
        }

        private string GetFalseExpression()
        {

            return string.Format("({0}={1})",
                GetQuotedTrueValue(),
                GetQuotedFalseValue());
        }

        private bool IsTrueExpression(string exp)
        {
            return (exp == GetTrueExpression());
        }

        private bool IsFalseExpression(string exp)
        {
            return (exp == GetFalseExpression());
        }

        private string GetQuotedTrueValue()
        {
            return DbConfig.DialectProvider.GetQuotedValue(true, typeof(bool));
        }

        private string GetQuotedFalseValue()
        {
            return DbConfig.DialectProvider.GetQuotedValue(false, typeof(bool));
        }

        private void BuildSelectExpression(string fields, bool distinct)
        {
            selectExpression = string.Format("SELECT {0}{1} \nFROM {2}",
                (distinct ? "DISTINCT " : ""),
                (string.IsNullOrEmpty(fields) ?
                    DbConfig.DialectProvider.GetColumnNames(typeof(T)) :
                    fields),
                DbConfig.DialectProvider.GetQuotedTableName(modelDef));
        }

        public IList<string> GetAllFields()
        {
            Database.PocoData data = Database.PocoData.ForType(typeof(T));
            return (from p in data.Columns
                    select p.Value.ColumnName).ToList();
        }


        public string MethodName { get; set; }

        public Type RepositoryType { get; set; }

        public void AddToStore()
        {
            QueryStore.AddCommand(RepositoryType, MethodName, this.ToSelectStatement());
        }
    }
}
