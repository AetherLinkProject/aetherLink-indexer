using AElfIndexer.Client;
using AElfIndexer.Client.Providers;
using AElfIndexer.Grains;
using AElfIndexer.Grains.Grain.Client;
using AElfIndexer.Grains.State.Client;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL.Dtos;
using AetherLink.Indexer.GraphQL.Input;
using GraphQL;
using Nest;
using Orleans;
using Volo.Abp.ObjectMapping;

namespace AetherLink.Indexer.GraphQL;

public class Query
{
    [Name("syncState")]
    public static async Task<SyncStateDto> SyncState([FromServices] IClusterClient clusterClient,
        [FromServices] IAElfIndexerClientInfoProvider clientInfoProvider, SyncStateInput input)
    {
        var version = clientInfoProvider.GetVersion();
        var clientId = clientInfoProvider.GetClientId();
        var blockStateSetInfoGrain =
            clusterClient.GetGrain<IBlockStateSetInfoGrain>(
                GrainIdHelper.GenerateGrainId("BlockStateSetInfo", clientId, input.ChainId, version));
        return new SyncStateDto
        {
            ConfirmedBlockHeight = await blockStateSetInfoGrain.GetConfirmedBlockHeight(input.FilterType)
        };
    }

    [Name("ocrJobEvents")]
    public static async Task<List<OcrJobEventDto>> OcrJobEventsQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, OcrLogEventInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<OcrJobEventIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));

        if (input.FromBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).GreaterThanOrEquals(input.FromBlockHeight)));
        }

        if (input.ToBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).LessThanOrEquals(input.ToBlockHeight)));
        }

        QueryContainer Filter(QueryContainerDescriptor<OcrJobEventIndex> f) => f.Bool(b => b.Must(mustQuery));

        var (_, logs) = await repository.GetListAsync(Filter);
        return objectMapper.Map<List<OcrJobEventIndex>, List<OcrJobEventDto>>(logs);
    }

    [Name("commitments")]
    public static async Task<List<CommitmentDto>> CommitmentsQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<CommitmentIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, CommitmentsInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<CommitmentIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));
        mustQuery.Add(q => q.Term(i => i.Field(f => f.RequestId).Value(input.RequestId)));

        QueryContainer Filter(QueryContainerDescriptor<CommitmentIndex> f) => f.Bool(b => b.Must(mustQuery));

        var (_, result) = await repository.GetListAsync(Filter);
        return objectMapper.Map<List<CommitmentIndex>, List<CommitmentDto>>(result);
    }

    [Name("configSets")]
    public static async Task<List<ConfigDigestDto>> ConfigDigestQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<ConfigDigestIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, ConfigDigestInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<ConfigDigestIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));

        QueryContainer Filter(QueryContainerDescriptor<ConfigDigestIndex> f) => f.Bool(b => b.Must(mustQuery));

        var (_, result) = await repository.GetListAsync(Filter);
        return objectMapper.Map<List<ConfigDigestIndex>, List<ConfigDigestDto>>(result);
    }

    [Name("latestRounds")]
    public static async Task<List<LatestRoundDto>> LatestRoundQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<LatestRoundIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, LatestRoundInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<LatestRoundIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));

        QueryContainer Filter(QueryContainerDescriptor<LatestRoundIndex> f) => f.Bool(b => b.Must(mustQuery));

        var (_, result) = await repository.GetListAsync(Filter);
        return objectMapper.Map<List<LatestRoundIndex>, List<LatestRoundDto>>(result);
    }
    
    [Name("requests")]
    public static async Task<List<OcrJobEventDto>> RequestsQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, RequestInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<OcrJobEventIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));
        mustQuery.Add(q => q.Term(i => i.Field(f => f.RequestId).Value(input.RequestId)));
        mustQuery.Add(q => q.Term(i => i.Field(f => f.RequestTypeIndex).Value(input.RequestType)));


        QueryContainer Filter(QueryContainerDescriptor<OcrJobEventIndex> f) => f.Bool(b => b.Must(mustQuery));

        var (_, logs) = await repository.GetListAsync(Filter);
        return objectMapper.Map<List<OcrJobEventIndex>, List<OcrJobEventDto>>(logs);
    }
}