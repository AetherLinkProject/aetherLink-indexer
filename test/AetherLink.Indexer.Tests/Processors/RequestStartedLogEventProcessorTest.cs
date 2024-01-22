using AElfIndexer.Client;
using AElfIndexer.Grains.State.Client;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Tests.Processors;

public class RequestStartedLogEventProcessorTest : AetherLinkIndexerDappTests
{
    private readonly IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> _repository;
    private readonly IObjectMapper _objectMapper;

    public RequestStartedLogEventProcessorTest()
    {
        _repository = GetRequiredService<IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo>>();
        
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Test()
    {
        await MockRequestStarted(20);
        var result = await Query.OcrJobEventsQueryAsync(_repository, _objectMapper,
            new OcrLogEventInput()
            {
                FromBlockHeight = 10,
                ToBlockHeight = 200,
            });
        result.Count.ShouldBe(1);
    }
}