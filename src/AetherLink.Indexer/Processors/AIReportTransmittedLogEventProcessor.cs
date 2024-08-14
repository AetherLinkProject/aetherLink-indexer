using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.AIFeeds;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.ObjectMapping;

namespace AetherLink.Indexer.Processors;

public class AIReportTransmittedLogEventProcessor : AElfLogEventProcessorBase<AIReportTransmitted, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly List<ContractInfo> _contractInfoOptions;
    private readonly ILogger<AIReportTransmittedLogEventProcessor> _logger;
    private readonly IAElfIndexerClientEntityRepository<AIReportTransmittedIndex, LogEventInfo> _repository;

    public AIReportTransmittedLogEventProcessor(IOptions<ContractInfoOptions> contractInfoOptions,
        IAElfIndexerClientEntityRepository<AIReportTransmittedIndex, LogEventInfo> repository,
        ILogger<AIReportTransmittedLogEventProcessor> logger, IObjectMapper objectMapper) : base(logger)
    {
        _logger = logger;
        _repository = repository;
        _objectMapper = objectMapper;
        _contractInfoOptions = contractInfoOptions.Value.ContractInfos;
    }

    public override string GetContractAddress(string chainId)
        => _contractInfoOptions.First(c => c.ChainId == chainId).AetherLinkAIOracleContractAddress;

    protected override async Task HandleEventAsync(AIReportTransmitted eventValue, LogEventContext context)
    {
        _logger.LogDebug("[AI Report Transmitted] Transmitted chainId:{chainId}, requestId:{reqId}",
            context.ChainId, eventValue.RequestId.ToHex());
        var indexId = IdGenerateHelper.GetId(IdGenerateHelper.AIReportTransmittedPrefix, context.ChainId,
            eventValue.RequestId.ToHex());
        var entity = await _repository.GetFromBlockStateSetAsync(indexId, context.ChainId);
        if (entity != null) return;

        entity = new AIReportTransmittedIndex
        {
            Id = indexId,
            RequestId = eventValue.RequestId.ToHex(),
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),
        };
        _objectMapper.Map(context, entity);
        await _repository.AddOrUpdateAsync(entity);
    }
}