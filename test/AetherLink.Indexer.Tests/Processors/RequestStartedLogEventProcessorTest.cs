using AElf;
using AElf.CSharp.Core.Extension;
using AElf.Types;
using AElfIndexer.Client;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using AetherLink.Indexer.Processors;
using Google.Protobuf;
using Nethereum.Hex.HexConvertors.Extensions;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Tests.Processors;

public sealed class RequestStartedLogEventProcessorTest : AetherLinkIndexerDappTests
{
    private readonly IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> _repository;
    private readonly IObjectMapper _objectMapper;

    public RequestStartedLogEventProcessorTest()
    {
        _repository = GetRequiredService<IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo>>();
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Query_Request_Start_LogEvent_Test()
    {
        await MockRequestStarted();
        var result = await Query.OcrJobEventsQueryAsync(_repository, _objectMapper,
            new OcrLogEventInput
            {
                FromBlockHeight = 10,
                ToBlockHeight = 200,
            });
        result.Count.ShouldBe(1);

        await MockRequestStarted(blockHeight: 120, requestId: "test_request_120");
        var result2 = await Query.OcrJobEventsQueryAsync(_repository, _objectMapper,
            new OcrLogEventInput
            {
                FromBlockHeight = 10,
                ToBlockHeight = 200,
            });
        result2.Count.ShouldBe(2);
    }

    private async Task MockRequestStarted(string chainId = "TEST", long blockHeight = 20,
        string requestId = "test_request_id", string requestingContract = "AAA", string requestingInitiator = "BBB",
        string subscriptionOwner = "CCC", string commitment = "Commitment", long subscriptionId = 1,
        int requestTypeIndex = 1)
    {
        var logEventContext = MockLogEventContext(blockHeight);
        var blockStateSetKey = await MockBlockState(logEventContext);

        var requestStarted = new RequestStarted
        {
            RequestId = HashHelper.ComputeFrom(requestId),
            RequestingContract = Address.FromPublicKey(requestingContract.HexToByteArray()),
            RequestingInitiator = Address.FromPublicKey(requestingInitiator.HexToByteArray()),
            SubscriptionId = subscriptionId,
            SubscriptionOwner = Address.FromPublicKey(subscriptionOwner.HexToByteArray()),
            Commitment = HashHelper.ComputeFrom(commitment).ToByteString(),
            RequestTypeIndex = requestTypeIndex
        };

        var logEventInfo = MockLogEventInfo(requestStarted.ToLogEvent());
        var requestStartedLogEventProcessor = GetRequiredService<RequestStartedLogEventProcessor>();
        await requestStartedLogEventProcessor.HandleEventAsync(logEventInfo, logEventContext);
        requestStartedLogEventProcessor.GetContractAddress(chainId);
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);
    }
}