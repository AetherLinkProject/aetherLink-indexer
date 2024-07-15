using AElf;
using AElf.CSharp.Core.Extension;
using AElfIndexer.Client;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using AetherLink.Indexer.Processors;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Tests.Processors;

public sealed class RequestCanceledLogEventProcessorTest : AetherLinkIndexerDappTests
{
    private readonly IAElfIndexerClientEntityRepository<RequestCancelledIndex, LogEventInfo> _repository;
    private readonly IObjectMapper _objectMapper;

    public RequestCanceledLogEventProcessorTest()
    {
        _repository = GetRequiredService<IAElfIndexerClientEntityRepository<RequestCancelledIndex, LogEventInfo>>();
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Query_Test()
    {
        await MockRequestCanceled();
        var result = await Query.RequestCancelledQueryAsync(_repository, _objectMapper,
            new RequestCancelledInput
            {
                FromBlockHeight = 10,
                ToBlockHeight = 200,
            });
        result.Count.ShouldBe(1);
    }

    private async Task MockRequestCanceled(string chainId = "TEST", long blockHeight = 20,
        string requestId = "test_request_id")
    {
        var logEventContext = MockLogEventContext(blockHeight);
        var blockStateSetKey = await MockBlockState(logEventContext);

        var requestStarted = new RequestCancelled
        {
            RequestId = HashHelper.ComputeFrom(requestId)
        };

        var logEventInfo = MockLogEventInfo(requestStarted.ToLogEvent());
        var requestCanceledLogEventProcessor = GetRequiredService<RequestCancelledLogEventProcessor>();
        await requestCanceledLogEventProcessor.HandleEventAsync(logEventInfo, logEventContext);
        requestCanceledLogEventProcessor.GetContractAddress(chainId);
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);
    }
}