namespace CG4.Story
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
