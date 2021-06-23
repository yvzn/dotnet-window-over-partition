using System;
using System.Collections.Generic;
using System.Linq;
using MaxOverPartition.Model;

namespace MaxOverPartition.Services
{
    public abstract class ContractSortingAlgorithm : IWindowFunctionOverPartition<Contract, Contract?>
    {
        public abstract IEnumerable<Contract?> Run<TPartitionKey, TOrderByKey>(
            ICollection<Contract> items,
            Func<IEnumerable<Contract>, Contract?> windowFunction,
            Func<Contract, TPartitionKey> partitionKeySelector,
            Func<Contract, TOrderByKey> orderingKeySelector)
            where TPartitionKey : notnull
            where TOrderByKey : IComparable<TOrderByKey>;

        public IEnumerable<Contract?> Run(ICollection<Contract> items)
        {
            return Run(
                items,
                windowFunction: contractsOfPartition => contractsOfPartition.FirstOrDefault(),
                partitionKeySelector: contract => contract.Reference,
                orderingKeySelector: contract => contract.Version
                );
        }
    }
}
