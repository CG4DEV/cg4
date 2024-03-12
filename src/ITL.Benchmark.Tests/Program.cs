using BenchmarkDotNet.Running;

namespace ITL.Benchmark.Tests
{
    internal class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<ExecutorBuilderTests>();
        }
    }
}
