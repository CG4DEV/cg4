using System.Threading.Tasks;
using CG4.Story.Impl;

namespace CG4.Story.Tests.Preparation
{
    public class TestStory : IStory<TestStoryContext, int>, IStory<TestVoidStoryContext>
    {
        public Task<int> ExecuteAsync(TestStoryContext context)
        {
            return Task.FromResult(0);
        }

        public Task ExecuteAsync(TestVoidStoryContext context)
        {
            return Task.CompletedTask;
        }
    }

    public class TestStoryContext : IResult<int>
    {
    }

    public class TestVoidStoryContext : IResult
    {
    }

    public class NotRegisteredStoryContext : IResult<int>, IResult
    {
    }
}
