using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.ObjectMapping;

namespace AetherLink.Indexer.Processors;

public class TransmittedLogEventProcessor : AElfLogEventProcessorBase<Transmitted, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly ContractInfoOptions _contractInfoOptions;
    private readonly ILogger<TransmittedLogEventProcessor> _logger;
    private readonly IAElfIndexerClientEntityRepository<TransmittedIndex, LogEventInfo> _repository;

    public TransmittedLogEventProcessor(IAElfIndexerClientEntityRepository<TransmittedIndex, LogEventInfo> repository,
        ILogger<TransmittedLogEventProcessor> logger, IOptions<ContractInfoOptions> contractInfoOptions,
        IObjectMapper objectMapper) : base(logger)
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

    protected override async Task HandleEventAsync(Transmitted eventValue, LogEventContext context)
    {
        _logger.LogDebug("[Transmitted] Transmitted chainId:{chainId}, requestId:{reqId}",
            context.ChainId, eventValue.RequestId.ToHex());
        var indexId = IdGenerateHelper.GetTransmittedIndexId(context.ChainId, eventValue.RequestId.ToHex(),
            eventValue.ConfigDigest.ToHex(), eventValue.EpochAndRound);
        var transmittedIndex = await _repository.GetFromBlockStateSetAsync(indexId, context.ChainId);
        if (transmittedIndex != null) return;

        transmittedIndex = new TransmittedIndex
        {
            Id = indexId,
            RequestId = eventValue.RequestId.ToHex(),
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),
            Epoch = eventValue.EpochAndRound
        };
        _objectMapper.Map(context, transmittedIndex);
        await _repository.AddOrUpdateAsync(transmittedIndex);
    }
}