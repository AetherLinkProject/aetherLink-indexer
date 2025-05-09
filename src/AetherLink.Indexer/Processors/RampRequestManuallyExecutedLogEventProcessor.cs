using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AElf;
using AetherLink.Contracts.Ramp;
using AetherLink.Indexer.Common;

namespace AetherLink.Indexer.Processors;

public class RampRequestManuallyExecutedLogEventProcessor : LogEventProcessorBase<RequestManuallyExecuted>
{
    private readonly IAeFinderLogger _logger;

    public RampRequestManuallyExecutedLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetRampContractAddress(chainId);

    public override async Task ProcessAsync(RequestManuallyExecuted logEvent, LogEventContext context)
    {
        var chainId = context.ChainId;
        var messageId = logEvent.MessageId.ToByteArray().ToHex();
        var blockHeight = context.Block.BlockHeight;
        _logger.LogDebug(
            $"[RequestManuallyExecuted] chainId:{chainId} messageId:{messageId} at {blockHeight}");

        var indexId = IdGenerateHelper.GetRampRequestManuallyExecuteId(chainId, messageId, blockHeight);
        if (await GetEntityAsync<RampRequestManuallyExecutedIndex>(indexId) != null) return;

        await SaveEntityAsync(new RampRequestManuallyExecutedIndex
        {
            Id = indexId,
            ChainId = chainId,
            MessageId = messageId,
            TransactionId = context.Transaction.TransactionId,
            StartTime = new DateTimeOffset(context.Block.BlockTime).ToUnixTimeMilliseconds(),
            BlockHeight = blockHeight
        });
    }
}