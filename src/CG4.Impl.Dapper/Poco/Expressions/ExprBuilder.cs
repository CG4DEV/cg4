using System.Linq.Expressions;
using System.Reflection;

namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprBuilder
    {
        private readonly ClassMap _map;
        private readonly string _alias;

        public ExprBuilder(ClassMap map, string alias = null)
        {
            _map = map;
            _alias = alias;
        }

        public Expr ParseExpr(Expression expression)
        {
            Expr result = expression.NodeType switch
            {
                ExpressionType.Equal => ParseEqual((BinaryExpression)expression),
                ExpressionType.NotEqual => ParseNotEqual((BinaryExpression)expression),
                ExpressionType.Constant => ParseConst((ConstantExpression)expression),
                ExpressionType.MemberAccess => ParseMember((MemberExpression)expression),
                ExpressionType.Parameter => ParseParametr((ParameterExpression)expression),
                ExpressionType.AndAlso => ParseAnd((BinaryExpression)expression),
                ExpressionType.OrElse => ParseOr((BinaryExpression)expression),
                ExpressionType.Convert => ParseConvert((UnaryExpression)expression),
                ExpressionType.Call => ParseCall((MethodCallExpression)expression),
                _ => throw new NotSupportedException($"NodeType '{expression.NodeType}' not suported"),
            };

            return result;
        }

        public Expr ParseCall(MethodCallExpression expression)
        {
            if (expression.Type == typeof(bool))
            {
                var expr = ExprBoolFunc.Create(expression.Method.DeclaringType, expression.Method);
                var exprConst = (ExprConst)ParseExpr(expression.Arguments[0]);
                switch (expr)
                {
                    case ExprLike exprLike:
                        exprLike.Column = (ExprColumn)ParseExpr(expression.Object);
                        exprLike.Value = ((ExprStr)exprConst).Value;
                        break;
                    case ExprIn exprIn:
                        exprIn.Column = (ExprColumn)ParseExpr(expression.Arguments[1]);
                        exprIn.Values = (ExprArray)exprConst;
                        break;
                    default: throw new NotSupportedException($"Expression '{expr.GetType().Name}' not supported");
                }

                return expr;
            }

            throw new NotSupportedException($"Method '{expression.Method.Name}' for type '{expression.Type.Name}' not suported");
        }

        public ExprBoolean ParseEqual(BinaryExpression expression)
        {
            var (col, val) = ParseEquality(expression);

            return new ExprBoolEqPredicate
            {
                Column = col,
                Value = val,
            };
        }

        public ExprBoolean ParseNotEqual(BinaryExpression expression)
        {
            var (col, val) = ParseEquality(expression);

            return new ExprBoolNotEqPredicate
            {
                Column = col,
                Value = val,
            };
        }

        public ExprConst ParseConst(ConstantExpression expression)
        {
            return ExprConst.Create(expression.Type, expression.Value);
        }

        public Expr ParseMember(MemberExpression expression)
        {
            if (expression.Expression.NodeType == ExpressionType.Parameter)
            {
                var col = ParseParametr((ParameterExpression)expression.Expression);
                var name = expression.Member.Name;
                var columntName = _map.Properties.FirstOrDefault(x => x.Name == name).ColumnName;
                col.Name = columntName ?? name;

                return col;
            }

            return ParseMemberConstant(expression);
        }

        public ExprColumn ParseParametr(ParameterExpression expression)
        {
            return new ExprColumn
            {
                Alias = _alias ?? expression.Name,
            };
        }

        public ExprBoolAnd ParseAnd(BinaryExpression expression)
        {
            return new ExprBoolAnd
            {
                Left = (ExprBoolean)ParseExpr(expression.Left),
                Right = (ExprBoolean)ParseExpr(expression.Right),
            };
        }

        public ExprBoolOr ParseOr(BinaryExpression expression)
        {
            return new ExprBoolOr
            {
                Left = (ExprBoolean)ParseExpr(expression.Left),
                Right = (ExprBoolean)ParseExpr(expression.Right),
            };
        }

        public ExprConst ParseMemberConstant(MemberExpression expression)
        {
            var stack = new Stack<MemberInfo>();
            stack.Push(expression.Member);
            return ParseMemberConstant(stack, expression);
        }

        public Expr ParseConvert(UnaryExpression expression)
        {
            return ParseExpr(expression.Operand);
        }

        private ExprConst ParseMemberConstant(Stack<MemberInfo> members, MemberExpression expression)
        {
            if (expression.Expression.NodeType == ExpressionType.MemberAccess)
            {
                var next = (MemberExpression)expression.Expression;
                members.Push(next.Member);
                return ParseMemberConstant(members, next);
            }

            var constExrp = (ConstantExpression)expression.Expression;
            Type valType = null;
            object val = constExrp.Value;
            MemberInfo member;

            while (members.TryPop(out member))
            {
                (valType, val) = GetValue(member, val);
            }

            return ExprConst.Create(valType, val);
        }

        private (Type t, object v) GetValue(MemberInfo member, object val) =>
            member.MemberType switch
            {
                MemberTypes.Field => (((FieldInfo)member).FieldType, ((FieldInfo)member).GetValue(val)),
                MemberTypes.Property => (((PropertyInfo)member).PropertyType, ((PropertyInfo)member).GetValue(val)),
                _ => throw new NotSupportedException($"Member '{member}' not supported"),
            };

        private (ExprColumn Column, ExprConst Value) ParseEquality(BinaryExpression expression)
        {
            var left = ParseExpr(expression.Left);
            var right = ParseExpr(expression.Right);

            ExprColumn col = left as ExprColumn ?? right as ExprColumn;
            ExprConst val = left as ExprConst ?? right as ExprConst;

            if (col is null || val is null)
            {
                throw new NotSupportedException();
            }

            return (col, val);
        }

    }
}
