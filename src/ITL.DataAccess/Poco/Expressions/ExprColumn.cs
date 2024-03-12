using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprColumn : Expr
    {
        public ExprColumn()
        {
        }

        public ExprColumn(string name)
        {
            Name = name;
        }

        public ExprColumn(string alias, string name)
        {
            Alias = alias;
            Name = name;
        }

        public string Alias { get; set; }

        public string Name { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitColumn(this);

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

        #region Operator reloads
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

        public static ExprBoolean operator ==(ExprColumn column, bool value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = new ExprBool { Value = value },
        };

        public static ExprBoolean operator !=(ExprColumn column, bool value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = new ExprBool { Value = value },
        };

        public static ExprBoolean operator ==(ExprColumn column, DateTime value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = new ExprDateTime { Value = value },
        };

        public static ExprBoolean operator !=(ExprColumn column, DateTime value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = new ExprDateTime { Value = value },
        };

        public static ExprBoolean operator ==(ExprColumn column, DateTimeOffset value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = new ExprDateTimeOffset { Value = value },
        };

        public static ExprBoolean operator !=(ExprColumn column, DateTimeOffset value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = new ExprDateTimeOffset { Value = value },
        };

        public static ExprBoolean operator ==(ExprColumn column, long? value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = value.HasValue ? new ExprLong { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator !=(ExprColumn column, long? value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = value.HasValue ? new ExprLong { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator ==(ExprColumn column, bool? value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = value.HasValue ? new ExprBool { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator !=(ExprColumn column, bool? value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = value.HasValue ? new ExprBool { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator ==(ExprColumn column, DateTime? value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = value.HasValue ? new ExprDateTime { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator !=(ExprColumn column, DateTime? value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = value.HasValue ? new ExprDateTime { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator ==(ExprColumn column, DateTimeOffset? value) => new ExprBoolEqPredicate
        {
            Column = column,
            Value = value.HasValue ? new ExprDateTimeOffset { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator !=(ExprColumn column, DateTimeOffset? value) => new ExprBoolNotEqPredicate
        {
            Column = column,
            Value = value.HasValue ? new ExprDateTimeOffset { Value = value.Value } : new ExprNull(),
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

        public static ExprBoolean operator >(ExprColumn column, long? value) => new ExprGreaterThan
        {
            Column = column,
            Value = value.HasValue ? new ExprLong { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator <(ExprColumn column, long? value) => new ExprLessThan
        {
            Column = column,
            Value = value.HasValue ? new ExprLong { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator >=(ExprColumn column, long? value) => new ExprGreaterThanOrEq
        {
            Column = column,
            Value = value.HasValue ? new ExprLong { Value = value.Value } : new ExprNull(),
        };

        public static ExprBoolean operator <=(ExprColumn column, long? value) => new ExprLessThanOrEq
        {
            Column = column,
            Value = value.HasValue ? new ExprLong { Value = value.Value } : new ExprNull(),
        };
        #endregion
    }
}
