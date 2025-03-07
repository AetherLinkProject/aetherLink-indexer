using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using AetherLink.Contracts.Ramp;
using AetherLink.Indexer.Common;
using AetherLink.Indexer.Entities;

namespace AetherLink.Indexer.Processors;

public class RampCommitReportAcceptedLogEventProcessor : LogEventProcessorBase<CommitReportAccepted>
{
    private readonly IAeFinderLogger _logger;

    public RampCommitReportAcceptedLogEventProcessor(IAeFinderLogger logger)
    {
        _logger = logger;
    }

    public override string GetContractAddress(string chainId) => ContractAddressHelper.GetRampContractAddress(chainId);

    public override async Task ProcessAsync(CommitReportAccepted logEvent, LogEventContext context)
    {
        var chainId = context.ChainId;
        var sourceChainId = logEvent.Report.ReportContext.SourceChainId;
        var messageId = logEvent.Report.ReportContext.MessageId.ToHex();

        _logger.LogDebug(
            $"[CommitReportAccepted] chainId:{chainId} sourceChainId:{sourceChainId} messageId:{messageId}");

        var indexId = IdGenerateHelper.GetRampCommitReportAcceptedId(chainId, messageId);
        if (await GetEntityAsync<RampCommitReportAcceptedIndex>(indexId) != null) return;

        await SaveEntityAsync(new RampCommitReportAcceptedIndex
        {
            Id = indexId,
            ChainId = chainId,
            SourceChainId = logEvent.Report.ReportContext.SourceChainId,
            TargetChainId = logEvent.Report.ReportContext.TargetChainId,
            MessageId = messageId,
            BlockHeight = context.Block.BlockHeight,
            TransactionId = context.Transaction.TransactionId
        });
    }
}