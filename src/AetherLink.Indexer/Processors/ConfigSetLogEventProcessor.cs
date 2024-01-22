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

public class ConfigSetLogEventProcessor : AElfLogEventProcessorBase<ConfigSet, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly IAElfIndexerClientEntityRepository<ConfigDigestIndex, LogEventInfo> _repository;
    private readonly ContractInfoOptions _contractInfoOptions;
    private readonly ILogger<TransmittedLogEventProcessor> _processorLogger;

    public ConfigSetLogEventProcessor(ILogger<ConfigSetLogEventProcessor> logger, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<ConfigDigestIndex, LogEventInfo> repository,
        IOptions<ContractInfoOptions> contractInfoOptions,
        ILogger<TransmittedLogEventProcessor> processorLogger) : base(logger)
    {
        _objectMapper = objectMapper;
        _repository = repository;
        _processorLogger = processorLogger;
        _contractInfoOptions = contractInfoOptions.Value;
    }

    public override string GetContractAddress(string chainId)
    {
        return _contractInfoOptions.ContractInfos.First(c => c.ChainId == chainId).AetherLinkOracleContractAddress;
    }

    protected override async Task HandleEventAsync(ConfigSet eventValue, LogEventContext context)
    {
        _processorLogger.LogDebug("[ConfigSetLogEventProcessor] ConfigSet chainId:{chainId}", context.ChainId);
        var configDigestIndex = new ConfigDigestIndex
        {
            Id = IndexPrefixHelper.GetConfigSetIndexId(context.ChainId),
            ConfigDigest = eventValue.ConfigDigest.ToHex(),
        };

        _objectMapper.Map(context, configDigestIndex);
        await _repository.AddOrUpdateAsync(configDigestIndex);
    }
}