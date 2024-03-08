using AElf.Indexing.Elasticsearch;
using AElfIndexer.Client;
using Nest;

namespace AetherLink.Indexer.Entities;

public class RequestCancelledIndex : AElfIndexerClientEntity<string>, IIndexBuild
{
    [Keyword] public string RequestId { get; set; }
    public long StartTime { get; set; }
}