using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class RampCommitReportAcceptedIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string ChainId { get; set; }
    [Keyword] public string MessageId { get; set; }
    public long BlockHeight { get; set; }
    [Keyword] public string TransactionId { get; set; }
    public long SourceChainId { get; set; }
    public long TargetChainId { get; set; }
    public long CommitTime { get; set; }
}