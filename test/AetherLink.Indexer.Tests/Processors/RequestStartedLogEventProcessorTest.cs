using AeFinder.Sdk;
using AeFinder.Sdk.Processor;
using AElf;
using AElf.Types;
using AetherLink.Contracts.Oracle;
using aetherLink.indexer;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using Google.Protobuf;
using Nethereum.Hex.HexConvertors.Extensions;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Processors;

public sealed class RequestStartedLogEventProcessorTest : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly RequestStartedLogEventProcessor _processor;
    private readonly IReadOnlyRepository<OcrJobEventIndex> _repository;

    public RequestStartedLogEventProcessorTest()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<RequestStartedLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<OcrJobEventIndex>>();
    }

    [Fact]
    public async Task Query_Request_Start_LogEvent_Test()
    {
        var ctx1 = await MockRequestStarted();
        var result = await Query.OcrJobEventsQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result.Count.ShouldBe(1);
        result.First().ChainId.ShouldBe("AELF");
        result.First().Commitment.ShouldBe(HashHelper.ComputeFrom("Commitment").ToByteString().ToBase64());
        result.First().RequestId.ShouldBe(HashHelper.ComputeFrom("test_request_id").ToHex());
        result.First().RequestTypeIndex.ShouldBe(1);
        result.First().BlockHeight.ShouldBe(ctx1.Block.BlockHeight);
        result.First().BlockHash.ShouldBe(ctx1.Block.BlockHash);

        var ctx2 = await MockRequestStarted(requestId: "test_request_id_2");
        var result2 = await Query.OcrJobEventsQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result2.Count.ShouldBe(2);
        result2[1].ChainId.ShouldBe("AELF");
        result2[1].Commitment.ShouldBe(HashHelper.ComputeFrom("Commitment").ToByteString().ToBase64());
        result2[1].RequestId.ShouldBe(HashHelper.ComputeFrom("test_request_id_2").ToHex());
        result2[1].RequestTypeIndex.ShouldBe(1);
        result2[1].BlockHeight.ShouldBe(ctx2.Block.BlockHeight);
        result2[1].BlockHash.ShouldBe(ctx2.Block.BlockHash);
    }

    private async Task<LogEventContext> MockRequestStarted(string requestId = "test_request_id")
    {
        var logEvent = new RequestStarted
        {
            RequestId = HashHelper.ComputeFrom(requestId),
            RequestingContract = Address.FromPublicKey("AAA".HexToByteArray()),
            RequestingInitiator = Address.FromPublicKey("BBB".HexToByteArray()),
            SubscriptionId = 1,
            SubscriptionOwner = Address.FromPublicKey("CCC".HexToByteArray()),
            Commitment = HashHelper.ComputeFrom("Commitment").ToByteString(),
            RequestTypeIndex = 1
        };

        var context = GenerateLogEventContext(logEvent);
        await _processor.ProcessAsync(logEvent, context);
        return context;
    }
}