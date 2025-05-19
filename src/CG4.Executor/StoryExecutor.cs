namespace CG4.Executor
{
    /// <summary>
    /// Implemetation of <see cref="IStoryExecutor"/>.
    /// </summary>
    public class StoryExecutor : BaseExecutor, IStoryExecutor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoryExecutor"/> class.
        /// </summary>
        /// <param name="provider">Mechanism for retrieving a service object.</param>
        public StoryExecutor(IServiceProvider provider)
            : base(provider)
        {
        }
    }
}
