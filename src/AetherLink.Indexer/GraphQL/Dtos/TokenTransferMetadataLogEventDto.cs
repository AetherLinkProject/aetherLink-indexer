namespace AetherLink.Indexer.GraphQL.Dtos;

public class TokenTransferMetadataLogEventDto
{
    public long? TargetChainId { get; set; }
    public string? TokenAddress { get; set; }
    public string? Symbol { get; set; }
    public long? Amount { get; set; }
}