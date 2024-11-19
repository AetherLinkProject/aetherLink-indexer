using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AElf;
using AetherLink.Contracts.Ramp;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;
using Google.Protobuf;

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
        var messageId = ByteString.CopyFrom(logEvent.MessageId.ToByteArray()).ToBase64();

        _logger.LogDebug("[Ramp Request] chainId:{chainId}, messageId:{reqId}, blockHeight:{height}",
            chainId, messageId, context.Block.BlockHeight);

        var indexId = IdGenerateHelper.GetOcrId(chainId, messageId);
        if (await GetEntityAsync<RampSendRequestedIndex>(indexId) != null) return;
        await SaveEntityAsync(new RampSendRequestedIndex
        {
            Id = indexId,
            ChainId = context.ChainId,
            TransactionId = context.Transaction.TransactionId,
            BlockHeight = context.Block.BlockHeight,
            MessageId = messageId,
            TargetChainId = logEvent.TargetChainId,
            SourceChainId = ChainHelper.ConvertBase58ToChainId(context.ChainId),
            Sender = logEvent.Sender.ToBase64(),
            Receiver = logEvent.Receiver.ToBase64(),
            Message = logEvent.Message.ToBase64(),
            TokenAmount = new()
            {
                TargetChainId = logEvent.TokenAmount.TargetChainId,
                TargetContractAddress = logEvent.TokenAmount.TargetContractAddress,
                OriginToken = logEvent.TokenAmount.OriginToken
            },
            Epoch = logEvent.Epoch,
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds()
        });
    }
}