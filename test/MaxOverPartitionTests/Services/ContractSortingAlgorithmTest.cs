using System;
using System.Linq;
using FluentAssertions;
using MaxOverPartition.Dataset;
using MaxOverPartition.Model;
using MaxOverPartition.Services;
using Xunit;

namespace MaxOverPartition.Tests.Services
{
    public class TestedAlgorithms : TheoryData<ContractSortingAlgorithm>
    {
        public TestedAlgorithms()
        {
            Add(new AlgorithmGroupByOrderBy());
            Add(new AlgorithmSortFirstThenGroupBy());
            Add(new AlgorithmForEach());
            Add(new AlgorithmSortFirstThenForEach());
        }
    }

    public class ContractSortingAlgorithmTest
    {
        public class Run
        {
            [Theory]
            [ClassData(typeof(TestedAlgorithms))]
            public void Should_sort_an_empty_collection(ContractSortingAlgorithm algorithm)
            {
                // Given
                var items = Array.Empty<Contract>();

                // When
                var actual = algorithm.Run(items);

                // Then
                actual.Should().HaveCount(0);
            }

            [Theory]
            [ClassData(typeof(TestedAlgorithms))]
            public void Should_return_a_value_for_every_reference(ContractSortingAlgorithm algorithm)
            {
                // Given
                var items = Generator.GetContracts(10);

                // When
                var actual = algorithm.Run(items);

                // Then
                actual
                    .Should()
                    .HaveCount(10);
            }

            [Theory]
            [ClassData(typeof(TestedAlgorithms))]
            public void Should_return_a_unique_value_for_each_reference(ContractSortingAlgorithm algorithm)
            {
                // Given
                var items = Generator.GetContracts(10);

                // When
                var actual = algorithm.Run(items);

                // Then
                actual
                    .GroupBy(contract => contract?.Reference)
                    .Select(group => new { group.Key, Count = group.Count() })
                    .Should()
                    .OnlyContain(reference => reference.Count == 1);
            }

            [Theory]
            [ClassData(typeof(TestedAlgorithms))]
            public void Should_keep_the_max_version_when_multiple_versions(ContractSortingAlgorithm algorithm)
            {
                // Given
                var items = Generator.GetVersionsOfContract(3).ToList();

                // When
                var actual = algorithm.Run(items);

                // Then
                actual.Should().HaveCount(1);
                actual.First()?.Version.Should().Be(3);
            }
        }
    }
}
;
