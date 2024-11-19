using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class TokenSwapConfigInfoIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string SwapId { get; set; }
    public long TargetChainId { get; set; }
    [Keyword] public string TargetContractAddress { get; set; }
    [Keyword] public string TokenAddress { get; set; }
    [Keyword] public string OriginToken { get; set; }
}