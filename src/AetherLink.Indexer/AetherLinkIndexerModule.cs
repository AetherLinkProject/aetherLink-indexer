using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.Processors;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AetherLink.Indexer;

[DependsOn(typeof(AElfIndexerClientModule))]
public class AetherLinkIndexerModule : AElfIndexerClientPluginBaseModule<AetherLinkIndexerModule, AetherLinkIndexerSchema, Query>
{
    
    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
        var configuration = serviceCollection.GetConfiguration();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, RequestStartedLogEventProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, ConfigSetLogEventProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, TransmittedLogEventProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, RequestCancelledLogEventProcessor>();
        Configure<ContractInfoOptions>(configuration.GetSection("ContractInfo"));
    }

    protected override string ClientId => "*";
    protected override string Version => "*";
}