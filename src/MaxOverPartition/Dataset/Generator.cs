using System;
using System.Collections.Generic;
using System.Linq;
using MaxOverPartition.Model;

namespace MaxOverPartition.Dataset
{
    public class Generator
    {
        public static ICollection<Contract> GetContracts(int referenceCount)
        {
            var shuffledDistribution = VersionCountVsIndexDistribution.OrderBy(_ => ReproductibleRandom.Next()).ToArray();

            var contracts = new List<Contract>(capacity: (int)(referenceCount * 1.6));

            for (int index = 0; index < referenceCount; ++index)
            {
                var versionCount = shuffledDistribution[index % shuffledDistribution.Length];

                contracts.AddRange(GetVersionsOfContract(versionCount));
            }

            return contracts;
        }

        public static IEnumerable<Contract> GetVersionsOfContract(int versionCount)
        {
            var contractTemplate = CreateContract();

            for (int version = 0; version < versionCount; ++version)
            {
                var contract = (Contract)contractTemplate.Clone();
                contract.Version = version + 1;
                yield return contract;
            }
        }

        private static Contract CreateContract()
        {
            var reference = CreateContractReference();
            return new(reference, 1);
        }

        private static string CreateContractReference() =>
            ReproductibleRandom.Next().ToString("x");

        private static readonly Random ReproductibleRandom = new(Seed: 11091980);
        private static readonly int[] VersionCountVsIndexDistribution = new[] { 1, 1, 1, 1, 1, 1, 2, 2, 2, 3 };
    }
}
