using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Client.Providers;
using AElfIndexer.Grains.State.Client;
using AetherLink.Indexer.Entities;
using Microsoft.Extensions.Logging;
using Orleans;
using Volo.Abp.ObjectMapping;

namespace AetherLink.Indexer.Handlers;

public class AetherLinkTransactionHandler : TransactionDataHandler
{
    private readonly IObjectMapper _objectMapper;
    private readonly HashSet<string> _unprocessedEvents;
    private readonly IAElfIndexerClientEntityRepository<TransactionEventIndex, TransactionInfo> _repository;

    public AetherLinkTransactionHandler(
        IClusterClient clusterClient, IAElfIndexerClientInfoProvider aelfIndexerClientInfoProvider,
        IDAppDataProvider dAppDataProvider, IBlockStateSetProvider<TransactionInfo> blockStateSetProvider,
        IDAppDataIndexManagerProvider dAppDataIndexManagerProvider, ILogger<AetherLinkTransactionHandler> logger,
        IEnumerable<IAElfLogEventProcessor<TransactionInfo>> processors, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<TransactionEventIndex, TransactionInfo> repository) : base(clusterClient,
        objectMapper, aelfIndexerClientInfoProvider, dAppDataProvider, blockStateSetProvider,
        dAppDataIndexManagerProvider, processors, logger)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _unprocessedEvents = new()
        {
            "MiningInformationUpdated",
            "ReceiptCreated",
            "ProposalReleased",
            "IrreversibleBlockFound",
            "SecretSharingInformation",
            "ProposalCreated"
        };
    }

    protected override async Task ProcessTransactionsAsync(List<TransactionInfo> transactions)
    {
        if (!transactions.Any()) return;

        Logger.LogDebug("Start processing {count} Transactions", transactions.Count);

        await Task.WhenAll(transactions.Select(ProcessTransactionAsync));
    }

    private async Task ProcessTransactionAsync(TransactionInfo transaction)
    {
        if (!transaction.LogEvents.Any()) return;

        Logger.LogDebug("Chain id: {chain}, BlockHeight: {height}, TransactionId: {transactionId}",
            transaction.ChainId, transaction.BlockHeight, transaction.TransactionId);

        var originTransaction = _objectMapper.Map<TransactionInfo, TransactionEventIndex>(transaction);
        await Task.WhenAll(transaction.LogEvents.Select(t =>
        {
            originTransaction.StartTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            return ProcessEventAsync(originTransaction, t);
        }));
    }

    private async Task ProcessEventAsync(TransactionEventIndex info, LogEventInfo logEvent)
    {
        if (_unprocessedEvents.Contains(logEvent.EventName))
        {
            Logger.LogDebug("{EventName} no need process.", logEvent.EventName);
            return;
        }

        info.EventName = logEvent.EventName;
        info.ContractAddress = logEvent.ContractAddress;
        info.Index = logEvent.Index;
        info.Id = IdGenerateHelper.GetId(IdGenerateHelper.LogInfoPrefix, info.ChainId, info.BlockHeight,
            info.TransactionId, info.MethodName, info.EventName, info.ContractAddress, info.Index);

        await _repository.AddOrUpdateAsync(info);
    }
}