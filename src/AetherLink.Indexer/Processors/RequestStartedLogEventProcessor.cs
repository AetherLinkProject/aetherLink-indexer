using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;

namespace AetherLink.Indexer.Processors;

public class RequestStartedLogEventProcessor : LogEventProcessorBase<RequestStarted>
{
    private readonly IAeFinderLogger _logger;

    public RequestStartedLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetContractAddress(chainId);

    public override async Task ProcessAsync(RequestStarted logEvent, LogEventContext context)
    {
        var chainId = context.ChainId;
        var requestId = logEvent.RequestId.ToHex();

        _logger.LogDebug("[RequestStarted] chainId:{chainId}, requestId:{reqId}, blockHeight:{height}", chainId,
            requestId, context.Block.BlockHeight);

        var indexId = IdGenerateHelper.GetOcrId(chainId, requestId);
        if (await GetEntityAsync<OcrJobEventIndex>(indexId) != null) return;

        await SaveEntityAsync(new OcrJobEventIndex
        {
            Id = indexId,
            ChainId = context.ChainId,
            BlockHeight = context.Block.BlockHeight,
            BlockHash = context.Block.BlockHash,
            RequestId = requestId,
            RequestTypeIndex = logEvent.RequestTypeIndex,
            TransactionId = context.Transaction.TransactionId,
            StartTime = new DateTimeOffset(context.Block.BlockTime).ToUnixTimeMilliseconds(),
            Commitment = logEvent.Commitment.ToBase64()
        });
    }
}