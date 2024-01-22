using AElf.Indexing.Elasticsearch;
using AElfIndexer.Client;
using Nest;

namespace AetherLink.Indexer.Entities;

public class CommitmentIndex : AElfIndexerClientEntity<string>, IIndexBuild
{
    [Keyword] public string RequestId { get; set; }
    [Keyword] public string Commitment { get; set; }
}