using AeFinder.Sdk.Entities;
using Nest;

namespace AetherLink.Indexer.Entities;

public class TransactionEventIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string ChainId { get; set; }
    [Keyword] public string TransactionId { get; set; }
    [Keyword] public string BlockHash { get; set; }
    [Keyword] public string MethodName { get; set; }
    [Keyword] public string ContractAddress { get; set; }
    [Keyword] public string EventName { get; set; }
    [Keyword] public int Index { get; set; }
    public long StartTime { get; set; }
    public long BlockHeight { get; set; }
}