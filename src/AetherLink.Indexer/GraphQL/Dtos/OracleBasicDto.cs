namespace AetherLink.Indexer.GraphQL.Dtos;

public class OracleBasicDto
{
    public string ChainId { get; set; }
    public string RequestId { get; set; }
    public long BlockHeight { get; set; }
}