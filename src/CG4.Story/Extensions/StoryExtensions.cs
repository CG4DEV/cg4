using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CG4.Story.Extensions;

public static class StoryExtensions
{
    public static IServiceCollection AddExecutors(
        this IServiceCollection services,
        Action<ExecutorServiceConfiguration> configuration,
        params Type[] handlerAssemblyMarkerTypes)
        => services.AddExecutors(configuration, handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly).ToArray());

    public static IServiceCollection AddExecutors(this IServiceCollection services,
        Action<ExecutorServiceConfiguration> configuration,
        params Assembly[] assembliesToScan)
    {
        if (!assembliesToScan.Any())
        {
            throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
        }

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        var typeInfos = assembliesToScan
            .SelectMany(t => t.DefinedTypes)
            .Distinct()
            .ToArray();

        var serviceConfig = new ExecutorServiceConfiguration();
        configuration.Invoke(serviceConfig);

        AddExecutorsClasses(services, typeInfos, serviceConfig);
        return services;
    }

    private static void AddExecutorsClasses(IServiceCollection services, TypeInfo[] typeInfos, ExecutorServiceConfiguration config)
    {
        var executorInterfaces = new[]
        {
            typeof(IExecution<>),
            typeof(IExecution<,>),
        };

        var internalTypes = config?.ExecutionTypes.ToArray() ?? executorInterfaces;
        foreach (var internalType in internalTypes)
        {
            var impls = new List<Type>(); //concrete classes
            var baseInterfaces = new List<Type>(); //executor child interfaces

            foreach (var type in typeInfos)
            {
                var similarInterfaces = type.FindInterfacesThatClose(internalType).ToArray();
                if (!similarInterfaces.Any())
                    continue;

                if (type.IsClassImplementation())
                {
                    impls.Add(type);
                }

                foreach (var similar in similarInterfaces)
                {
                    if (baseInterfaces.Contains(similar))
                        continue;

                    baseInterfaces.Add(similar);
                }
            }

            foreach (var @interface in baseInterfaces)
            {
                var exactMatches = impls.Where(x => x.CanBeCastTo(@interface)).ToList();

                if (exactMatches.Count > 1)
                {
                    exactMatches.RemoveAll(m => !IsMatchingWithInterface(m, @interface));
                }

                foreach (var type in exactMatches)
                {
                    var lengthArguments = internalType.GetGenericArguments().Length;
                    var baseInterface = executorInterfaces.First(x => x.GetGenericArguments().Length == lengthArguments) ?? throw InvalidOperationException(type.Name);
                    baseInterface = baseInterface.MakeGenericType(@interface.GetGenericArguments());
                    var method = baseInterface.GetMethod("ExecuteAsync") ?? throw InvalidOperationException(type.Name);
                    (Type st, MethodInfo mtd) storyType = new (@interface, method);
                    CacheExecutor.TryAdd(@interface.GetGenericArguments().First(), storyType);
                    services.TryAdd(new ServiceDescriptor(@interface, type, config?.ExecutionTypesLifetime ?? ServiceLifetime.Transient));
                }
            }
        }
    }

    private static InvalidOperationException InvalidOperationException(string nameType)
    {
        return new InvalidOperationException($"{nameType} must have implementation {typeof(IExecution<,>).Name} or {typeof(IExecution<>).Name}");
    }

    private static bool IsMatchingWithInterface(Type handlerType, Type handlerInterface)
    {
        if (handlerType == null || handlerInterface == null)
        {
            return false;
        }

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