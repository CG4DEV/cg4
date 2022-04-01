using System.Reflection;
using CG4.Story.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CG4.Story
{
    public abstract class BaseExecutor
    {
        protected readonly IServiceProvider _provider;

        public BaseExecutor(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc/>
        public Task<TStoryResult> Execute<TStoryResult>(IResult<TStoryResult> context)
        {
            if (!CacheExecutor.TryGetValue(context.GetType(), out var type))
            {
                throw new InvalidOperationException($"{context.GetType().Name} must have implementation {typeof(IExecution<,>).Name} or {typeof(IExecution<>).Name}");
            }

            return (Task<TStoryResult>)Invoke(type.Method, type.ExecutionType, context);
        }

        /// <inheritdoc/>
        public Task Execute(IResult context)
        {
            if (!CacheExecutor.TryGetValue(context.GetType(), out var type))
            {
                throw new InvalidOperationException($"{context.GetType().Name} must have implementation {typeof(IExecution<,>).Name} or {typeof(IExecution<>).Name}");
            }

            return (Task)Invoke(type.Method, type.ExecutionType, context);
        }

        protected object Invoke(MethodInfo method, Type storyType, object context)
        {
            var story = _provider.GetRequiredService(storyType);
            return method.Invoke(story, new[] { context }) ?? InvalidOperationException(storyType.Name);
        }

        private InvalidOperationException InvalidOperationException(string nameType)
        {
            return new InvalidOperationException($"{nameType} must have implementation {typeof(IExecution<,>).Name} or {typeof(IExecution<>).Name}");
        }
    }
}