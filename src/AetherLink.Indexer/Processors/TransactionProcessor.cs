using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Constants;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.Providers;

namespace AetherLink.Indexer.Processors;

public class TransactionProcessor : TransactionProcessorBase
{
    private readonly IAeFinderLogger _logger;
    private readonly ITransactionEventProvider _eventProvider;

    public TransactionProcessor(IAeFinderLogger logger, ITransactionEventProvider eventProvider)
    {
        _logger = logger;
        _eventProvider = eventProvider;
    }

    public override async Task ProcessAsync(Transaction transaction, TransactionContext context)
    {
        if (transaction.LogEvents.Count == 0) return;

        var chainId = context.ChainId;
        var blockHeight = context.Block.BlockHeight;
        var blockHash = context.Block.BlockHash;
        _logger.LogDebug("Chain id: {chain}, BlockHeight: {height}, TransactionId: {transactionId}",
            chainId, blockHeight, transaction.TransactionId);

        foreach (var logEvent in transaction.LogEvents.Where(l => !EventWhitelist.Whitelist.Contains(l.EventName)))
        {
            await ProcessEventAsync(new TransactionEventIndex
            {
                ChainId = chainId,
                TransactionId = transaction.TransactionId,
                MethodName = transaction.MethodName,
                StartTime = new DateTimeOffset(context.Block.BlockTime).ToUnixTimeMilliseconds(),
                BlockHash = blockHash,
                BlockHeight = blockHeight
            }, logEvent);
        }
    }

    private async Task ProcessEventAsync(TransactionEventIndex info, LogEvent logEvent)
    {
        var contractAddress = logEvent.ContractAddress;
        var id = IdGenerateHelper.GetLogInfoId(info.ChainId, info.BlockHeight, info.TransactionId, info.MethodName,
            logEvent.EventName, contractAddress, logEvent.Index);
        info.Id = id;
        info.EventName = logEvent.EventName;
        info.ContractAddress = contractAddress;
        info.Index = logEvent.Index;
        _eventProvider.SaveTransactionEventId(info.ChainId, info.BlockHeight, id);
        await SaveEntityAsync(info);
    }
}