using System.Reflection;

namespace CG4.Executor
{
    /// <summary>
    /// Cache for implementation of <see cref="IExecution{TExecutionContext,TExecutionResult}"/>.
    /// </summary>
    internal static class CacheExecutor
    {
        /// <summary>
        /// Cache values.
        /// </summary>
        public static Dictionary<Type, (Type ExecutionType, MethodInfo Method)> Cache { get; } = new();

        /// <summary>
        /// Try getting value from cache.
        /// </summary>
        /// <param name="context">Type of Context for implementation of <see cref="IExecution{TExecutionContext,TExecutionResult}"/>.</param>
        /// <param name="executor">Tuple of pair execution type and method from <see cref="IExecution{TExecutionContext,TExecutionResult}"/>.</param>
        public static bool TryGetValue(Type context, out (Type ExecutionType, MethodInfo Method) executor)
        {
            return Cache.TryGetValue(context, out executor);
        }

        /// <summary>
        /// Try add value to cache.
        /// </summary>
        /// <param name="context">Type of Context for implementation of <see cref="IExecution{TExecutionContext,TExecutionResult}"/>.</param>
        /// <param name="executor">Tuple of pair execution type and method from <see cref="IExecution{TExecutionContext,TExecutionResult}"/>.</param>
        /// <returns></returns>
        public static bool TryAdd(Type context, (Type ExecutionType, MethodInfo Method) executor)
        {
            return Cache.TryAdd(context, executor);
        }
    }
}
