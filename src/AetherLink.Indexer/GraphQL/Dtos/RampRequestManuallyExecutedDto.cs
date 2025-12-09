namespace AetherLink.Indexer.GraphQL.Dtos;

public class RampRequestManuallyExecutedDto
{
    public string ChainId { get; set; }
    public string MessageId { get; set; }
    public long BlockHeight { get; set; }
    public string TransactionId { get; set; }
    public long StartTime { get; set; }
}