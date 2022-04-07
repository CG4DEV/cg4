using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CG4.Executor.Extensions;

/// <summary>
/// Extensions for scan and registers executor and executions types.
/// </summary>
public static class ExecutorsExtensions
{
    /// <summary>
    /// Registers executor and executions types from the specified assemblies.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="options">Options to configure executor and executions types.</param>
    /// <param name="handlerAssemblyMarkerTypes">Mark type to find assemblies to scan</param>
    /// <returns></returns>
    public static IServiceCollection AddExecutors(
        this IServiceCollection services,
        Action<ExecutorOptions> options,
        params Type[] handlerAssemblyMarkerTypes)
        => services.AddExecutors(options, handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly).ToArray());

    /// <summary>
    /// Registers executor and executions types from the specified assemblies.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="options">Options to configure executor and executions types.</param>
    /// <param name="assembliesToScan">Assemblies to scan.</param>
    /// <returns></returns>
    public static IServiceCollection AddExecutors(this IServiceCollection services,
        Action<ExecutorOptions> options,
        params Assembly[] assembliesToScan)
    {
        if (!assembliesToScan.Any())
        {
            throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
        }

        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        var executorOptions = new ExecutorOptions();
        options.Invoke(executorOptions);

        ValidateOptions(executorOptions);

        var typeInfos = assembliesToScan
            .SelectMany(t => t.DefinedTypes)
            .Distinct()
            .ToArray();

        AddExecutionsClasses(services, typeInfos, executorOptions);
        AddExecutorClass(services, executorOptions);
        return services;
    }

    private static void AddExecutorClass(IServiceCollection services, ExecutorOptions executorOptions)
    {
        services.TryAdd(new ServiceDescriptor(
            executorOptions.ExecutorInterfaceType ?? executorOptions.ExecutorImplementationType,
            executorOptions.ExecutorImplementationType,
            executorOptions.ExecutorLifetime));
    }

    private static void ValidateOptions(ExecutorOptions executorOptions)
    {
        if (executorOptions.ExecutorImplementationType == null)
        {
            throw new ArgumentNullException(nameof(executorOptions.ExecutorImplementationType) + " must be set!");
        }
        
        if (executorOptions.ExecutionTypes == null)
        {
            throw new ArgumentNullException(nameof(executorOptions.ExecutionTypes) + " must be set!");
        }
    }

    private static void AddExecutionsClasses(IServiceCollection services, TypeInfo[] typeInfos, ExecutorOptions options)
    {
        var executionTypes = options.ExecutionTypes;

        foreach (var internalType in executionTypes)
        {
            var executionInterfaces = new List<Type>(); //execution interfaces
            var executionImpls = new List<Type>(); //concrete classes

            foreach (var type in typeInfos)
            {
                var similarInterfaces = type.FindInterfacesThatClose(internalType).ToArray();
                if (!similarInterfaces.Any())
                    continue;

                foreach (var similar in similarInterfaces)
                {
                    if (executionInterfaces.Contains(similar))
                        continue;

                    executionInterfaces.Add(similar);
                }

                if (type.IsClassImplementation())
                {
                    executionImpls.Add(type);
                }
            }

            foreach (var executionInterface in executionInterfaces)
            {
                var exactMatchesExecutions = executionImpls.Where(x => x.CanBeCastTo(executionInterface)).ToList();

                if (exactMatchesExecutions.Count > 1)
                {
                    exactMatchesExecutions.RemoveAll(m => !IsMatchingWithInterface(m, executionInterface));
                }

                foreach (var type in exactMatchesExecutions)
                {
                    var lengthArguments = internalType.GetGenericArguments().Length;
                    var method = GetBaseMethod(lengthArguments, executionInterface);

                    (Type, MethodInfo) storyType = new (executionInterface, method);
                    CacheExecutor.TryAdd(executionInterface.GetGenericArguments().First(), storyType);

                    services.TryAdd(new ServiceDescriptor(executionInterface, type, options.ExecutionTypesLifetime ?? ServiceLifetime.Transient));
                }
            }
        }
    }

    private static MethodInfo GetBaseMethod(int argumentsLength, Type executionInterface)
    {
        var executorInterfaces = new[]
        {
            typeof(IExecution<>),
            typeof(IExecution<,>),
        };

        var baseInterface = executorInterfaces.First(x => x.GetGenericArguments().Length == argumentsLength) ??
                            throw InvalidOperationException(executionInterface.Name);
        baseInterface = baseInterface.MakeGenericType(executionInterface.GetGenericArguments());

        return baseInterface.GetMethod("ExecuteAsync") ?? throw InvalidOperationException(executionInterface.Name);
    }

    private static InvalidOperationException InvalidOperationException(string nameType)
    {
        return new InvalidOperationException($"{nameType} must have implementation {typeof(IExecution<,>).Name} or {typeof(IExecution<>).Name}");
    }

    private static bool IsMatchingWithInterface(Type handlerType, Type handlerInterface)
    {
        if (handlerType.IsInterface)
        {
            if (handlerType.GenericTypeArguments.SequenceEqual(handlerInterface.GenericTypeArguments))
            {
                return true;
            }
        }
        else
        {
            return IsMatchingWithInterface(handlerType.GetInterface(handlerInterface.Name), handlerInterface);
        }

        return false;
    }

    private static bool CanBeCastTo(this Type pluggedType, Type pluginType)
    {
        if (pluggedType == pluginType)
        {
            return true;
        }

        return pluginType.GetTypeInfo().IsAssignableFrom(pluggedType.GetTypeInfo());
    }

    private static IEnumerable<Type> FindInterfacesThatClose(this Type pluggedType, Type templateType)
    {
        return FindInterfacesThatClosesCore(pluggedType, templateType).Distinct();
    }

    private static IEnumerable<Type> FindInterfacesThatClosesCore(Type pluggedType, Type templateType)
    {
        if (!pluggedType.IsClassImplementation())
        {
            yield break;
        }

        if (!templateType.GetTypeInfo().IsInterface)
        {
            yield break;
        }

        foreach (var interfaceType in pluggedType.GetInterfaces())
        {
            if (interfaceType.GetTypeInfo().IsGenericType && (interfaceType.GetGenericTypeDefinition() == templateType))
            {
                yield return interfaceType;
            }
        }
    }

    private static bool IsClassImplementation(this Type type)
        => !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
}