namespace AetherLink.Indexer.GraphQL.Input;

public class RequestStartEpochQueryInput
{
    public string ChainId { get; set; }
    public long BlockHeight { get; set; }
}