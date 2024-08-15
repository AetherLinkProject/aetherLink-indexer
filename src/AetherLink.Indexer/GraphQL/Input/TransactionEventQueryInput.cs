namespace AetherLink.Indexer.GraphQL.Input;

public class TransactionEventQueryInput
{
    public long FromBlockHeight { get; set; }
    public long ToBlockHeight { get; set; }
    public string ChainId { get; set; }
}