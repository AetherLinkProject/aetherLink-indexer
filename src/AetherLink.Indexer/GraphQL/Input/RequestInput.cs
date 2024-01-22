namespace AetherLink.Indexer.GraphQL.Input;

public class RequestInput
{
    public string ChainId { get; set; }
    public string RequestId { get; set; }
    public int RequestType { get; set; }
}