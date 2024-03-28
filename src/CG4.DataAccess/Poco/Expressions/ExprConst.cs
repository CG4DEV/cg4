using System.Collections;

namespace CG4.DataAccess.Poco.Expressions
{
    public abstract class ExprConst : Expr
    {
        public static ExprConst Create(Type valueType, object value)
        {
            if (value == null)
            {
                return new ExprNull();
            }

            var builder = ExprConstBuildRegister.Get(valueType);

            if (builder is not null)
            {
                return builder.Build(valueType, value);
            }

            if (valueType.IsEnum)
            {
                var member = Enum.GetUnderlyingType(valueType);
                var b = ExprConstBuildRegister.Get(member);
                
                if (b is not null)
                {
                    return b.Build(member, value);
                }
            }

            if (valueType.IsAssignableTo(typeof(IEnumerable)))
            {
                return CreateArray(value as IEnumerable);
            }

            throw new NotSupportedException($"Type '{valueType.Name}' not supported");
        }

        public static ExprArray CreateArray(IEnumerable objects)
        {
            var arr = new ExprArray();

            foreach (var item in objects)
            {
                arr.Add(Create(item.GetType(), item));
            }

            return arr;
        }
    }
}
