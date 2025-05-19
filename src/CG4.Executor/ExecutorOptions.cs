using Microsoft.Extensions.DependencyInjection;

namespace CG4.Executor
{
    /// <summary>
    /// Options to configure executor and executions types.
    /// </summary>
    public class ExecutorOptions
    {
        /// <summary>
        /// Executor interface type.
        /// </summary>
        public Type? ExecutorInterfaceType { get; set; }

        /// <summary>
        /// Executor current type. Required property.
        /// <example>
        ///     For example:
        ///     <code>
        ///         options.ExecutorImplementationType = typeof(ExampleExecutor)
        ///     </code>
        /// </example>
        /// </summary>
        public Type ExecutorImplementationType { get; set; }

        /// <summary>
        /// Executor lifetime.
        /// </summary>
        public ServiceLifetime ExecutorLifetime { get; set; } = ServiceLifetime.Transient;

        /// <summary>
        /// Execution types lifetime.
        /// </summary>
        public ServiceLifetime? ExecutionTypesLifetime { get; set; } = ServiceLifetime.Transient;

        /// <summary>
        /// Types that implements <see cref="IExecution{TExecutionContext,TExecutionResult}"/>. Required property. 
        /// </summary>
        public Type[] ExecutionTypes { get; set; }
    }
}
