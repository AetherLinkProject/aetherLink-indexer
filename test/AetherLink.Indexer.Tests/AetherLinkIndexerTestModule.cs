using AeFinder.App.TestBase;
using AetherLink.Indexer;
using AetherLink.Indexer.Processors;
using AetherLink.Indexer.Providers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace aetherLink.indexer;

[DependsOn(
    typeof(AeFinderAppTestBaseModule),
    typeof(AetherLinkIndexerModule))]
public class AetherLinkIndexerTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AeFinderAppEntityOptions>(options => { options.AddTypes<AetherLinkIndexerModule>(); });

        // Add your Processors.
        context.Services.AddSingleton<ConfigSetLogEventProcessor>();
        context.Services.AddSingleton<RequestCancelledLogEventProcessor>();
        context.Services.AddSingleton<RequestStartedLogEventProcessor>();
        context.Services.AddSingleton<TransmittedLogEventProcessor>();
        context.Services.AddSingleton<TransactionProcessor>();
        context.Services.AddSingleton<TransactionEventProvider>();
    }
}