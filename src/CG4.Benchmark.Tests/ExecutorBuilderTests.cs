using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using bgTeam;
using bgTeam.Impl;
using CG4.Benchmark.Tests.Preparation;
using CG4.Executor;
using CG4.Executor.Story;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CG4.Benchmark.Tests
{
    [MemoryDiagnoser]
    public class ExecutorBuilderTests
    {
        private readonly IStoryExecutor _newBuilder;
        private readonly StoryBuilder _oldBuilder;
        private readonly IMediator _mediatr;

        public ExecutorBuilderTests()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<bgTeam.IStory<TestStoryContext, int>, TestStory>();
            collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            collection.AddExecutors(options =>
            {
                var executionTypes = new[] { typeof(IStory<>), typeof(Executor.Story.IStory<,>) };
                options.ExecutorInterfaceType = typeof(IStoryExecutor);
                options.ExecutorImplementationType = typeof(StoryExecutor);
                options.ExecutorLifetime = ServiceLifetime.Singleton;
                options.ExecutionTypes = executionTypes;
                options.ExecutionTypesLifetime = ServiceLifetime.Transient;
            }, AppDomain.CurrentDomain.GetAssemblies());


            var provider = collection.BuildServiceProvider();

            _newBuilder = provider.GetRequiredService<IStoryExecutor>();
            var factory = new StoryFactory(provider);
            _oldBuilder = new StoryBuilder(factory);
            _mediatr = provider.GetRequiredService<IMediator>();
        }

        [Benchmark]
        public Task<int> NewBuilder() => _newBuilder.Execute(new TestStoryContext());

        [Benchmark]
        public Task<int> OldBuilder() => _oldBuilder.Build(new TestStoryContext()).ReturnAsync<int>();

        [Benchmark]
        public Task<int> MediatR() => _mediatr.Send(new TestStoryContext());
    }
}
