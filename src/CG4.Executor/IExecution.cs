namespace CG4.Executor
{
    public interface IExecution<in TExecutionContext, TExecutionResult>
        where TExecutionContext : IResult<TExecutionResult>
    {
        Task<TExecutionResult> ExecuteAsync(TExecutionContext context);
    }

    public interface IExecution<in TExecutionContext>
        where TExecutionContext : IResult
    {
        Task ExecuteAsync(TExecutionContext context);
    }
}
