namespace AetherLink.Indexer.GraphQL.Dtos;

public class TransactionEventDto
{
    public string ChainId { get; set; }
    public string BlockHash { get; set; }
    public long BlockHeight { get; set; }
    public string TransactionId { get; set; }
    public string MethodName { get; set; }
    public long StartTime { get; set; }
    public string ContractAddress { get; set; }
    public string EventName { get; set; }
    public int Index { get; set; }
}