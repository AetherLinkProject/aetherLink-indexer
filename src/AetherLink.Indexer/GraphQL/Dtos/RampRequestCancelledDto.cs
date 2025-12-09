namespace AetherLink.Indexer.GraphQL.Dtos;

public class RampRequestCancelledDto
{
    public string ChainId { get; set; }
    public string MessageId { get; set; }
    public long BlockHeight { get; set; }
}