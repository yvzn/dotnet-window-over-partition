# .NET <q>Window Functions</q>

Experimentations on implementing complex filtering and sorting of collections, 
with associated benchmarks to verify performance and memory usage.

## Why?

The original dataset is a collection of items following these rules:
- each item has a `Reference` and a `Version`
- two items can have the same `Reference` but then their `Version` differ

The business case is to filter the collection and remove `Reference`
duplicates, keeping the item with the highest `Version` in case they
share the same `Reference`.

Example:
```js
Run([
	{ Reference: 'A', Version: 1 }, { Reference: 'A', Version: 2 }, 
	{ Reference: 'B', Version: 1 }, 
])

=> returns 
[
	{ Reference: 'A', Version: 2 }, 
	{ Reference: 'B', Version: 1 }, 
]
```

## How?

This would be typically solved in SQL using Window Functions.

The experiment is try to implement similar functionality in C#, using
different algorithms and approaches:
- with or without LINQ
- with or without sorting the collection first

Each algorithm has a few unit tests to check that the contract is respected.

Then each algorithm goes through a micro-benchmark framework 
to verify its performance regarding time of execution and memory usage / memory pressure.

[BenchmarkDotNet](https://benchmarkdotnet.org/)

## Run locally

Requirements:
- .NET >= 5.0

Make sure you always use `--configuration Release` otherwise the tooling
will not work.

Build in *Release* configuration
```bash
dotnet build --configuration Release
```

Run the unit tests
```bash
dotnet test --configuration Release
```

Run the benchmarks
```bash
dotnet run --configuration Release --project .\benchmark\MaxOverPartitionBenchmarks\
```

## Results

The first, naive, LINQ implementation is not that bad (12ms for 10.000 items)
but allocates as much as 3 MB.

A faster option, backed by a Dictionary, does better (3x speed and 6x memory allocated)

Sorting the collection before running the filtering is actually slower in all conditions
(I would have guessed the contrary) but has a better memory footprint: less memory allocated,
less garbage collector pressure

Sample output:

|                        Method |     N |         Mean |      Error |     StdDev | Ratio | RatioSD |    Gen 0 |    Gen 1 |   Gen 2 | Allocated |
|------------------------------ |------ |-------------:|-----------:|-----------:|------:|--------:|---------:|---------:|--------:|----------:|
|       AlgorithmGroupByOrderBy |   100 |     30.77 us |   0.607 us |   0.851 us |  1.00 |    0.00 |  18.6157 |        - |       - |     29 KB |
| AlgorithmSortFirstThenGroupBy |   100 |    108.35 us |   2.160 us |   3.362 us |  3.54 |    0.14 |  12.3291 |        - |       - |     19 KB |
|              AlgorithmForEach |   100 |     16.08 us |   0.269 us |   0.331 us |  0.52 |    0.02 |   3.6011 |        - |       - |      6 KB |
| AlgorithmSortFirstThenForEach |   100 |     99.31 us |   1.811 us |   2.479 us |  3.23 |    0.13 |   3.5400 |        - |       - |      6 KB |
|                               |       |              |            |            |       |         |          |          |         |           |
|       AlgorithmGroupByOrderBy |  1000 |    390.41 us |   7.791 us |  10.664 us |  1.00 |    0.00 | 139.6484 |  29.2969 |       - |    273 KB |
| AlgorithmSortFirstThenGroupBy |  1000 |  1,805.76 us |  33.100 us |  62.976 us |  4.66 |    0.21 |  87.8906 |  17.5781 |       - |    175 KB |
|              AlgorithmForEach |  1000 |    187.60 us |   3.457 us |   4.731 us |  0.48 |    0.02 |  33.2031 |        - |       - |     52 KB |
| AlgorithmSortFirstThenForEach |  1000 |  1,516.40 us |  30.205 us |  39.275 us |  3.89 |    0.15 |  29.2969 |        - |       - |     46 KB |
|                               |       |              |            |            |       |         |          |          |         |           |
|       AlgorithmGroupByOrderBy | 10000 | 12,986.37 us | 257.056 us | 376.790 us |  1.00 |    0.00 | 484.3750 | 203.1250 | 46.8750 |  2,911 KB |
| AlgorithmSortFirstThenGroupBy | 10000 | 30,933.42 us | 591.147 us | 680.765 us |  2.38 |    0.08 | 281.2500 | 125.0000 | 31.2500 |  1,935 KB |
|              AlgorithmForEach | 10000 |  4,397.78 us |  86.869 us | 161.018 us |  0.34 |    0.02 |  70.3125 |  39.0625 | 23.4375 |    558 KB |
| AlgorithmSortFirstThenForEach | 10000 | 24,571.71 us | 423.984 us | 375.850 us |  1.90 |    0.05 |  93.7500 |        - |       - |    550 KB |
