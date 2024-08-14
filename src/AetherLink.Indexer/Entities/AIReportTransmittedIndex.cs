using AElf.Indexing.Elasticsearch;
using AElfIndexer.Client;
using Nest;

namespace AetherLink.Indexer.Entities;

public class AIReportTransmittedIndex : AElfIndexerClientEntity<string>, IIndexBuild
{
    [Keyword] public string RequestId { get; set; }
    [Keyword] public string TransactionId { get; set; }
    public long StartTime { get; set; }
}