using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.ObjectMapping;

namespace AetherLink.Indexer.Processors;

public class RequestCancelledLogEventProcessor : AElfLogEventProcessorBase<RequestCancelled, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly ContractInfoOptions _contractInfoOptions;
    private readonly IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> _repository;
    private readonly ILogger<RequestCancelledLogEventProcessor> _processorLogger;


    public RequestCancelledLogEventProcessor(ILogger<RequestCancelledLogEventProcessor> logger,
        IOptions<ContractInfoOptions> contractInfoOptions, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> repository,
        ILogger<RequestCancelledLogEventProcessor> processorLogger) : base(logger)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _processorLogger = processorLogger;
        _contractInfoOptions = contractInfoOptions.Value;
    }

    public override string GetContractAddress(string chainId)
    {
        return _contractInfoOptions.ContractInfos.First(c => c.ChainId == chainId).AetherLinkOracleContractAddress;
    }

    protected override async Task HandleEventAsync(RequestCancelled eventValue, LogEventContext context)
    {
        _processorLogger.LogDebug(
            "[RequestCancelledLogEventProcessor] RequestCancelled chainId:{chainId}, requestId:{reqId}",
            context.ChainId, eventValue.RequestId.ToHex());
        var indexId = IndexPrefixHelper.GetRequestCancelIndexId(context.ChainId, eventValue.RequestId.ToHex());
        var ocrLogEventIndex = await _repository.GetFromBlockStateSetAsync(indexId, context.ChainId);
        if (ocrLogEventIndex != null) return;

        ocrLogEventIndex = new OcrJobEventIndex
        {
            Id = indexId,
            TransactionId = context.TransactionId,
            RequestId = eventValue.RequestId.ToHex(),
            RequestTypeIndex = -2,
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds()
        };
        _objectMapper.Map(context, ocrLogEventIndex);
        await _repository.AddOrUpdateAsync(ocrLogEventIndex);
    }
}