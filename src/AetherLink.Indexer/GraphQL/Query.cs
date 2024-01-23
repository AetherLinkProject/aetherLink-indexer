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

    [Name("transmitted")]
    public static async Task<List<TransmittedDto>> TransmittedQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<TransmittedIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, TransmittedInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<TransmittedIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));

        if (input.FromBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).GreaterThanOrEquals(input.FromBlockHeight)));
        }

        if (input.ToBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).LessThanOrEquals(input.ToBlockHeight)));
        }

        QueryContainer Filter(QueryContainerDescriptor<TransmittedIndex> f) => f.Bool(b => b.Must(mustQuery));

        var (_, logs) = await repository.GetListAsync(Filter);
        return objectMapper.Map<List<TransmittedIndex>, List<TransmittedDto>>(logs);
    }

    [Name("requestCancelled")]
    public static async Task<List<RequestCancelledDto>> RequestCancelledQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<RequestCancelledIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, RequestCancelledInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<RequestCancelledIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));

        if (input.FromBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).GreaterThanOrEquals(input.FromBlockHeight)));
        }

        if (input.ToBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).LessThanOrEquals(input.ToBlockHeight)));
        }

        QueryContainer Filter(QueryContainerDescriptor<RequestCancelledIndex> f) => f.Bool(b => b.Must(mustQuery));

        var (_, logs) = await repository.GetListAsync(Filter);
        return objectMapper.Map<List<RequestCancelledIndex>, List<RequestCancelledDto>>(logs);
    }


    [Name("requestCommitment")]
    public static async Task<CommitmentDto> RequestCommitmentQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<OcrJobEventIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, RequestCommitmentInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<OcrJobEventIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));
        mustQuery.Add(q => q.Term(i => i.Field(f => f.RequestId).Value(input.RequestId)));

        QueryContainer Filter(QueryContainerDescriptor<OcrJobEventIndex> f) => f.Bool(b => b.Must(mustQuery));

        var commitment = await repository.GetAsync(Filter);
        return objectMapper.Map<OcrJobEventIndex, CommitmentDto>(commitment);
    }

    [Name("oracleConfigDigest")]
    public static async Task<ConfigDigestDto> OracleConfigDigestQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<ConfigDigestIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, OracleConfigDigestInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<ConfigDigestIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));

        QueryContainer Filter(QueryContainerDescriptor<ConfigDigestIndex> f) => f.Bool(b => b.Must(mustQuery));

        var result = await repository.GetAsync(Filter);
        return objectMapper.Map<ConfigDigestIndex, ConfigDigestDto>(result);
    }

    [Name("oracleLatestEpoch")]
    public static async Task<RequestStartEpochDto> OracleLatestEpochQueryAsync(
        [FromServices] IAElfIndexerClientEntityRepository<TransmittedIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, RequestStartEpochQueryInput input)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<TransmittedIndex>, QueryContainer>>();
        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(input.ChainId)));

        if (input.BlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).LessThanOrEquals(input.BlockHeight)));
        }

        QueryContainer Filter(QueryContainerDescriptor<TransmittedIndex> f) => f.Bool(b => b.Must(mustQuery));

        var (count, result) =
            await repository.GetListAsync(Filter, sortType: SortOrder.Descending, sortExp: k => k.BlockHeight);
        return count < 1
            ? new RequestStartEpochDto()
            : objectMapper.Map<TransmittedIndex, RequestStartEpochDto>(result[0]);
    }
}