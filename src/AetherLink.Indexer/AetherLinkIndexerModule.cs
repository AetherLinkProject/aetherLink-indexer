using AeFinder.Sdk.Processor;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.Processors;
using AetherLink.Indexer.Providers;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AetherLink.Indexer;

public class AetherLinkIndexerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<AetherLinkIndexerModule>(); });
        context.Services.AddSingleton<ISchema, AppSchema>();
        context.Services.AddSingleton<ITransactionEventProvider, TransactionEventProvider>();
        context.Services.AddTransient<ILogEventProcessor, RequestStartedLogEventProcessor>();
        context.Services.AddTransient<ILogEventProcessor, ConfigSetLogEventProcessor>();
        context.Services.AddTransient<ILogEventProcessor, TransmittedLogEventProcessor>();
        context.Services.AddTransient<ILogEventProcessor, RequestCancelledLogEventProcessor>();
        context.Services.AddTransient<ILogEventProcessor, RampSendRequestedLogEventProcessor>();
        context.Services.AddTransient<ILogEventProcessor, RampRequestCancelledLogEventProcessor>();
        context.Services.AddTransient<ILogEventProcessor, RampRequestManuallyExecutedLogEventProcessor>();
        context.Services.AddTransient<ILogEventProcessor, RampCommitReportAcceptedLogEventProcessor>();
        context.Services.AddTransient<ILogEventProcessor, TokenSwapConfigUpdatedLogEventProcessor>();
        context.Services.AddTransient<ITransactionProcessor, TransactionProcessor>();
        context.Services.AddTransient<IBlockProcessor, BlockProcessor>();
    }
}