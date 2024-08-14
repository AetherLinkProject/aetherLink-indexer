namespace AetherLink.Indexer.GraphQL.Dtos;

public class AIRequestDto : OracleBasicDto
{
    public string TransactionId { get; set; }
    public long StartTime { get; set; }
    public string Commitment { get; set; }
}