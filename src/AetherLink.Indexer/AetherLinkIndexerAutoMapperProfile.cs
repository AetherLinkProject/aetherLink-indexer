using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL.Dtos;
using AutoMapper;

namespace AetherLink.Indexer;

public class AetherLinkIndexerProfile : Profile
{
    public AetherLinkIndexerProfile()
    {
        CreateMap<OcrJobEventIndex, CommitmentDto>();
        CreateMap<TransmittedIndex, TransmittedDto>();
        CreateMap<RequestCancelledIndex, RequestCancelledDto>();
        CreateMap<OcrJobEventIndex, OcrJobEventDto>();
        CreateMap<ConfigDigestIndex, ConfigDigestDto>();
        CreateMap<TransmittedIndex, RequestStartEpochDto>();
        CreateMap<TransactionEventIndex, TransactionEventDto>();
        CreateMap<RampSendRequestedIndex, RampRequestDto>();
    }
}