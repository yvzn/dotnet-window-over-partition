using System;
using System.Collections.Generic;
using System.Linq;
using MaxOverPartition.Model;

namespace MaxOverPartition.Services
{
    /// <summary>
    /// Another approach using LINQ, sorting the whole collection then grouping
    /// (to check if global sorting is more efficient than micro-sorting every partition)
    /// </summary>
    public class AlgorithmSortFirstThenGroupBy : ContractSortingAlgorithm
    {
        public override IEnumerable<Contract?> Run<TPartitionKey, TOrderByKey>(ICollection<Contract> items, Func<IEnumerable<Contract>, Contract?> windowFunction, Func<Contract, TPartitionKey> partitionKeySelector, Func<Contract, TOrderByKey> orderingKeySelector)
        {
            return items
                .OrderBy(partitionKeySelector).ThenByDescending(orderingKeySelector)
                .GroupBy(partitionKeySelector)
                .Select(windowFunction);
        }
    }
}
