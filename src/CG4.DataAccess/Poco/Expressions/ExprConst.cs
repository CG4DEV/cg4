using System.Collections;

namespace CG4.DataAccess.Poco.Expressions
{
    public abstract class ExprConst : Expr
    {
        private static readonly Dictionary<Type, Func<object, ExprConst>> _builders = new()
        {
            { typeof(string), x => new ExprStr { Value = (string)x } },
            { typeof(long), x => new ExprLong { Value = (long)x } },
            { typeof(int), x => new ExprInt { Value = (int)x } },
            { typeof(short), x => new ExprInt { Value = (short)x } },
            { typeof(byte), x => new ExprInt { Value = (byte)x } },
            { typeof(DateTimeOffset), x => new ExprDateTimeOffset { Value = (DateTimeOffset)x } },
            { typeof(bool), x => new ExprBool { Value = (bool)x } },

            { typeof(long?), x => new ExprLong { Value = (long)x } },
            { typeof(int?), x => new ExprInt { Value = (int)x } },
            { typeof(short?), x => new ExprInt { Value = (short)x } },
            { typeof(byte?), x => new ExprInt { Value = (byte)x } },
            { typeof(DateTimeOffset?), x => new ExprDateTimeOffset { Value = (DateTimeOffset)x } },
            { typeof(bool?), x => new ExprBool { Value = (bool)x } },
        };

        public static ExprConst Create(Type valueType, object value)
        {
            if (value == null)
            {
                return new ExprNull();
            }

            if (_builders.TryGetValue(valueType, out var builder))
            {
                return builder.Invoke(value);
            }

            if (valueType.IsEnum)
            {
                var member = ((System.Reflection.TypeInfo)valueType).DeclaredFields.First();

                return _builders[member.FieldType].Invoke(value);
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
