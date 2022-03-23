using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CG4.Story
{
    public abstract class BaseExecutor
    {
        protected static readonly Dictionary<Type, (Type StoryType, MethodInfo Method)> _cache = new();
        protected readonly IServiceProvider _provider;

        public BaseExecutor(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected abstract Type GetResultedExcution();

        protected abstract Type GetVoidExcution();

        /// <inheritdoc/>
        public Task<TStoryResult> Execute<TStoryResult>(IResult<TStoryResult> context)
        {
            var type = GetStoryType(context.GetType(), typeof(TStoryResult));
            return (Task<TStoryResult>)Invoke(type.Method, type.StoryType, context);
        }

        /// <inheritdoc/>
        public Task Execute(IResult context)
        {
            var type = GetStoryType(context.GetType());
            return (Task)Invoke(type.Method, type.StoryType, context);
        }

        protected object Invoke(MethodInfo method, Type storyType, object context)
        {
            var story = _provider.GetRequiredService(storyType);
            return method.Invoke(story, new[] { context }) ?? InvalidOperationException(storyType.Name);
        }

        protected (Type StoryType, MethodInfo Method) GetStoryType(Type contextType, Type? resultType = null)
        {
            if (!_cache.TryGetValue(contextType, out var storyType))
            {
                Type type;
                Type baseInterface;
                if (resultType != null)
                {
                    type = GetResultedExcution().MakeGenericType(contextType, resultType);
                    baseInterface = typeof(IExecution<,>).MakeGenericType(contextType, resultType);
                }
                else
                {
                    type = GetVoidExcution().MakeGenericType(contextType);
                    baseInterface = typeof(IExecution<>).MakeGenericType(contextType);
                }

                baseInterface = type.GetInterfaces().FirstOrDefault(x => x == baseInterface) ?? throw InvalidOperationException(type.Name);
                var method = baseInterface.GetMethod("ExecuteAsync") ?? throw InvalidOperationException(type.Name);
                storyType = new(type, method);
                _cache[contextType] = storyType;
            }

            return storyType;
        }

        private InvalidOperationException InvalidOperationException(string nameType)
        {
            return new InvalidOperationException($"{nameType} must have implementation {typeof(IExecution<,>).Name} or {typeof(IExecution<>).Name}"); ;
        }
    }
}
