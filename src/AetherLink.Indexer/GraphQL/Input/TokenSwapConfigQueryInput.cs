namespace AetherLink.Indexer.GraphQL.Input;

public class TokenSwapConfigQueryInput
{
    public long TargetChainId { get; set; }
    public long SourceChainId { get; set; }
    public string Receiver { get; set; }
    public string? TokenAddress { get; set; }
    public string? Symbol { get; set; }
}