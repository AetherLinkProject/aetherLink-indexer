using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.Providers;

namespace AetherLink.Indexer.Processors;

public class BlockProcessor : BlockProcessorBase
{
    private readonly IAeFinderLogger _logger;
    private readonly ITransactionEventProvider _eventProvider;

    public BlockProcessor(IAeFinderLogger logger, ITransactionEventProvider eventProvider)
    {
        _logger = logger;
        _eventProvider = eventProvider;
    }

    public override async Task ProcessAsync(Block block, BlockContext context)
    {
        _logger.LogDebug($"Get Block at {block.BlockHeight}");

        var expiredBlockHeight = block.BlockHeight - 2 * 60 * 60 * 24;
        var eventList = _eventProvider.GetTransactionEventIds(context.ChainId, expiredBlockHeight);
        if (eventList.Count == 0) return;

        foreach (var logEvent in eventList)
        {
            var entity = await GetEntityAsync<TransactionEventIndex>(logEvent);
            if (entity == null)
            {
                _logger.LogDebug($"transaction event id {logEvent} not exist");
                continue;
            }

            await DeleteEntityAsync<TransactionEventIndex>(logEvent);
        }

        _logger.LogDebug($"Ready delete transaction event at {context.ChainId} {expiredBlockHeight}");
        _eventProvider.DeleteTransactionEventId(context.ChainId, expiredBlockHeight);
    }
}