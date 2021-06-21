using System;
using System.Collections.Generic;
using System.Linq;
using MaxOverPartition.Model;

namespace MaxOverPartition.Services
{
    public class AlgorithmGroupByOrderBy : ContractSortingAlgorithm
    {
        public override IEnumerable<Contract?> Run<TPartitionKey, TOrderByKey>(
            IEnumerable<Contract> items,
            Func<IEnumerable<Contract>, Contract?> windowFunction,
            Func<Contract, TPartitionKey> partitionKeySelector,
            Func<Contract, TOrderByKey> sortingKeySelector)
        {
            return items
                .GroupBy(partitionKeySelector)
                .Select(group => group.OrderByDescending(sortingKeySelector))
                .Select(windowFunction);
        }
    }
}
