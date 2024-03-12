namespace ITLTeam.Impl
{
    using System;
    using ITLTeam;
    using Microsoft.Extensions.DependencyInjection;

    /// <inheritdoc/>
    public class StoryFactory : IStoryFactory
    {
        private readonly IServiceProvider _provider;

        public StoryFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc/>
        public IStory<TStoryContext, TResult> Create<TStoryContext, TResult>()
        {
            return _provider.GetRequiredService<IStory<TStoryContext, TResult>>();
        }
    }
}
