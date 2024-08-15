using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.ObjectMapping;

namespace AetherLink.Indexer.Processors;

public class RequestCancelledLogEventProcessor : AElfLogEventProcessorBase<RequestCancelled, LogEventInfo>
{
    private readonly IObjectMapper _objectMapper;
    private readonly ContractInfoOptions _contractInfoOptions;
    private readonly ILogger<RequestCancelledLogEventProcessor> _logger;
    private readonly IAElfIndexerClientEntityRepository<RequestCancelledIndex, LogEventInfo> _repository;

    public RequestCancelledLogEventProcessor(ILogger<RequestCancelledLogEventProcessor> logger,
        IOptions<ContractInfoOptions> contractInfoOptions, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<RequestCancelledIndex, LogEventInfo> repository) : base(logger)
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

    protected override async Task HandleEventAsync(RequestCancelled eventValue, LogEventContext context)
    {
        _logger.LogDebug("[RequestCancelled] RequestCancelled chainId:{chainId}, requestId:{reqId}", context.ChainId,
            eventValue.RequestId.ToHex());
        var indexId = IdGenerateHelper.GetId(IdGenerateHelper.RequestCancelPrefix, context.ChainId,
            eventValue.RequestId.ToHex());
        var requestCancelledIndex = await _repository.GetFromBlockStateSetAsync(indexId, context.ChainId);
        if (requestCancelledIndex != null) return;

        requestCancelledIndex = new RequestCancelledIndex
        {
            Id = indexId,
            RequestId = eventValue.RequestId.ToHex(),
            StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds()
        };
        _objectMapper.Map(context, requestCancelledIndex);
        await _repository.AddOrUpdateAsync(requestCancelledIndex);
    }
}