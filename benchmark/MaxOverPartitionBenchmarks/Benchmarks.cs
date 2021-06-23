using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using MaxOverPartition.Dataset;
using MaxOverPartition.Model;
using MaxOverPartition.Services;

namespace MaxOverPartitionBenchmarks
{
    [SimpleJob(RuntimeMoniker.Net50)]
    [MemoryDiagnoser]
    public class Benchmarks
    {
        private ICollection<Contract>? dataset;

        [Params(100, 1000, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            dataset = Generator.GetContracts(N);
        }

        [Benchmark(Baseline = true)]
        public void AlgorithmGroupByOrderBy() => new AlgorithmGroupByOrderBy().Run(dataset!).ToList();

        [Benchmark]
        public void AlgorithmForEach() => new AlgorithmForEach().Run(dataset!).ToList();
    }
}
