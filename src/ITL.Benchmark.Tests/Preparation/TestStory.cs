using System.Threading;
using System.Threading.Tasks;
using ITL.Executor;
using ITL.Executor.Story;
using MediatR;

namespace ITL.Benchmark.Tests.Preparation
{
    public class TestStory : IStory<TestStoryContext, int>, ITLTeam.IStory<TestStoryContext, int>, IRequestHandler<TestStoryContext, int>
    {
        public Task<int> ExecuteAsync(TestStoryContext context)
        {
            return Task.FromResult(0);
        }

        public Task<int> Handle(TestStoryContext request, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }

    public class TestStoryContext : IResult<int>, IRequest<int>
    {
    }
}
