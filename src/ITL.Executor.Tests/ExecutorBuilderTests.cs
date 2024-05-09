using System;
using System.Threading.Tasks;
using ITL.Executor.Extensions;
using ITL.Executor.Story;
using ITL.Executor.Tests.Preparation;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ITL.Executor.Tests
{
    public class ExecutorBuilderTests
    {
        private readonly ServiceProvider _provider;

        public ExecutorBuilderTests()
        {
            var collection = new ServiceCollection();
            collection.AddExecutors(options =>
            {
                var executionTypes = new[] { typeof(IStory<>), typeof(IStory<,>) };
                options.ExecutorInterfaceType = typeof(IStoryExecutor);
                options.ExecutorImplementationType = typeof(StoryExecutor);
                options.ExecutorLifetime = ServiceLifetime.Singleton;
                options.ExecutionTypes = executionTypes;
                options.ExecutionTypesLifetime = ServiceLifetime.Transient;
            }, typeof(ExecutorBuilderTests).Assembly);

            _provider = collection.BuildServiceProvider();
        }

        [Fact]
        public void StoryBuilder_ResolveByTestExecutorContext_WasResolved()
        {
            var builder = (IStoryExecutor)_provider.GetRequiredService(typeof(IStoryExecutor));
            var result = builder.Execute(new TestExecutorContext());
            Assert.NotNull(result);
            Assert.True(result.IsCompleted);
            Assert.Equal(0, result.Result);
        }

        [Fact]
        public void StoryBuilder_ResolveByTestVoidExecutorContext_WasResolved()
        {
            var builder = (IStoryExecutor)_provider.GetRequiredService(typeof(IStoryExecutor));
            var result = builder.Execute(new TestVoidExecutorContext());
            Assert.NotNull(result);
            Assert.True(result.IsCompleted);
        }

        [Fact]
        public async Task StoryBuilder_ResolveNotRegisteredExecutorContext_Exception()
        {
            var builder = (IStoryExecutor)_provider.GetRequiredService(typeof(IStoryExecutor));

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await builder.Execute(new NotRegisteredExecutorContext()));
        }

        [Fact]
        public async Task StoryBuilder_ResolveNotRegisteredVoidStoryExecutor_Exception()
        {
            var builder = (IStoryExecutor)_provider.GetRequiredService(typeof(IStoryExecutor));
            await Assert.ThrowsAsync<InvalidOperationException>(() => builder.Execute(new NotRegisteredExecutorContext()));
        }
    }
}