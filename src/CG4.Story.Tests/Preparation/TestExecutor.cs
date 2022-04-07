using System.Threading.Tasks;
using CG4.Executor;
using CG4.Executor.Story;

namespace CG4.Story.Tests.Preparation
{
    public class TestExecutor : IStory<TestExecutorContext, int>, IStory<TestVoidExecutorContext>
    {
        public Task<int> ExecuteAsync(TestExecutorContext context)
        {
            return Task.FromResult(0);
        }

        public Task ExecuteAsync(TestVoidExecutorContext context)
        {
            return Task.CompletedTask;
        }
    }

    public class TestExecutorContext : IResult<int>
    {
    }

    public class TestVoidExecutorContext : IResult
    {
    }

    public class NotRegisteredExecutorContext : IResult<int>, IResult
    {
    }
}
