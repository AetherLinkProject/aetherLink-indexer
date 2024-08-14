namespace AetherLink.Indexer.GraphQL.Dtos;

public class AIReportTransmittedDto : OracleBasicDto
{
    public string TransactionId { get; set; }
    public long StartTime { get; set; }
}