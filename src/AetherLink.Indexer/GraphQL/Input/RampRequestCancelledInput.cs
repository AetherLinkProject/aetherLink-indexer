namespace AetherLink.Indexer.GraphQL.Input;

public class RampRequestCancelledInput
{
    public long FromBlockHeight { get; set; }
    public long ToBlockHeight { get; set; }
    public string ChainId { get; set; }
}