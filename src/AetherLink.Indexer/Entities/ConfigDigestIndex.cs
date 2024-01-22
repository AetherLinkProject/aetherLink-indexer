using AElf.Indexing.Elasticsearch;
using AElfIndexer.Client;
using Nest;

namespace AetherLink.Indexer.Entities;

public class ConfigDigestIndex : AElfIndexerClientEntity<string>, IIndexBuild
{
    [Keyword] public string ConfigDigest { get; set; }
}