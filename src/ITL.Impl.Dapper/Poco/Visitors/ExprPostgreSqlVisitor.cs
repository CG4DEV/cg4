using System.Text;
using ITL.DataAccess.Poco.Expressions;
using ITL.DataAccess.Poco.Visitors;

namespace ITL.Impl.Dapper.Poco.Visitors
{
    public class ExprPostgreSqlVisitor : IExprVisitor
    {
        private readonly StringBuilder _stringBuilder;

        public ExprPostgreSqlVisitor()
        {
            _stringBuilder = new();
        }

        public ExprPostgreSqlVisitor(StringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
        }

        public void VisitAnd(ExprBoolAnd and)
        {
            and.Left.Accept(this);
            _stringBuilder.Append(" AND ");
            and.Right.Accept(this);
        }

        public void VisitOr(ExprBoolOr or)
        {
            _stringBuilder.Append('(');
            _stringBuilder.Append('(');
            or.Left.Accept(this);
            _stringBuilder.Append(')');
            _stringBuilder.Append(" OR ");
            _stringBuilder.Append('(');
            or.Right.Accept(this);
            _stringBuilder.Append(')');
            _stringBuilder.Append(')');
        }

        public void VisitColumn(ExprColumn column)
        {
            _stringBuilder.Append(column.Alias);
            _stringBuilder.Append('.');
            _stringBuilder.Append('"');
            _stringBuilder.Append(column.Name);
            _stringBuilder.Append('"');
        }

        public void VisitOrderColumn(ExprOrderColumn order)
        {
            VisitColumn(order);

            _stringBuilder.Append(' ');
            _stringBuilder.Append(order.Order);
        }

        public void VisitOrderBy(ExprOrderBy orderBy)
        {
            if (!orderBy.Any())
            {
                return;
            }

            _stringBuilder.Append("ORDER BY ");
            AcceptList(orderBy);
            _stringBuilder.AppendLine();
        }

        public void VisitSelectedColumn(ExprSelectedColumn selectedColumn)
        {
            VisitColumn(selectedColumn);

            _stringBuilder.Append(" AS \"");
            _stringBuilder.Append(selectedColumn.ResultName);
            _stringBuilder.Append('"');
        }

        public void VisitEqPredicate(ExprBoolEqPredicate eqPredicate)
        {
            eqPredicate.Column.Accept(this);
            if (eqPredicate.Value is ExprNull)
            {
                _stringBuilder.Append(" IS ");
            }
            else
            {
                _stringBuilder.Append(" = ");
            }

            eqPredicate.Value.Accept(this);
        }

        public void VisitNotEqPredicate(ExprBoolNotEqPredicate notEqPredicate)
        {
            notEqPredicate.Column.Accept(this);
            if (notEqPredicate.Value is ExprNull)
            {
                _stringBuilder.Append(" IS NOT ");
            }
            else
            {
                _stringBuilder.Append(" != ");
            }

            notEqPredicate.Value.Accept(this);
        }

        public string Build()
        {
            return _stringBuilder.ToString();
        }

        public void VisitStr(ExprStr str)
        {
            _stringBuilder.Append('\'');
            _stringBuilder.Append(str.Value.Replace("'", "''"));
            _stringBuilder.Append('\'');
        }

        public void VisitLong(ExprLong @long)
        {
            _stringBuilder.Append(@long.Value);
        }

        public void VisitNot(ExprNot not)
        {
            _stringBuilder.Append("NOT ");

            not.Body.Accept(this);
        }

        public void VisitInt(ExprInt @int)
        {
            _stringBuilder.Append(@int.Value);
        }

        public void VisitDateTime(ExprDateTime dateTime)
        {
            _stringBuilder.Append('\'');
            _stringBuilder.Append(dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            _stringBuilder.Append('\'');
        }

        public void VisitDateTimeOffset(ExprDateTimeOffset dateTimeOffset)
        {
            _stringBuilder.Append('\'');
            _stringBuilder.Append(dateTimeOffset.Value.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"));
            _stringBuilder.Append('\'');
        }

        public void VisitListColumns(ExprListColumns listColumns)
        {
            AcceptList(listColumns);
        }

        public void VisitParam(ExprParam param)
        {
            _stringBuilder.Append('@');
            _stringBuilder.Append(param.Name);
        }

        public void VisitListJoins(ExprListJoins listJoins)
        {
            AcceptList(listJoins, string.Empty);
        }

        public void VisitTableName(ExprTableName table)
        {
            _stringBuilder.Append('"');
            _stringBuilder.Append(table.TableName);
            _stringBuilder.Append('"');
            _stringBuilder.Append(" AS ");
            _stringBuilder.Append(table.Alias);
        }

        public void VisitFrom(ExprFrom from)
        {
            _stringBuilder.Append("FROM ");
            from.TableName.Accept(this);
            _stringBuilder.AppendLine();

            if (from.Joins.Any())
            {
                from.Joins.Accept(this);
                _stringBuilder.AppendLine();
            }
        }

        public void VisitJoin(ExprJoin join)
        {
            _stringBuilder.Append("JOIN ");
            join.TableName.Accept(this);
            _stringBuilder.Append(" ON ");
            join.TableColumn.Accept(this);
            _stringBuilder.Append(" = ");
            join.OtherColumn.Accept(this);
        }

        public void VisitInnerJoin(ExprInnerJoin innerJoin)
        {
            _stringBuilder.Append("INNER ");
            VisitJoin(innerJoin);
        }

        public void VisitLeftJoin(ExprLeftJoin leftJoin)
        {
            _stringBuilder.Append("LEFT ");
            VisitJoin(leftJoin);
        }

        public void VisitRightJoin(ExprRightJoin rightJoin)
        {
            _stringBuilder.Append("RIGHT ");
            VisitJoin(rightJoin);
        }

        public void VisitSelect(ExprSelect select)
        {
            _stringBuilder.Append("SELECT ");
            if (select.Any())
            {
                AcceptList(select);
            }
            else
            {
                _stringBuilder.Append('*');
            }

            _stringBuilder.AppendLine();
        }

        public void VisitSql(ExprSql sql)
        {
            sql.Select.Accept(this);
            sql.From.Accept(this);
            sql.Where?.Accept(this);
            sql.OrderBy.Accept(this);

            if (sql.Limit.HasValue)
            {
                _stringBuilder.Append("LIMIT ");
                _stringBuilder.Append(sql.Limit.Value);
            }

            if (sql.Limit.HasValue && sql.Offset.HasValue)
            {
                _stringBuilder.Append(' ');
            }

            if (sql.Offset.HasValue)
            {
                _stringBuilder.Append("OFFSET ");
                _stringBuilder.Append(sql.Offset.Value);
            }
        }

        public void VisitWhere(ExprWhere where)
        {
            if (where.Expression == null)
            {
                return;
            }

            _stringBuilder.Append("WHERE ");
            where.Expression.Accept(this);
            _stringBuilder.AppendLine();
        }

        public void VisitFunction(ExprFunction function)
        {
            _stringBuilder.Append(function.Name);
            _stringBuilder.Append('(');
            AcceptList(function.Parametrs);
            _stringBuilder.Append(')');

            if (!string.IsNullOrEmpty(function.ResultName))
            {
                _stringBuilder.Append(" AS \"");
                _stringBuilder.Append(function.ResultName);
                _stringBuilder.Append('"');
            }
        }

        public void VisitStar(ExprStar star)
        {
            _stringBuilder.Append('*');
        }

        public void VisitNull(ExprNull exprNull)
        {
            _stringBuilder.Append("NULL");
        }

        public void VisitBool(ExprBool @bool)
        {
            if (@bool.Value)
            {
                _stringBuilder.Append("TRUE");
            }
            else
            {
                _stringBuilder.Append("FALSE");
            }
        }

        public void VisitLike(ExprLike exprLike)
        {
            exprLike.Column.Accept(this);
            _stringBuilder.Append(" ILIKE '");
            _stringBuilder.Append(exprLike.StartsPattern);
            _stringBuilder.Append(exprLike.Value.Replace("'", "''"));
            _stringBuilder.Append(exprLike.EndsPattern);
            _stringBuilder.Append('\'');
        }

        public void VisitIn(ExprIn exprIn)
        {
            exprIn.Column.Accept(this);
            _stringBuilder.Append(" = ANY (");
            VisitArray(exprIn.Values);
            _stringBuilder.Append(')');
        }

        public void VisitArray(ExprArray array)
        {
            _stringBuilder.Append("ARRAY [");
            AcceptList(array);
            _stringBuilder.Append(']');
        }

        public void VisitBoolEmpty(ExprBoolEmpty empty)
        {
            _stringBuilder.Append("TRUE");
        }

        public void VisitGreaterThanPredicate(ExprGreaterThan greaterPredicate)
        {
            greaterPredicate.Column.Accept(this);
            _stringBuilder.Append(" > ");
            greaterPredicate.Value.Accept(this);
        }

        public void VisitLessThanPredicate(ExprLessThan lessPredicate)
        {
            lessPredicate.Column.Accept(this);
            _stringBuilder.Append(" < ");
            lessPredicate.Value.Accept(this);
        }

        public void VisitGreaterThanOrEqPredicate(ExprGreaterThanOrEq greaterEqPredicate)
        {
            greaterEqPredicate.Column.Accept(this);
            _stringBuilder.Append(" >= ");
            greaterEqPredicate.Value.Accept(this);
        }

        public void VisitLessThanOrEqPredicate(ExprLessThanOrEq lesssEqPredicate)
        {
            lesssEqPredicate.Column.Accept(this);
            _stringBuilder.Append(" <= ");
            lesssEqPredicate.Value.Accept(this);
        }

        public void VisitConstValue(string value)
        {
            _stringBuilder.Append(value);
        }

        private void AcceptList(IEnumerable<Expr> expressions, string separator = ", ")
        {
            bool isFirst = true;

            foreach (var expr in expressions)
            {
                if (!isFirst)
                {
                    _stringBuilder.Append(separator);
                }

                expr.Accept(this);
                isFirst = false;
            }
        }
    }
}
