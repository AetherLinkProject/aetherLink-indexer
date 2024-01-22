using AetherLink.Indexer.TestBase;
using Orleans.TestingHost;
using Volo.Abp.Modularity;

namespace AetherLink.Indexer.Orleans.TestBase;

public abstract class AetherLinkIndexerOrleansTestBase<TStartupModule> : AetherLinkIndexerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    protected readonly TestCluster Cluster;

    public AetherLinkIndexerOrleansTestBase()
    {
        Cluster = GetRequiredService<ClusterFixture>().Cluster;
    }
}