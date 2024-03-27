using System.Collections;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace ITL.DataAccess.Poco.Expressions
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
                var enumUnderlyingType = Enum.GetUnderlyingType(valueType);
                var b = ExprConstBuildRegister.Get(enumUnderlyingType);

                if (b is null)
                {
                    throw new NotSupportedException($"Enum underlying type '{enumUnderlyingType}' not supported");
                }

                return b.Build(enumUnderlyingType, value);
            }

            if (valueType.IsAssignableTo(typeof(IEnumerable)))
            {
                return CreateArray(value as IEnumerable);
            }

            var t = Nullable.GetUnderlyingType(valueType);

            if (t is not null)
            {
                return Create(t, value);
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
