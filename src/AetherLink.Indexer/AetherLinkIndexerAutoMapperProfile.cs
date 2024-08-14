using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL.Dtos;
using AutoMapper;

namespace AetherLink.Indexer;

public class AetherLinkIndexerAutoMapperProfile : Profile
{
    public AetherLinkIndexerAutoMapperProfile()
    {
        // Transaction Processor Map
        CreateMap<TransactionInfo, TransactionEventIndex>();

        // LogEvent Processor Map
        CreateMap<LogEventContext, OcrJobEventIndex>();
        CreateMap<LogEventContext, ConfigDigestIndex>();
        CreateMap<LogEventContext, TransmittedIndex>();
        CreateMap<LogEventContext, RequestCancelledIndex>();
        CreateMap<LogEventContext, AIRequestIndex>();
        CreateMap<LogEventContext, AIReportTransmittedIndex>();

        // Query Map
        CreateMap<OcrJobEventIndex, CommitmentDto>();
        CreateMap<TransmittedIndex, TransmittedDto>();
        CreateMap<RequestCancelledIndex, RequestCancelledDto>();
        CreateMap<OcrJobEventIndex, OcrJobEventDto>();
        CreateMap<ConfigDigestIndex, ConfigDigestDto>();
        CreateMap<TransmittedIndex, RequestStartEpochDto>();
        CreateMap<TransactionEventIndex, TransactionEventDto>();
        CreateMap<AIRequestIndex, AIRequestDto>();
        CreateMap<AIReportTransmittedIndex, AIReportTransmittedDto>();
    }
}