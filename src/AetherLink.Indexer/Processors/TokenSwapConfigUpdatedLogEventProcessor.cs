using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AElf;
using AetherLink.Contracts.Ramp;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;

namespace AetherLink.Indexer.Processors;

public class TokenSwapConfigUpdatedLogEventProcessor : LogEventProcessorBase<TokenSwapConfigUpdated>
{
    private readonly IAeFinderLogger _logger;

    public TokenSwapConfigUpdatedLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetRampContractAddress(chainId);

    public override async Task ProcessAsync(TokenSwapConfigUpdated logEvent, LogEventContext context)
    {
        if (context.ChainId.ToLower() != "aelf")
        {
            _logger.LogWarning("[TokenSwapConfigUpdated] Only accept main chain token swap config settings.");
            return;
        }

        var contractAddress = logEvent.ContractAddress.ToBase58();
        var configListId = IdGenerateHelper.GetTokenSwapConfigContractId(contractAddress);
        var configListEntity = await GetEntityAsync<TokenSwapConfigListIndex>(configListId);
        if (configListEntity != null)
        {
            var configDeletePendingTasks =
                configListEntity.TokenSwapConfigIdList.Select(DeleteEntityAsync<TokenSwapConfigInfoIndex>);
            await Task.WhenAll(configDeletePendingTasks);
        }

        var tokenSwapConfigIdList = new List<string>();
        var configCreatePendingTasks = new List<Task>();
        foreach (var tokenSwapInfo in logEvent.TokenSwapList.TokenSwapInfoList)
        {
            var configId = HashHelper.ComputeFrom(tokenSwapInfo).ToHex();
            tokenSwapConfigIdList.Add(configId);
            configCreatePendingTasks.Add(SaveEntityAsync(new TokenSwapConfigInfoIndex
            {
                Id = configId,
                SwapId = tokenSwapInfo.SwapId,
                TargetChainId = tokenSwapInfo.TargetChainId,
                TargetContractAddress = tokenSwapInfo.TargetContractAddress,
                TokenAddress = tokenSwapInfo.TokenAddress,
                OriginToken = tokenSwapInfo.OriginToken
            }));
        }

        await Task.WhenAll(configCreatePendingTasks);
        _logger.LogInformation(
            $"[TokenSwapConfigUpdated] Ready to create token swap config: {string.Join(",", tokenSwapConfigIdList)} in {configListId}");
        await SaveEntityAsync(new TokenSwapConfigListIndex
        {
            Id = configListId,
            ContractAddress = contractAddress,
            TokenSwapConfigIdList = tokenSwapConfigIdList
        });
    }
}