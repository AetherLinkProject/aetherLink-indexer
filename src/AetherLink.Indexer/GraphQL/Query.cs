using AeFinder.Sdk;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL.Dtos;
using AetherLink.Indexer.GraphQL.Input;
using GraphQL;
using Volo.Abp.ObjectMapping;

namespace AetherLink.Indexer.GraphQL;

public class Query
{
    [Name("ocrJobEvents")]
    public static async Task<List<OcrJobEventDto>> OcrJobEventsQueryAsync(
        [FromServices] IReadOnlyRepository<OcrJobEventIndex> repository, [FromServices] IObjectMapper objectMapper,
        OcrLogEventInput input)
    {
        var queryable = await repository.GetQueryableAsync();
        queryable = queryable.Where(a => a.ChainId == input.ChainId
                                         && a.BlockHeight <= input.ToBlockHeight
                                         && a.BlockHeight >= input.FromBlockHeight);
        return objectMapper.Map<List<OcrJobEventIndex>, List<OcrJobEventDto>>(queryable.ToList());
    }

    [Name("transmitted")]
    public static async Task<List<TransmittedDto>> TransmittedQueryAsync(
        [FromServices] IReadOnlyRepository<TransmittedIndex> repository, [FromServices] IObjectMapper objectMapper,
        TransmittedInput input)
    {
        var queryable = await repository.GetQueryableAsync();
        queryable = queryable.Where(a => a.ChainId == input.ChainId
                                         && a.BlockHeight <= input.ToBlockHeight
                                         && a.BlockHeight >= input.FromBlockHeight);
        return objectMapper.Map<List<TransmittedIndex>, List<TransmittedDto>>(queryable.ToList());
    }

    [Name("requestCancelled")]
    public static async Task<List<RequestCancelledDto>> RequestCancelledQueryAsync(
        [FromServices] IReadOnlyRepository<RequestCancelledIndex> repository, [FromServices] IObjectMapper objectMapper,
        RequestCancelledInput input)
    {
        var queryable = await repository.GetQueryableAsync();
        queryable = queryable.Where(a => a.ChainId == input.ChainId
                                         && a.BlockHeight <= input.ToBlockHeight
                                         && a.BlockHeight >= input.FromBlockHeight);
        return objectMapper.Map<List<RequestCancelledIndex>, List<RequestCancelledDto>>(queryable.ToList());
    }

    [Name("requestCommitment")]
    public static async Task<CommitmentDto> RequestCommitmentQueryAsync(
        [FromServices] IReadOnlyRepository<OcrJobEventIndex> repository, [FromServices] IObjectMapper objectMapper,
        RequestCommitmentInput input)
    {
        var queryable = await repository.GetQueryableAsync();
        queryable = queryable.Where(a => a.ChainId == input.ChainId && a.RequestId == input.RequestId);
        var result = queryable.FirstOrDefault();
        return result != null
            ? objectMapper.Map<OcrJobEventIndex, CommitmentDto>(result)
            : new();
    }

    [Name("oracleConfigDigest")]
    public static async Task<ConfigDigestDto> OracleConfigDigestQueryAsync(
        [FromServices] IReadOnlyRepository<ConfigDigestIndex> repository, [FromServices] IObjectMapper objectMapper,
        OracleConfigDigestInput input)
    {
        var queryable = await repository.GetQueryableAsync();
        queryable = queryable.Where(a => a.ChainId == input.ChainId);
        var result = queryable.FirstOrDefault();
        return result != null
            ? objectMapper.Map<ConfigDigestIndex, ConfigDigestDto>(result)
            : new();
    }

    [Name("oracleLatestEpoch")]
    public static async Task<RequestStartEpochDto> OracleLatestEpochQueryAsync(
        [FromServices] IReadOnlyRepository<TransmittedIndex> repository, [FromServices] IObjectMapper objectMapper,
        RequestStartEpochQueryInput input)
    {
        var queryable = await repository.GetQueryableAsync();
        queryable = queryable.Where(a => a.ChainId == input.ChainId
                                         && a.BlockHeight <= input.BlockHeight);
        var results = queryable.ToList().OrderByDescending(r => r.BlockHeight);
        // var results = queryable.OrderByDescending(r => r.Block.BlockHeight);
        return !results.Any()
            ? new RequestStartEpochDto()
            : objectMapper.Map<TransmittedIndex, RequestStartEpochDto>(results.First());
    }

    [Name("transactionEvents")]
    public static async Task<List<TransactionEventDto>> TransactionEventQueryAsync(
        [FromServices] IReadOnlyRepository<TransactionEventIndex> repository, [FromServices] IObjectMapper objectMapper,
        TransactionEventQueryInput input)
    {
        var queryable = await repository.GetQueryableAsync();
        queryable = queryable.Where(a => a.ChainId == input.ChainId
                                         && a.BlockHeight <= input.ToBlockHeight
                                         && a.BlockHeight >= input.FromBlockHeight);
        return objectMapper.Map<List<TransactionEventIndex>, List<TransactionEventDto>>(queryable.ToList());
    }

    [Name("rampRequests")]
    public static async Task<List<RampRequestDto>> RampRequestQueryAsync(
        [FromServices] IReadOnlyRepository<RampSendRequestedIndex> repository,
        [FromServices] IObjectMapper objectMapper, TransactionEventQueryInput input)
    {
        var queryable = await repository.GetQueryableAsync();
        var temp = queryable.ToList();
        queryable = queryable.Where(a => a.ChainId == input.ChainId
                                         && a.BlockHeight <= input.ToBlockHeight
                                         && a.BlockHeight >= input.FromBlockHeight);
        return objectMapper.Map<List<RampSendRequestedIndex>, List<RampRequestDto>>(queryable.ToList());
    }
}