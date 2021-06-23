using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            return contracts.OrderBy(_ => ReproductibleRandom.Next()).ToList();
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
            return new(reference, 1)
            {
                DummyPropertyA = CreateRandomString(),
                DummyPropertyB = CreateRandomString(),
                DummyPropertyC = CreateRandomString(),
                DummyPropertyD = CreateRandomString(),
                DummyPropertyE = CreateRandomString(),
                DummyPropertyF = CreateRandomString(),
                DummyPropertyG = CreateRandomString(),
                DummyPropertyH = CreateRandomString(),
                DummyPropertyI = CreateRandomString(),
                DummyPropertyJ = CreateRandomString(),
                DummyPropertyK = CreateRandomString(),
                DummyPropertyL = CreateRandomString(),
                DummyPropertyM = CreateRandomString(),
                DummyPropertyN = CreateRandomString(),
                DummyPropertyO = CreateRandomString(),
                DummyPropertyP = CreateRandomString(),
                DummyPropertyQ = CreateRandomString(),
                DummyPropertyR = CreateRandomString(),
                DummyPropertyS = CreateRandomString(),
                DummyPropertyT = CreateRandomString(),
                DummyPropertyU = CreateRandomString(),
                DummyPropertyV = CreateRandomString(),
                DummyPropertyW = CreateRandomString(),
                DummyPropertyX = CreateRandomString(),
                DummyPropertyY = CreateRandomString(),
                DummyPropertyZ = CreateRandomString(),
            };
        }

        private static string CreateContractReference() =>
            ReproductibleRandom.Next().ToString("x");

        private static string CreateRandomString()
        {
            var buffer = ArrayPool<byte>.Shared.Rent(10);
            ReproductibleRandom.NextBytes(buffer);
            var value = Encoding.UTF8.GetString(buffer);
            ArrayPool<byte>.Shared.Return(buffer);
            return value;
        }

        private static readonly Random ReproductibleRandom = new(Seed: 11091980);
        private static readonly int[] VersionCountVsIndexDistribution = new[] { 1, 1, 1, 1, 1, 1, 2, 2, 2, 3 };
    }
}
