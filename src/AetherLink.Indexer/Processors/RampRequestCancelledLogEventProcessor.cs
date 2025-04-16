using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AElf;
using AetherLink.Contracts.Ramp;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;

namespace AetherLink.Indexer.Processors;

public class RampRequestCancelledLogEventProcessor : LogEventProcessorBase<RequestCancelled>
{
    private readonly IAeFinderLogger _logger;

    public RampRequestCancelledLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetRampContractAddress(chainId);

    public override async Task ProcessAsync(RequestCancelled logEvent, LogEventContext context)
    {
        var chainId = context.ChainId;
        var messageId = logEvent.MessageId.ToByteArray().ToHex();

        _logger.LogDebug($"[RampRequestCancelled] chainId:{chainId} messageId:{messageId}");

        var indexId = IdGenerateHelper.GetRampRequestCancelId(chainId, messageId);
        if (await GetEntityAsync<RampRequestCancelledIndex>(indexId) != null) return;

        await SaveEntityAsync(new RampRequestCancelledIndex
        {
            Id = indexId,
            ChainId = chainId,
            MessageId = messageId,
            BlockHeight = context.Block.BlockHeight,
        });
    }
}