using BenchmarkDotNet.Running;

namespace MaxOverPartitionBenchmarks
{

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
