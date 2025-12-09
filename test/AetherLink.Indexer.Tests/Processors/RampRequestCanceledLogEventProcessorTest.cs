using AeFinder.Sdk;
using AeFinder.Sdk.Processor;
using AElf;
using AetherLink.Contracts.Ramp;
using aetherLink.indexer;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using Google.Protobuf;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Processors;

public sealed class RampRequestCanceledLogEventProcessorTest : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly RampRequestCancelledLogEventProcessor _processor;
    private readonly IReadOnlyRepository<RampRequestCancelledIndex> _repository;

    public RampRequestCanceledLogEventProcessorTest()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<RampRequestCancelledLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<RampRequestCancelledIndex>>();
    }

    [Fact]
    public async Task Query_Test()
    {
        var ctx = await MockRequestCanceled();
        var result = await Query.RampRequestCancelledQueryAsync(_repository, _objectMapper,
            new RequestCancelledInput
            {
                ChainId = "AELF",
                FromBlockHeight = 10,
                ToBlockHeight = 200
            });
        result.Count.ShouldBe(1);
        result.First().MessageId.ShouldBe(HashHelper.ComputeFrom("test_message_id").ToHex());
        result.First().ChainId.ShouldBe("AELF");
        result.First().BlockHeight.ShouldBe(ctx.Block.BlockHeight);
    }

    private async Task<LogEventContext> MockRequestCanceled()
    {
        var logEvent = new RequestCancelled { MessageId = HashHelper.ComputeFrom("test_message_id") };
        var context = GenerateLogEventContext(logEvent);
        await _processor.ProcessAsync(logEvent, context);
        return context;
    }
}