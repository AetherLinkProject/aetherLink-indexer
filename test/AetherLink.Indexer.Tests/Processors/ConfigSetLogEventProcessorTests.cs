using AElfIndexer.Client;
using AElfIndexer.Grains.State.Client;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Tests.Processors;

public class ConfigSetLogEventProcessorTests : AetherLinkIndexerDappTests
{
    private readonly IAElfIndexerClientEntityRepository<ConfigDigestIndex, LogEventInfo> _repository;
    private readonly IObjectMapper _objectMapper;

    public ConfigSetLogEventProcessorTests()
    {
        _repository = GetRequiredService<IAElfIndexerClientEntityRepository<ConfigDigestIndex, LogEventInfo>>();
        
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Test()
    {
        await MockConfigSet(20);
        var result = await Query.ConfigDigestQueryAsync(_repository, _objectMapper,
            new ConfigDigestInput()
            {
                ChainId = "tDVW"
            });
        result.Count.ShouldBe(1);
    }
}