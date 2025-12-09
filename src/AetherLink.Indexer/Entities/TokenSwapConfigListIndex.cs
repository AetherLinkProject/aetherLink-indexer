using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class TokenSwapConfigListIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string ContractAddress { get; set; }
    public List<string> TokenSwapConfigIdList { get; set; }
}
