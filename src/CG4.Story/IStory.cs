namespace CG4.Story
{
    /// <summary>
    /// Интерфейс стори.
    /// </summary>
    /// <typeparam name="TStoryContext">Тип контекста.</typeparam>
    /// <typeparam name="TStoryResult">Тип возвращаемого значения.</typeparam>
    public interface IStory<in TStoryContext, TStoryResult>
        where TStoryContext : IResult<TStoryResult>
    {
        /// <summary>
        /// Асинхронно выполняет действия стори и возвращает результат.
        /// </summary>
        /// <param name="context">Контекст стори.</param>
        Task<TStoryResult> ExecuteAsync(TStoryContext context);
    }

    /// <summary>
    /// Интерфейс стори.
    /// </summary>
    /// <typeparam name="TStoryContext">Тип контекста.</typeparam>
    public interface IStory<in TStoryContext>
        where TStoryContext : IResult
    {
        /// <summary>
        /// Асинхронно выполняет действия стори.
        /// </summary>
        /// <param name="context">Контекст стори.</param>
        Task ExecuteAsync(TStoryContext context);
    }
}
