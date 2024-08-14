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

public class AIRequestStartedLogEventProcessor : AElfLogEventProcessorBase<RequestStarted, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly List<ContractInfo> _contractInfoOptions;
    private readonly ILogger<AIRequestStartedLogEventProcessor> _logger;
    private readonly IAElfIndexerClientEntityRepository<AIRequestIndex, LogEventInfo> _repository;

    public AIRequestStartedLogEventProcessor(IObjectMapper objectMapper,
        IOptionsSnapshot<ContractInfoOptions> contractInfoOptions,
        IAElfIndexerClientEntityRepository<AIRequestIndex, LogEventInfo> repository,
        ILogger<AIRequestStartedLogEventProcessor> logger) : base(logger)
    {
        _logger = logger;
        _repository = repository;
        _objectMapper = objectMapper;
        _contractInfoOptions = contractInfoOptions.Value.ContractInfos;
    }

    public override string GetContractAddress(string chainId)
        => _contractInfoOptions.First(c => c.ChainId == chainId).AetherLinkAIOracleContractAddress;

    protected override async Task HandleEventAsync(RequestStarted eventValue, LogEventContext context)
    {
        _logger.LogDebug("[AI Oracle] RequestStarted chainId:{chainId}, requestId:{reqId}, blockHeight:{height}",
            context.ChainId, eventValue.RequestId.ToHex(), context.BlockHeight);
        var indexId = IdGenerateHelper.GetId(IdGenerateHelper.AIPrefix, context.ChainId, eventValue.RequestId.ToHex());
        var entity = await _repository.GetFromBlockStateSetAsync(indexId, context.ChainId);
        if (entity != null) return;

        entity = new AIRequestIndex
        {
            Id = indexId,
            TransactionId = context.TransactionId,
            RequestId = eventValue.RequestId.ToHex(),
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),
            Commitment = eventValue.Commitment.ToBase64()
        };
        _objectMapper.Map(context, entity);
        await _repository.AddOrUpdateAsync(entity);
    }
}