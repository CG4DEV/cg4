using System.Reflection;
using CG4.Executor.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CG4.Executor
{
    /// <summary>
    /// Базовый класс выполнения стори.
    /// </summary>
    public abstract class BaseExecutor
    {
        protected readonly IServiceProvider _provider;

        public BaseExecutor(IServiceProvider provider)
        {
            _provider = provider;
        }
        
        /// <summary>
        /// Выполнение с возвращаемым значением.
        /// </summary>
        /// <param name="context">Контекст стори.</param>
        /// <typeparam name="TStoryResult">Тип возвращаемого значения.</typeparam>
        /// <returns>Возвращаемое значение стори.</returns>
        /// <exception cref="InvalidOperationException">Не найден исполнитель стори по переданному контексту.</exception>
        public Task<TStoryResult> Execute<TStoryResult>(IResult<TStoryResult> context)
        {
            if (!CacheExecutor.TryGetValue(context.GetType(), out var type))
            {
                throw InvalidOperationException(context.GetType().Name);
            }

            return (Task<TStoryResult>)Invoke(type.Method, type.ExecutionType, context);
        }
        
        /// <summary>
        /// Выполнение.
        /// </summary>
        /// <param name="context">Контекст стори.</param>
        /// <exception cref="InvalidOperationException">Не найден исполнитель стори по переданному контексту.</exception>
        public Task Execute(IResult context)
        {
            if (!CacheExecutor.TryGetValue(context.GetType(), out var type))
            {
                throw InvalidOperationException(context.GetType().Name);
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
