using System.Collections.Concurrent;
using Volo.Abp.DependencyInjection;

namespace AetherLink.Indexer.Providers;

public interface ITransactionEventProvider
{
    void SaveTransactionEventId(string chainId, long blockHeight, string entityId);
    void DeleteTransactionEventId(string chainId, long blockHeight);
    List<string> GetTransactionEventIds(string chainId, long blockHeight);
}

public class TransactionEventProvider : ITransactionEventProvider, ISingletonDependency
{
    private readonly Dictionary<string, Dictionary<long, List<string>>> _transactionEvents;

    public TransactionEventProvider()
    {
        _transactionEvents = new();
    }

    public void SaveTransactionEventId(string chainId, long blockHeight, string entityId)
    {
        if (!_transactionEvents.TryGetValue(chainId, out var chainDict))
        {
            _transactionEvents[chainId] = new() { { blockHeight, new() { entityId } } };
            return;
        }

        if (!chainDict.TryGetValue(blockHeight, out var chainBlockDict))
        {
            _transactionEvents[chainId][blockHeight] = new() { entityId };
            return;
        }

        chainBlockDict.Add(entityId);
        _transactionEvents[chainId][blockHeight] = chainBlockDict;
    }

    public void DeleteTransactionEventId(string chainId, long blockHeight)
    {
        if (_transactionEvents.TryGetValue(chainId, out var chainDict)) chainDict.Remove(blockHeight);
    }

    public List<string> GetTransactionEventIds(string chainId, long blockHeight)
        => !_transactionEvents.TryGetValue(chainId, out var chainDict)
            ? new()
            : !chainDict.TryGetValue(blockHeight, out var chainBlockDict)
                ? new()
                : chainBlockDict;
}