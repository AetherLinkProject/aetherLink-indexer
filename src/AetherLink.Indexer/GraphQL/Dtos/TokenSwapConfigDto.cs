namespace AetherLink.Indexer.GraphQL.Dtos;

public class TokenSwapConfigDto
{
    public string SwapId { get; set; }
    public long TargetChainId { get; set; }
    public string TargetContractAddress { get; set; }
    public string TokenAddress { get; set; }
    public string OriginToken { get; set; }
}