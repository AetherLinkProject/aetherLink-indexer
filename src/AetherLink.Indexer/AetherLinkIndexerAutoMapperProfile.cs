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
        CreateMap<LogEventContext, CommitmentIndex>();
        CreateMap<LogEventContext, ConfigDigestIndex>();
        CreateMap<LogEventContext, LatestRoundIndex>();

        // Query Map
        CreateMap<OcrJobEventIndex, OcrJobEventDto>();
        CreateMap<CommitmentIndex, CommitmentDto>();
        CreateMap<ConfigDigestIndex, ConfigDigestDto>();
        CreateMap<LatestRoundIndex, LatestRoundDto>();
    }
}