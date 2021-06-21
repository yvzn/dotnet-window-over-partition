using System;
using System.Collections.Generic;
using System.Linq;
using MaxOverPartition.Model;

namespace MaxOverPartition.Services
{
    public abstract class ContractSortingAlgorithm : IWindowFunctionOverPartition<Contract, Contract?>
    {
        public abstract IEnumerable<Contract?> Run<TPartitionKey, TOrderByKey>(
            IEnumerable<Contract> items,
            Func<IEnumerable<Contract>, Contract?> windowFunction,
            Func<Contract, TPartitionKey> partitionKeySelector,
            Func<Contract, TOrderByKey> sortingKeySelector);

        public IEnumerable<Contract?> Run(IEnumerable<Contract> items)
        {
            return Run(
                items,
                windowFunction: contractsOfPartition => contractsOfPartition.FirstOrDefault(),
                partitionKeySelector: contract => contract.Reference,
                sortingKeySelector: contract => contract.Version
                );
        }
    }
}
