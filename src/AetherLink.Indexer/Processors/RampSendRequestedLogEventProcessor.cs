using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AElf;
using AetherLink.Contracts.Ramp;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;

namespace AetherLink.Indexer.Processors;

public class RampSendRequestedLogEventProcessor : LogEventProcessorBase<SendRequested>
{
    private readonly IAeFinderLogger _logger;

    public RampSendRequestedLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetRampContractAddress(chainId);

    public override async Task ProcessAsync(SendRequested logEvent, LogEventContext context)
    {
        var chainId = context.ChainId;
        var messageId = logEvent.MessageId.ToHex();

        _logger.LogDebug("[Ramp Request] chainId:{chainId}, messageId:{reqId}, blockHeight:{height}", chainId,
            messageId, context.Block.BlockHeight);

        var indexId = IdGenerateHelper.GetOcrId(chainId, messageId);
        if (await GetEntityAsync<RampSendRequestedIndex>(indexId) != null) return;
        await SaveEntityAsync(new RampSendRequestedIndex
        {
            Id = indexId,
            MessageId = messageId,
            TargetChainId = logEvent.TargetChainId,
            SourceChainId = ChainHelper.ConvertBase58ToChainId(context.ChainId),
            Sender = logEvent.Sender.ToBase64(),
            Receiver = logEvent.Receiver.ToBase64(),
            Data = logEvent.Data.ToBase64()
        });
    }
}