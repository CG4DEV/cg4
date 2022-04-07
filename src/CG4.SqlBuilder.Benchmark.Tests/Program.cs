using BenchmarkDotNet.Running;

namespace CG4.SqlBuilder.Benchmark.Tests
{
    internal class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<SqlBuilderTests>();
        }
    }
}
