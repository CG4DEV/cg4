using System.Reflection;

namespace CG4.Impl.Dapper.Poco.Expressions
{
    public abstract class ExprBoolFunc : ExprBoolean
    {
        // key is type of column
        private static readonly Dictionary<Type, Func<MethodInfo, ExprBoolFunc>> _container = new()
        {
            { typeof(string), CreateLike },
            { typeof(Enumerable), CreateIn },
        };

        public static ExprBoolFunc Create(Type columnType, MethodInfo methodInfo)
        {
            if (!_container.TryGetValue(columnType, out var builder))
            {
                throw new NotSupportedException($"Method for type '{columnType.Name}' not suported");
            }

            return builder(methodInfo);
        }

        private static ExprBoolFunc CreateLike(MethodInfo mi)
        {
            return mi.Name switch
            {
                nameof(string.Contains) => new ExprLike
                {
                    StartsPattern = "%",
                    EndsPattern = "%",
                },
                nameof(string.StartsWith) => new ExprLike
                {
                    EndsPattern = "%",
                },
                nameof(string.EndsWith) => new ExprLike
                {
                    StartsPattern = "%",
                },
                _ => throw new NotSupportedException($"Method {mi.Name} not supported")
            };
        }

        private static ExprBoolFunc CreateIn(MethodInfo mi)
        {
            return mi.Name switch
            {
                nameof(Enumerable.Contains) => new ExprIn(),
                _ => throw new NotSupportedException($"Method '{mi.Name}' not suported"),
            };
        }
    }
}
