using AeFinder.Sdk;
using AeFinder.Sdk.Processor;
using AElf;
using AetherLink.Contracts.Oracle;
using aetherLink.indexer;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Processors;

public sealed class RequestCanceledLogEventProcessorTest : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly RequestCancelledLogEventProcessor _processor;
    private readonly IReadOnlyRepository<RequestCancelledIndex> _repository;

    public RequestCanceledLogEventProcessorTest()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<RequestCancelledLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<RequestCancelledIndex>>();
    }

    [Fact]
    public async Task Query_Test()
    {
        var ctx = await MockRequestCanceled();
        var result = await Query.RequestCancelledQueryAsync(_repository, _objectMapper,
            new RequestCancelledInput
            {
                ChainId = "AELF",
                FromBlockHeight = 10,
                ToBlockHeight = 200
            });
        result.Count.ShouldBe(1);
        result.First().RequestId.ShouldBe(HashHelper.ComputeFrom("test_request_id").ToHex());
        result.First().ChainId.ShouldBe("AELF");
        result.First().BlockHeight.ShouldBe(ctx.Block.BlockHeight);
    }

    private async Task<LogEventContext> MockRequestCanceled()
    {
        var logEvent = new RequestCancelled { RequestId = HashHelper.ComputeFrom("test_request_id") };
        var context = GenerateLogEventContext(logEvent);
        await _processor.ProcessAsync(logEvent, context);
        return context;
    }
}