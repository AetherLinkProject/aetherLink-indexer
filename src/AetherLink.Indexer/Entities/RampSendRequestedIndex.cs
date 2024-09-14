using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class RampSendRequestedIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string ChainId { get; set; }
    public long BlockHeight { get; set; }
    [Keyword] public string MessageId { get; set; }
    public long TargetChainId { get; set; }
    public long SourceChainId { get; set; }
    [Keyword] public string Sender { get; set; }
    [Keyword] public string Receiver { get; set; }
    [Keyword] public string Data { get; set; }
}