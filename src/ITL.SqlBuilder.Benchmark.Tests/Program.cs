using BenchmarkDotNet.Running;

namespace ITL.SqlBuilder.Benchmark.Tests
{
    internal class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<SqlBuilderTests>();
        }
    }
}
