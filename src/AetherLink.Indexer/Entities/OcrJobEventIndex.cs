using AElf.Indexing.Elasticsearch;
using AElfIndexer.Client;
using Nest;

namespace AetherLink.Indexer.Entities;

public class OcrJobEventIndex : AElfIndexerClientEntity<string>, IIndexBuild
{
    [Keyword] public string TransactionId { get; set; }
    [Keyword] public string RequestId { get; set; }
    [Keyword] public string Commitment { get; set; }
    public long StartTime { get; set; }
    public long Epoch { get; set; }
    public int RequestTypeIndex { get; set; }
}