using AElfIndexer.Client.Handlers;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL.Dtos;
using AutoMapper;

namespace AetherLink.Indexer;

public class AetherLinkIndexerAutoMapperProfile : Profile
{
    public AetherLinkIndexerAutoMapperProfile()
    {
        // LogEvent Processor Map
        CreateMap<LogEventContext, OcrJobEventIndex>();
        CreateMap<LogEventContext, ConfigDigestIndex>();
        CreateMap<LogEventContext, TransmittedIndex>();
        CreateMap<LogEventContext, RequestCancelledIndex>();

        // Query Map
        CreateMap<OcrJobEventIndex, CommitmentDto>();
        CreateMap<TransmittedIndex, TransmittedDto>();
        CreateMap<RequestCancelledIndex, RequestCancelledDto>();
        CreateMap<OcrJobEventIndex, OcrJobEventDto>();
        CreateMap<ConfigDigestIndex, ConfigDigestDto>();
        CreateMap<TransmittedIndex, RequestStartEpochDto>();
    }
}