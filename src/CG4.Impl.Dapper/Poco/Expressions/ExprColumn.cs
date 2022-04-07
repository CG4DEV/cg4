namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprColumn : Expr
    {
        public string Alias { get; set; }

        public string Name { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitColumn(this);

        public static ExprBoolean operator ==(ExprColumn column, string value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = new ExprStr { Value = value },
        };

        public static ExprBoolean operator !=(ExprColumn column, string value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = new ExprStr { Value = value },
        };

        public static ExprBoolean operator ==(ExprColumn column, long value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = new ExprLong { Value = value },
        };

        public static ExprBoolean operator !=(ExprColumn column, long value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = new ExprLong { Value = value },
        };

        public static ExprBoolean operator ==(ExprColumn column, ExprParam param) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = param,
        };

        public static ExprBoolean operator !=(ExprColumn column, ExprParam param) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = param,
        };

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return Equals(obj as ExprColumn);
        }

        public bool Equals(ExprColumn other)
        {
            if (other is null)
            {
                return false;
            }

            return Name == other.Name
                && Alias == other.Alias;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Alias, Name);
        }
    }
}
