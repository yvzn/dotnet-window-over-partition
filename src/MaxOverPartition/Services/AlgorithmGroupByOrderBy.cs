using System;
using System.Collections.Generic;
using System.Linq;
using MaxOverPartition.Model;

namespace MaxOverPartition.Services
{
    /// <summary>
    /// A naive approach using LINQ, GroupBy and OrderBy
    /// </summary>
    public class AlgorithmGroupByOrderBy : ContractSortingAlgorithm
    {
        public override IEnumerable<Contract?> Run<TPartitionKey, TOrderByKey>(
            ICollection<Contract> items,
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
