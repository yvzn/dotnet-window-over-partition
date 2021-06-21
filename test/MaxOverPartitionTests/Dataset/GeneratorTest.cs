using System.Linq;
using FluentAssertions;
using MaxOverPartition.Dataset;
using Xunit;

namespace MaxOverPartition.Tests.Dataset
{
    public class GeneratorTest
    {
        public class GetContracts
        {
            [Fact]
            public void Should_generate_more_than_ten_contracts()
            {
                // Given
                var referenceCount = 10;

                // When
                var actual = Generator.GetContracts(referenceCount);

                // Then
                actual.Should().HaveCountGreaterOrEqualTo(referenceCount);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void Should_generate_some_references_with_n_versions(int n)
            {
                // Given
                var referenceCount = 10;

                // When
                var actual = Generator.GetContracts(referenceCount);

                // Then
                actual
                    .GroupBy(contract => contract.Reference)
                    .Where(group => group.Count() == n)
                    .Should().NotBeEmpty();
            }
        }
    }
}
