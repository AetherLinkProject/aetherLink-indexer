using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;

namespace AetherLink.Indexer.Processors;

public class ConfigSetLogEventProcessor : LogEventProcessorBase<ConfigSet>
{
    private readonly IAeFinderLogger _logger;

    public ConfigSetLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetContractAddress(chainId);

    public override async Task ProcessAsync(ConfigSet logEvent, LogEventContext context)
    {
        _logger.LogDebug("[ConfigSet] ConfigSet chainId:{chainId}", context.ChainId);

        await SaveEntityAsync(new ConfigDigestIndex
        {
            Id = IdGenerateHelper.GetConfigSetId(context.ChainId),
            ChainId = context.ChainId,
            ConfigDigest = logEvent.ConfigDigest.ToHex(),
        });
    }
}