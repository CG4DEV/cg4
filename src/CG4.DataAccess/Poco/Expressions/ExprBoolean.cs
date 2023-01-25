using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public abstract class ExprBoolean : Expr
    {
        public static readonly ExprBoolean Empty = ExprBoolEmpty.Instance;

        public static ExprBoolean operator &(ExprBoolean left, ExprBoolean right)
        {
            if (left is ExprBoolEmpty)
            {
                return right;
            }

            if (right is ExprBoolEmpty)
            {
                return left;
            }

            return new ExprBoolAnd { Left = left, Right = right };
        }

        public static ExprBoolean operator |(ExprBoolean left, ExprBoolean right)
        {
            if (left is ExprBoolEmpty)
            {
                return right;
            }

            if (right is ExprBoolEmpty)
            {
                return left;
            }

            return new ExprBoolOr { Left = left, Right = right };
        }
    }

    public sealed class ExprBoolEmpty : ExprBoolean
    {
        private ExprBoolEmpty()
        {
        }

        public static readonly ExprBoolEmpty Instance = new();

        public override void Accept(IExprVisitor visitor) => visitor.VisitBoolEmpty(this);
    }
}
