using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CG4.Story.Impl
{
    /// <summary>
    /// Implemetation of <see cref="IStoryExecutor"/>
    /// </summary>
    public class StoryExecutor : IStoryExecutor
    {
        private static readonly Dictionary<Type, (Type StoryType, MethodInfo Method)> _cache = new();
        private readonly IServiceProvider _provider;

        public StoryExecutor(IServiceProvider provider)
        {
            _provider = provider;
        }

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
            return method.Invoke(story, new[] { context }) ?? throw new InvalidOperationException("Some shit");
        }

        protected (Type StoryType, MethodInfo Method) GetStoryType(Type contextType, Type? resultType = null)
        {
            if (!_cache.TryGetValue(contextType, out var storyType))
            {
                Type type;
                if (resultType != null)
                {
                    type = typeof(IStory<,>).MakeGenericType(contextType, resultType);
                }
                else
                {
                    type = typeof(IStory<>).MakeGenericType(contextType);
                }

                var method = type.GetMethod("ExecuteAsync") ?? throw new InvalidOperationException("Some shit");
                storyType = new(type, method);
                _cache[contextType] = storyType;
            }

            return storyType;
        }
    }
}