using AElfIndexer;

namespace AetherLink.Indexer.GraphQL.Input;

public class SyncStateInput
{
    public string ChainId { get; set; }
    public BlockFilterType FilterType { get; set; }
}