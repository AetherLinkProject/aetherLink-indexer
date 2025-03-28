using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;

namespace AetherLink.Indexer.Processors;

public class TransmittedLogEventProcessor : LogEventProcessorBase<Transmitted>
{
    private readonly IAeFinderLogger _logger;

    public TransmittedLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetContractAddress(chainId);

    public override async Task ProcessAsync(Transmitted logEvent, LogEventContext context)
    {
        var chainId = context.ChainId;
        var requestId = logEvent.RequestId.ToHex();

        _logger.LogDebug("[Transmitted] chainId:{chainId}, requestId:{reqId}", chainId, requestId);

        var indexId = IdGenerateHelper.GetTransmittedId(chainId, requestId, logEvent.ConfigDigest.ToHex(),
            logEvent.EpochAndRound);
        if (await GetEntityAsync<TransmittedIndex>(indexId) != null) return;

        await SaveEntityAsync(new TransmittedIndex
        {
            Id = indexId,
            ChainId = context.ChainId,
            TransactionId = context.Transaction.TransactionId,
            BlockHeight = context.Block.BlockHeight,
            RequestId = logEvent.RequestId.ToHex(),
            StartTime = new DateTimeOffset(context.Block.BlockTime).ToUnixTimeMilliseconds(),
            Epoch = logEvent.EpochAndRound
        });
    }
}