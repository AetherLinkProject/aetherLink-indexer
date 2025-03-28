namespace AetherLink.Indexer.GraphQL.Dtos;

public class TransmittedDto : OracleBasicDto
{
    public string TransactionId { get; set; }
    public long Epoch { get; set; }
    public long StartTime { get; set; }
}