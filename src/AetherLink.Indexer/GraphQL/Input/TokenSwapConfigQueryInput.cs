namespace AetherLink.Indexer.GraphQL.Input;

public class TokenSwapConfigQueryInput
{
    public long TargetChainId { get; set; }
    public string TargetContractAddress { get; set; }
    public string? TokenAddress { get; set; }
    public string? OriginToken { get; set; }
}