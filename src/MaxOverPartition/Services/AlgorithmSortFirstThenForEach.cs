using System;
using System.Collections.Generic;
using System.Linq;
using MaxOverPartition.Model;

namespace MaxOverPartition.Services
{
    public class AlgorithmSortFirstThenForEach : ContractSortingAlgorithm
    {
        public override IEnumerable<Contract?> Run<TPartitionKey, TOrderByKey>(
            ICollection<Contract> items,
            Func<IEnumerable<Contract>, Contract?> windowFunction,
            Func<Contract, TPartitionKey> partitionKeySelector,
            Func<Contract, TOrderByKey> orderingKeySelector)
        {
            var sortedItems = items.OrderBy(partitionKeySelector);

            TPartitionKey? previousPartitionKey = default;
            Contract? previousItem = default;

            var windowFuctionBuffer = new Contract[2];

            foreach (var item in sortedItems)
            {
                var partitionKey = partitionKeySelector(item);

                if (partitionKey.Equals(previousPartitionKey))
                {
                    if (previousItem is null)
                    {
                        previousItem = item;
                    }
                    else
                    {
                        var previousOrderingKey = orderingKeySelector(previousItem);
                        var orderingKey = orderingKeySelector(item);

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

                        previousItem = windowFunction(windowFuctionBuffer);
                    }
                }
                else
                {
                    if (previousItem is not null)
                    {
                        yield return previousItem;
                    }

                    previousItem = item;
                    previousPartitionKey = partitionKey;
                }
            }

            if (previousItem is not null)
            {
                yield return previousItem;
            }
        }
    }
}
