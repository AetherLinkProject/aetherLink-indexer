using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.ObjectMapping;

namespace AetherLink.Indexer.Processors;

public class RequestStartedLogEventProcessor : AElfLogEventProcessorBase<RequestStarted, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly ContractInfoOptions _contractInfoOptions;
    private readonly ILogger<RequestStartedLogEventProcessor> _logger;
    private readonly IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> _repository;

    public RequestStartedLogEventProcessor(ILogger<RequestStartedLogEventProcessor> logger,
        IOptions<ContractInfoOptions> contractInfoOptions, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> repository) : base(logger)
    {
        _logger = logger;
        _repository = repository;
        _objectMapper = objectMapper;
        _contractInfoOptions = contractInfoOptions.Value;
    }

    public override string GetContractAddress(string chainId)
    {
        return _contractInfoOptions.ContractInfos.First(c => c.ChainId == chainId).AetherLinkOracleContractAddress;
    }

    protected override async Task HandleEventAsync(RequestStarted eventValue, LogEventContext context)
    {
        _logger.LogDebug("[RequestStarted] RequestStarted chainId:{chainId}, requestId:{reqId}, blockHeight:{height}",
            context.ChainId, eventValue.RequestId.ToHex(), context.BlockHeight);
        var indexId = IdGenerateHelper.GetOcrIndexId(context.ChainId, eventValue.RequestId.ToHex());
        var ocrLogEventIndex = await _repository.GetFromBlockStateSetAsync(indexId, context.ChainId);
        if (ocrLogEventIndex != null) return;

        ocrLogEventIndex = new OcrJobEventIndex
        {
            Id = indexId,
            TransactionId = context.TransactionId,
            RequestId = eventValue.RequestId.ToHex(),
            RequestTypeIndex = eventValue.RequestTypeIndex,
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),
            Commitment = eventValue.Commitment.ToBase64()
        };
        _objectMapper.Map(context, ocrLogEventIndex);
        await _repository.AddOrUpdateAsync(ocrLogEventIndex);
    }
}