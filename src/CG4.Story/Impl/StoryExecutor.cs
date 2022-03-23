namespace CG4.Story.Impl
{
    /// <summary>
    /// Implemetation of <see cref="IStoryExecutor"/>
    /// </summary>
    public class StoryExecutor : BaseExecutor, IStoryExecutor
    {
        public StoryExecutor(IServiceProvider provider)
            : base(provider)
        {
        }

        protected override Type GetResultedExcution() => typeof(IStory<,>);

        protected override Type GetVoidExcution() => typeof(IStory<>);
    }
}