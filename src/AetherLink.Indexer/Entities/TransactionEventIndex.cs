using AElf.Indexing.Elasticsearch;
using AElfIndexer.Client;
using Nest;

namespace AetherLink.Indexer.Entities;

public class TransactionEventIndex : AElfIndexerClientEntity<string>, IIndexBuild
{
    [Keyword] public string TransactionId { get; set; }
    [Keyword] public string MethodName { get; set; }
    public long StartTime { get; set; }
    [Keyword] public string ContractAddress { get; set; }
    [Keyword] public string EventName { get; set; }
    [Keyword] public int Index { get; set; }
}