using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class TokenSwapConfigInfoIndex : AeFinderEntity, IAeFinderEntity
{
    public long TargetChainId { get; set; }
    public long SourceChainId { get; set; }
    [Keyword] public string Receiver { get; set; }
    [Keyword] public string TokenAddress { get; set; }
    [Keyword] public string Symbol { get; set; }
    [Keyword] public string ExtraData { get; set; }
}