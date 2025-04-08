using AeFinder.Sdk;
using AeFinder.Sdk.Processor;
using AElf;
using AetherLink.Contracts.Ramp;
using aetherLink.indexer;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using Google.Protobuf;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Processors;

public sealed class RampRequestManuallyExecutedLogEventProcessorTest : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly RampRequestManuallyExecutedLogEventProcessor _processor;
    private readonly IReadOnlyRepository<RampRequestManuallyExecutedIndex> _repository;

    public RampRequestManuallyExecutedLogEventProcessorTest()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<RampRequestManuallyExecutedLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<RampRequestManuallyExecutedIndex>>();
    }

    [Fact]
    public async Task Query_Test()
    {
        var ctx = await MockRequestManuallyExecuted();
        var result = await Query.RampRequestManuallyExecuteQueryAsync(_repository, _objectMapper,
            new RequestManuallyExecutedInput()
            {
                ChainId = "AELF",
                FromBlockHeight = 10,
                ToBlockHeight = 200
            });
        result.Count.ShouldBe(1);
        result.First().MessageId
            .ShouldBe(ByteString.CopyFrom(HashHelper.ComputeFrom("test_message_id").ToByteArray()).ToBase64());
        result.First().ChainId.ShouldBe("AELF");
        result.First().BlockHeight.ShouldBe(ctx.Block.BlockHeight);
    }

    private async Task<LogEventContext> MockRequestManuallyExecuted()
    {
        var logEvent = new RequestManuallyExecuted { MessageId = HashHelper.ComputeFrom("test_message_id") };
        var context = GenerateLogEventContext(logEvent);
        await _processor.ProcessAsync(logEvent, context);
        return context;
    }
}