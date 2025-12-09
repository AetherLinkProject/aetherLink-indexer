using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class OcrJobEventIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string TransactionId { get; set; }
    [Keyword] public string RequestId { get; set; }
    [Keyword] public string Commitment { get; set; }
    [Keyword] public string ChainId { get; set; }
    [Keyword] public string BlockHash { get; set; }
    public long BlockHeight { get; set; }
    public long StartTime { get; set; }
    public int RequestTypeIndex { get; set; }
}