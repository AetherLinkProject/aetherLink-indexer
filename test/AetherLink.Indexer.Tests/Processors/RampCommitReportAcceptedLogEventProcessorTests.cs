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

public class RampCommitReportAcceptedLogEventProcessorTests : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly RampCommitReportAcceptedLogEventProcessor _processor;
    private readonly IReadOnlyRepository<RampCommitReportAcceptedIndex> _repository;

    public RampCommitReportAcceptedLogEventProcessorTests()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<RampCommitReportAcceptedLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<RampCommitReportAcceptedIndex>>();
    }

    [Fact]
    public async Task Query_Request_Start_LogEvent_Test()
    {
        var ctx1 = await MockRampCommitReportAccepted();
        var result = await Query.RampCommitReportQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result.Count.ShouldBe(1);
        result.First().ChainId.ShouldBe("AELF");
        result.First().MessageId
            .ShouldBe(ByteString.CopyFrom(HashHelper.ComputeFrom("test_message_id").ToByteArray()).ToHex());
        result.First().BlockHeight.ShouldBe(ctx1.Block.BlockHeight);

        var ctx2 = await MockRampCommitReportAccepted("test_message_id_2");
        var result2 = await Query.RampCommitReportQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result2.Count.ShouldBe(2);
        result2[1].ChainId.ShouldBe("AELF");
        result2[1].MessageId
            .ShouldBe(ByteString.CopyFrom(HashHelper.ComputeFrom("test_message_id_2").ToByteArray()).ToHex());
        result2[1].BlockHeight.ShouldBe(ctx2.Block.BlockHeight);
    }

    private async Task<LogEventContext> MockRampCommitReportAccepted(string messageId = "test_message_id")
    {
        var logEvent = new CommitReportAccepted
        {
            Report = new()
            {
                ReportContext = new()
                {
                    MessageId = HashHelper.ComputeFrom(messageId),
                    SourceChainId = 2,
                    TargetChainId = 1,
                    Sender = Address.FromPublicKey("BBB".HexToByteArray()).ToByteString(),
                    Receiver = Address.FromPublicKey("AAA".HexToByteArray()).ToByteString()
                },
                Message = ByteString.CopyFrom("AAA".HexToByteArray()),
                TokenTransferMetadata = new()
                {
                    TargetChainId = 1,
                    TokenAddress = "test_TokenAddress",
                    Symbol = "test_OriginToken",
                    ExtraData = ByteString.CopyFromUtf8("test_SwapId")
                }
            }
        };

        var context = GenerateLogEventContext(logEvent);
        await _processor.ProcessAsync(logEvent, context);
        return context;
    }
}