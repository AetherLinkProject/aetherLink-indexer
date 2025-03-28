namespace AetherLink.Indexer.GraphQL.Dtos;

public class TokenSwapConfigDto
{
    public long TargetChainId { get; set; }
    public long SourceChainId { get; set; }
    public string Receiver { get; set; }
    public string TokenAddress { get; set; }
    public string Symbol { get; set; }
    public string ExtraData { get; set; }
}