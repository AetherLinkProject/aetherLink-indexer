using AeFinder.Sdk.Entities;
using Nest;

public class RampRequestManuallyExecutedIndex : AeFinderEntity, IAeFinderEntity
{
    [Keyword] public string ChainId { get; set; }
    [Keyword] public string MessageId { get; set; }
    public long BlockHeight { get; set; }
    [Keyword] public string TransactionId { get; set; }
    public long StartTime { get; set; }
}