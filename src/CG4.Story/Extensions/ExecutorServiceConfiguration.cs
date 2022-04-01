using CG4.Story.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CG4.Story.Extensions;

public class ExecutorServiceConfiguration
{
    /// <summary>
    /// Executor current type.
    /// </summary>
    public Type ExecutorImplementationType { get; private set; }
    
    /// <summary>
    /// Executor lifetime.
    /// </summary>
    public ServiceLifetime ExecutorLifetime { get; private set; }

    /// <summary>
    /// Execution types lifetime.
    /// </summary>
    public ServiceLifetime? ExecutionTypesLifetime { get; private set; }
    
    /// <summary>
    /// Types that implements <see cref="IExecution{TExecutionContext,TExecutionResult}"/>
    /// </summary>
    public Type[] ExecutionTypes { get; private set; }

    public ExecutorServiceConfiguration()
    {
        ExecutorImplementationType = typeof(StoryExecutor);
        ExecutorLifetime = ServiceLifetime.Transient;
    }

    /// <summary>
    /// Use your custom executor instead of base.
    /// </summary>
    /// <typeparam name="TStoryExecutor">type of custom executor <see cref="IStoryExecutor"/>.</typeparam>
    /// <returns></returns>
    public ExecutorServiceConfiguration UseCustom<TStoryExecutor>() where TStoryExecutor : IStoryExecutor
    {
        ExecutorImplementationType = typeof(TStoryExecutor);
        return this;
    }

    /// <summary>
    /// Add lifetime for executor.
    /// </summary>
    /// <param name="lifetime">Lifetime.</param>
    /// <returns></returns>
    public ExecutorServiceConfiguration AddLifetimeExecutor(ServiceLifetime lifetime)
    {
        ExecutorLifetime = lifetime;
        return this;
    }

    /// <summary>
    /// Add array of types that implements <see cref="IExecution{TExecutionContext,TExecutionResult}"/>.
    /// </summary>
    /// <param name="executionTypes">Types that implements <see cref="IExecution{TExecutionContext,TExecutionResult}"/>.</param>
    /// <param name="lifetime">Lifetime.</param>
    public ExecutorServiceConfiguration AddExecutionTypes(Type[] executionTypes, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        ExecutionTypes = executionTypes;
        ExecutionTypesLifetime = lifetime;
        return this;
    }
}