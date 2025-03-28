using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class TransmittedIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string RequestId { get; set; }
    [Keyword] public string ChainId { get; set; }
    public long StartTime { get; set; }
    public long Epoch { get; set; }
    public long BlockHeight { get; set; }
    public string TransactionId { get; set; }
}