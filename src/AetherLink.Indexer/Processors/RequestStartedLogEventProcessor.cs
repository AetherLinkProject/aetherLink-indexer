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

public class RequestStartedLogEventProcessor : AElfLogEventProcessorBase<RequestStarted, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly ContractInfoOptions _contractInfoOptions;
    private readonly IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> _repository;
    private readonly IAElfIndexerClientEntityRepository<CommitmentIndex, LogEventInfo> _commitmentRepository;
    private readonly ILogger<RequestStartedLogEventProcessor> _processorLogger;

    public RequestStartedLogEventProcessor(ILogger<RequestStartedLogEventProcessor> logger,
        IOptions<ContractInfoOptions> contractInfoOptions, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> repository,
        IAElfIndexerClientEntityRepository<CommitmentIndex, LogEventInfo> commitmentRepository,
        ILogger<RequestStartedLogEventProcessor> processorLogger) : base(logger)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _processorLogger = processorLogger;
        _commitmentRepository = commitmentRepository;
        _contractInfoOptions = contractInfoOptions.Value;
    }

    public override string GetContractAddress(string chainId)
    {
        return _contractInfoOptions.ContractInfos.First(c => c.ChainId == chainId).AetherLinkOracleContractAddress;
    }

    protected override async Task HandleEventAsync(RequestStarted eventValue, LogEventContext context)
    {
        _processorLogger.LogDebug(
            "[RequestStartedLogEventProcessor] RequestStarted chainId:{chainId}, requestId:{reqId}",
            context.ChainId, eventValue.RequestId.ToHex());
        var indexId = IndexPrefixHelper.GetOcrIndexId(context.ChainId, eventValue.RequestId.ToHex());
        var ocrLogEventIndex = await _repository.GetFromBlockStateSetAsync(indexId, context.ChainId);
        if (ocrLogEventIndex != null) return;

        ocrLogEventIndex = new OcrJobEventIndex
        {
            Id = indexId,
            TransactionId = context.TransactionId,
            RequestId = eventValue.RequestId.ToHex(),
            RequestTypeIndex = eventValue.RequestTypeIndex,
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds()
        };
        _objectMapper.Map(context, ocrLogEventIndex);
        await _repository.AddOrUpdateAsync(ocrLogEventIndex);

        _processorLogger.LogDebug("[RequestStartedLogEventProcessor] CommitmentId chainId:{chainId}, requestId:{reqId}",
            context.ChainId, eventValue.RequestId.ToHex());
        var commitmentId = IndexPrefixHelper.GetCommitmentIndexId(context.ChainId, eventValue.RequestId.ToHex());
        var commitmentIndex = await _commitmentRepository.GetFromBlockStateSetAsync(commitmentId, context.ChainId);
        if (commitmentIndex != null) return;

        commitmentIndex = new CommitmentIndex
        {
            Id = commitmentId,
            RequestId = eventValue.RequestId.ToHex(),
            Commitment = eventValue.Commitment.ToBase64()
        };
        _objectMapper.Map(context, commitmentIndex);
        await _commitmentRepository.AddOrUpdateAsync(commitmentIndex);
    }
}