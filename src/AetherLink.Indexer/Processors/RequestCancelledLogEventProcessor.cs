using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;

namespace AetherLink.Indexer.Processors;

public class RequestCancelledLogEventProcessor : LogEventProcessorBase<RequestCancelled>
{
    private readonly IAeFinderLogger _logger;

    public RequestCancelledLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetContractAddress(chainId);

    public override async Task ProcessAsync(RequestCancelled logEvent, LogEventContext context)
    {
        var chainId = context.ChainId;
        var requestId = logEvent.RequestId.ToHex();

        _logger.LogDebug("[RequestCancelled] chainId:{chainId}, requestId:{reqId}", chainId, requestId);

        var indexId = IdGenerateHelper.GetRequestCancelId(chainId, requestId);
        if (await GetEntityAsync<RequestCancelledIndex>(indexId) != null) return;

        await SaveEntityAsync(new RequestCancelledIndex
        {
            Id = indexId,
            ChainId = context.ChainId,
            RequestId = requestId,
            BlockHeight = context.Block.BlockHeight,
        });
    }
}