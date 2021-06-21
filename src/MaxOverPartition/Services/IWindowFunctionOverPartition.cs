using System;
using System.Collections.Generic;

namespace MaxOverPartition.Services
{
    internal interface IWindowFunctionOverPartition<TItem, TResult>
    {
        IEnumerable<TResult> Run<TPartitionKey, TOrderByKey>(
            IEnumerable<TItem> items,
            Func<IEnumerable<TItem>, TResult> windowFunction,
            Func<TItem, TPartitionKey> partitionKeySelector,
            Func<TItem, TOrderByKey> sortingKeySelector);
    }
}
