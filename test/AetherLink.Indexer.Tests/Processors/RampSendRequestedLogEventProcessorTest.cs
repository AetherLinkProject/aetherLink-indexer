using AeFinder.Sdk;
using AeFinder.Sdk.Processor;
using AElf;
using AElf.Types;
using AetherLink.Contracts.Ramp;
using aetherLink.indexer;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using Google.Protobuf;
using Nethereum.Hex.HexConvertors.Extensions;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Processors;

public class RampSendRequestedLogEventProcessorTest : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly RampSendRequestedLogEventProcessor _processor;
    private readonly IReadOnlyRepository<RampSendRequestedIndex> _repository;

    public RampSendRequestedLogEventProcessorTest()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<RampSendRequestedLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<RampSendRequestedIndex>>();
    }

    [Fact]
    public async Task Query_Request_Start_LogEvent_Test()
    {
        var ctx1 = await MockSendRequested();
        var result = await Query.RampRequestQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result.Count.ShouldBe(1);
        result.First().ChainId.ShouldBe("AELF");
        result.First().MessageId.ShouldBe(HashHelper.ComputeFrom("test_message_id").ToHex());
        result.First().Sender.ShouldBe(Address.FromPublicKey("BBB".HexToByteArray()).ToByteString().ToBase64());
        result.First().Receiver.ShouldBe(Address.FromPublicKey("AAA".HexToByteArray()).ToByteString().ToBase64());
        result.First().TargetChainId.ShouldBe(13);
        result.First().Data.ShouldBe(HashHelper.ComputeFrom("Message Data").ToByteString().ToBase64());
        result.First().BlockHeight.ShouldBe(ctx1.Block.BlockHeight);
        result.First().Epoch.ShouldBe(0);

        var ctx2 = await MockSendRequested("test_message_id_2", 1);
        var result2 = await Query.RampRequestQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result2.Count.ShouldBe(2);
        result2[1].ChainId.ShouldBe("AELF");
        result2[1].MessageId.ShouldBe(HashHelper.ComputeFrom("test_message_id_2").ToHex());
        result2[1].Sender.ShouldBe(Address.FromPublicKey("BBB".HexToByteArray()).ToByteString().ToBase64());
        result2[1].Receiver.ShouldBe(Address.FromPublicKey("AAA".HexToByteArray()).ToByteString().ToBase64());
        result2[1].TargetChainId.ShouldBe(13);
        result2[1].Data.ShouldBe(HashHelper.ComputeFrom("Message Data").ToByteString().ToBase64());
        result2[1].BlockHeight.ShouldBe(ctx2.Block.BlockHeight);
        result2[1].Epoch.ShouldBe(1);
    }

    private async Task<LogEventContext> MockSendRequested(string messageId = "test_message_id", long epoch = 0)
    {
        var logEvent = new SendRequested
        {
            MessageId = HashHelper.ComputeFrom(messageId),
            TargetChainId = 13,
            Receiver = Address.FromPublicKey("AAA".HexToByteArray()).ToByteString(),
            Sender = Address.FromPublicKey("BBB".HexToByteArray()).ToByteString(),
            Data = HashHelper.ComputeFrom("Message Data").ToByteString(),
            Epoch = epoch
        };

        var context = GenerateLogEventContext(logEvent);
        await _processor.ProcessAsync(logEvent, context);
        return context;
    }
}