using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class RequestCancelledIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string ChainId { get; set; }
    [Keyword] public string RequestId { get; set; }
    public long BlockHeight { get; set; }
}