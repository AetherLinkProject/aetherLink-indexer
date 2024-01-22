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

public class TransmittedLogEventProcessor : AElfLogEventProcessorBase<Transmitted, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly ContractInfoOptions _contractInfoOptions;
    private readonly IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> _repository;
    private readonly IAElfIndexerClientEntityRepository<LatestRoundIndex, LogEventInfo> _latestRoundRepository;
    private readonly ILogger<TransmittedLogEventProcessor> _processorLogger;

    public TransmittedLogEventProcessor(ILogger<TransmittedLogEventProcessor> logger, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> repository,
        IOptions<ContractInfoOptions> contractInfoOptions,
        IAElfIndexerClientEntityRepository<LatestRoundIndex, LogEventInfo> latestRoundRepository,
        ILogger<TransmittedLogEventProcessor> processorLogger) : base(logger)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _processorLogger = processorLogger;
        _latestRoundRepository = latestRoundRepository;
        _contractInfoOptions = contractInfoOptions.Value;
    }

    public override string GetContractAddress(string chainId)
    {
        return _contractInfoOptions.ContractInfos.First(c => c.ChainId == chainId).AetherLinkOracleContractAddress;
    }

    protected override async Task HandleEventAsync(Transmitted eventValue, LogEventContext context)
    {
        _processorLogger.LogDebug("[TransmittedLogEventProcessor] Transmitted chainId:{chainId}, requestId:{reqId}",
            context.ChainId, eventValue.RequestId.ToHex());
        var indexId = IndexPrefixHelper.GetTransmittedIndexId(context.ChainId, eventValue.RequestId.ToHex(),
            eventValue.ConfigDigest.ToHex(), eventValue.EpochAndRound);
        var ocrLogEventIndex = await _repository.GetFromBlockStateSetAsync(indexId, context.ChainId);
        if (ocrLogEventIndex != null) return;

        ocrLogEventIndex = new OcrJobEventIndex
        {
            Id = indexId,
            RequestId = eventValue.RequestId.ToHex(),
            TransactionId = context.TransactionId,
            RequestTypeIndex = -1,
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),
            Epoch = eventValue.EpochAndRound
        };
        _objectMapper.Map(context, ocrLogEventIndex);
        await _repository.AddOrUpdateAsync(ocrLogEventIndex);

        _processorLogger.LogDebug("[TransmittedLogEventProcessor] EpochAndRound chainId:{chainId}, address:{addr}",
            context.ChainId, context.From);
        var latestRoundIndex = new LatestRoundIndex
        {
            Id = IndexPrefixHelper.GetLatestRoundIndexId(context.ChainId, context.From),
            EpochAndRound = eventValue.EpochAndRound
        };
        _objectMapper.Map(context, latestRoundIndex);
        await _latestRoundRepository.AddOrUpdateAsync(latestRoundIndex);
    }
}