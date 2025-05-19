namespace CG4.Executor
{
    /// <summary>
    /// Интерфейс стори с возвращаемым значением.
    /// </summary>
    /// <typeparam name="TStoryContext">Тип контекста.</typeparam>
    /// <typeparam name="TStoryResult">Тип возвращаемого значения.</typeparam>
    public interface IStory<in TStoryContext, TStoryResult> : IExecution<TStoryContext, TStoryResult>
        where TStoryContext : IResult<TStoryResult>
    {
    }

    /// <summary>
    /// Интерфейс стори.
    /// </summary>
    /// <typeparam name="TStoryContext">Тип контекста.</typeparam>
    public interface IStory<in TStoryContext> : IExecution<TStoryContext>
        where TStoryContext : IResult
    {
    }
}
