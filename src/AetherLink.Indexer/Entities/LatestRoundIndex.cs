using AElf.Indexing.Elasticsearch;
using AElfIndexer.Client;

namespace AetherLink.Indexer.Entities;

public class LatestRoundIndex : AElfIndexerClientEntity<string>, IIndexBuild
{
    public long EpochAndRound { get; set; }
}