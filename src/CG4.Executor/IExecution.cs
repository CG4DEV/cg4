namespace CG4.Executor
{
    /// <summary>
    /// Выполнение с возвращаемым значением.
    /// </summary>
    /// <typeparam name="TExecutionContext">Тип контекста.</typeparam>
    /// <typeparam name="TExecutionResult">Тип возвращаемого значения.</typeparam>
    public interface IExecution<in TExecutionContext, TExecutionResult>
        where TExecutionContext : IResult<TExecutionResult>
    {
        /// <summary>
        /// Асинхронно выполняет алгоритм, связанный с контекстом.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <returns>Возвращаемое значение контекста.</returns>
        Task<TExecutionResult> ExecuteAsync(TExecutionContext context);
    }

    /// <summary>
    /// Выполнение.
    /// </summary>
    /// <typeparam name="TExecutionContext">Тип контекста.</typeparam>
    public interface IExecution<in TExecutionContext>
        where TExecutionContext : IResult
    {
        /// <summary>
        /// Асинхронно выполняет алгоритм, связанный с контекстом.
        /// </summary>
        /// <param name="context">Контекст.</param>
        Task ExecuteAsync(TExecutionContext context);
    }
}
