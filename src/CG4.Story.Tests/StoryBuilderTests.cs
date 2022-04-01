using System;
using CG4.Story.Extensions;
using CG4.Story.Impl;
using CG4.Story.Tests.Preparation;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CG4.Story.Tests
{
    public class StoryBuilderTests
    {
        private readonly ServiceProvider _provider;

        public StoryBuilderTests()
        {
            var collection = new ServiceCollection();
            collection.AddExecutors(config =>
            {
                var executionTypes = new[] { typeof(IStory<>), typeof(IStory<,>) };
                config.AddExecutionTypes(executionTypes, ServiceLifetime.Transient);
            }, typeof(StoryBuilderTests).Assembly);
            _provider = collection.BuildServiceProvider();
        }

        [Fact]
        public void StoryBuilder_ResolveByTestStoryContext_WasResolved()
        {
            var builder = new StoryExecutor(_provider);
            var result = builder.Execute(new TestStoryContext());
            Assert.NotNull(result);
            Assert.True(result.IsCompleted);
            Assert.Equal(0, result.Result);
        }

        [Fact]
        public void StoryBuilder_ResolveByTestVoidStoryContext_WasResolved()
        {
            var builder = new StoryExecutor(_provider);
            var result = builder.Execute(new TestVoidStoryContext());
            Assert.NotNull(result);
            Assert.True(result.IsCompleted);
        }

        [Fact]
        public void StoryBuilder_ResolveNotRegisteredStoryContext_Exception()
        {
            var builder = new StoryExecutor(_provider);
            Assert.Throws<InvalidOperationException>(() => { builder.Execute((IResult<int>)new NotRegisteredStoryContext()); });
        }

        [Fact]
        public void StoryBuilder_ResolveNotRegisteredVoidStoryContext_Exception()
        {
            var builder = new StoryExecutor(_provider);
            Assert.Throws<InvalidOperationException>(() => { builder.Execute((IResult)new NotRegisteredStoryContext()); });
        }
    }
}