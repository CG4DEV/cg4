using System.Threading;
using System.Threading.Tasks;
using CG4.Story;
using MediatR;

namespace CG4.Benchmark.Tests.Preparation
{
    public class TestStory : IStory<TestStoryContext, int>, bgTeam.IStory<TestStoryContext, int>, IRequestHandler<TestStoryContext, int>
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
