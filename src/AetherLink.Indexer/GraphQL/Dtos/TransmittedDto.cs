namespace AetherLink.Indexer.GraphQL.Dtos;

public class TransmittedDto : OracleBasicDto
{
    public long Epoch { get; set; }
    public long StartTime { get; set; }
}