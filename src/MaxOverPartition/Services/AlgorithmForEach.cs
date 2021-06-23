using System;
using System.Collections.Generic;
using MaxOverPartition.Model;

namespace MaxOverPartition.Services
{
    /// <summary>
    /// An optimization using a Dictionary indexed by TPartitionKey
    /// </summary>
    public class AlgorithmForEach : ContractSortingAlgorithm
    {
        public override IEnumerable<Contract?> Run<TPartitionKey, TOrderByKey>(
            ICollection<Contract> items,
            Func<IEnumerable<Contract>, Contract?> windowFunction,
            Func<Contract, TPartitionKey> partitionKeySelector,
            Func<Contract, TOrderByKey> orderingKeySelector)
        {
            var result = new Dictionary<TPartitionKey, Contract?>(items.Count);
            var windowFuctionBuffer = new Contract[2];

            foreach (var item in items)
            {
                var partitionKey = partitionKeySelector(item);
                if (result.ContainsKey(partitionKey) && result[partitionKey] is not null)
                {
                    var orderingKey = orderingKeySelector(item);

                    var previousItem = result[partitionKey]!;
                    var previousOrderingKey = orderingKeySelector(previousItem);

                    if (previousOrderingKey.CompareTo(orderingKey) > 0)
                    {
                        windowFuctionBuffer[0] = previousItem;
                        windowFuctionBuffer[1] = item;
                    }
                    else
                    {
                        windowFuctionBuffer[0] = item;
                        windowFuctionBuffer[1] = previousItem;
                    }

                    result[partitionKey] = windowFunction(windowFuctionBuffer);
                }
                else
                {
                    result[partitionKey] = item;
                }
            }

            return result.Values;
        }
    }
}
