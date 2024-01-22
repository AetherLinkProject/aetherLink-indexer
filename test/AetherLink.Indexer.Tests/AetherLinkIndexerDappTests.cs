using AElf;
using AElf.CSharp.Core.Extension;
using AElf.Types;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Client.Providers;
using AElfIndexer.Grains;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Orleans.TestBase;
using AetherLink.Indexer.Processors;
using AetherLink.Indexer.Tests.Helper;
using Google.Protobuf;
using Nethereum.Hex.HexConvertors.Extensions;

namespace AetherLink.Indexer.Tests;

public class AetherLinkIndexerDappTests : AetherLinkIndexerOrleansTestBase<AetherLinkIndexerDappTestModule>
{
    
    private readonly IAElfIndexerClientInfoProvider _indexerClientInfoProvider;
    public IBlockStateSetProvider<LogEventInfo> _blockStateSetLogEventInfoProvider;
    private readonly IBlockStateSetProvider<TransactionInfo> _blockStateSetTransactionInfoProvider;
    private readonly IDAppDataProvider _dAppDataProvider;
    private readonly IDAppDataIndexManagerProvider _dAppDataIndexManagerProvider;

    public AetherLinkIndexerDappTests()
    {
        _indexerClientInfoProvider = GetRequiredService<IAElfIndexerClientInfoProvider>();
        _blockStateSetLogEventInfoProvider = GetRequiredService<IBlockStateSetProvider<LogEventInfo>>();
        _blockStateSetTransactionInfoProvider = GetRequiredService<IBlockStateSetProvider<TransactionInfo>>();
        _dAppDataProvider = GetRequiredService<IDAppDataProvider>();
        _dAppDataIndexManagerProvider = GetRequiredService<IDAppDataIndexManagerProvider>();
    }

    protected async Task<string> InitializeBlockStateSetAsync(BlockStateSet<LogEventInfo> blockStateSet, string chainId)
    {
        var key = GrainIdHelper.GenerateGrainId("BlockStateSets", _indexerClientInfoProvider.GetClientId(), chainId,
            _indexerClientInfoProvider.GetVersion());

        await _blockStateSetLogEventInfoProvider.SetBlockStateSetAsync(key, blockStateSet);
        await _blockStateSetLogEventInfoProvider.SetCurrentBlockStateSetAsync(key, blockStateSet);
        await _blockStateSetLogEventInfoProvider.SetLongestChainBlockStateSetAsync(key, blockStateSet.BlockHash);

        return key;
    }

    protected async Task<string> InitializeBlockStateSetAsync(BlockStateSet<TransactionInfo> blockStateSet,
        string chainId)
    {
        var key = GrainIdHelper.GenerateGrainId("BlockStateSets", _indexerClientInfoProvider.GetClientId(), chainId,
            _indexerClientInfoProvider.GetVersion());

        await _blockStateSetTransactionInfoProvider.SetBlockStateSetAsync(key, blockStateSet);
        await _blockStateSetTransactionInfoProvider.SetCurrentBlockStateSetAsync(key, blockStateSet);
        await _blockStateSetTransactionInfoProvider.SetLongestChainBlockStateSetAsync(key, blockStateSet.BlockHash);

        return key;
    }

    protected async Task BlockStateSetSaveDataAsync<TSubscribeType>(string key)
    {
        await _dAppDataProvider.SaveDataAsync();
        await _dAppDataIndexManagerProvider.SavaDataAsync();
        if (typeof(TSubscribeType) == typeof(TransactionInfo))
            await _blockStateSetTransactionInfoProvider.SaveDataAsync(key);
        else if (typeof(TSubscribeType) == typeof(LogEventInfo))
            await _blockStateSetLogEventInfoProvider.SaveDataAsync(key);
    }

    protected LogEventContext MockLogEventContext(long inputBlockHeight)
    {        
        const string chainId = "tDVW";
        const string blockHash = "dac5cd67a2783d0a3d843426c2d45f1178f4d052235a907a0d796ae4659103b1";
        const string previousBlockHash = "e38c4fb1cf6af05878657cb3f7b5fc8a5fcfb2eec19cd76b73abb831973fbf4e";
        const string transactionId = "c1e625d135171c766999274a00a7003abed24cfe59a7215aabf1472ef20a2da2";
        long blockHeight = inputBlockHeight;
        return new LogEventContext
        {
            ChainId = chainId,
            BlockHeight = blockHeight,
            BlockHash = blockHash,
            PreviousBlockHash = previousBlockHash,
            TransactionId = transactionId,
            From = "aLyxCJvWMQH6UEykTyeWAcYss9baPyXkrMQ37BHnUicxD2LL3",
            BlockTime = DateTime.UtcNow,
        };
    }

    protected LogEventInfo MockLogEventInfo(LogEvent logEvent)
    {
        var logEventInfo = LogEventHelper.ConvertAElfLogEventToLogEventInfo(logEvent);
        var logEventContext = MockLogEventContext(100);
        logEventInfo.BlockHeight = logEventContext.BlockHeight;
        logEventInfo.ChainId = logEventContext.ChainId;
        logEventInfo.BlockHash = logEventContext.BlockHash;
        logEventInfo.TransactionId = logEventContext.TransactionId;
        return logEventInfo;
    }

    protected async Task<string> MockBlockState(LogEventContext logEventContext)
    {
        var blockStateSet = new BlockStateSet<LogEventInfo>
        {
            BlockHash = logEventContext.BlockHash,
            BlockHeight = logEventContext.BlockHeight,
            Confirmed = true,
            PreviousBlockHash = logEventContext.PreviousBlockHash
        };
        return await InitializeBlockStateSetAsync(blockStateSet, logEventContext.ChainId);
    }
    
    
    protected async Task MockConfigSet(int height)
    {
        var logEventContext = MockLogEventContext(height);
        var blockStateSetKey = await MockBlockState(logEventContext);

        var configSet = new ConfigSet()
        {
            PreviousConfigBlockNumber = 1,
            ConfigDigest = HashHelper.ComputeFrom("test@google.com"),
            ConfigCount = 2,
            Signers = new AddressList()
            {
                Data = { Address.FromPublicKey("AAA".HexToByteArray()), Address.FromPublicKey("BBB".HexToByteArray()) }
            },
            Transmitters = 
                new AddressList()
                {
                    Data = { Address.FromPublicKey("CCC".HexToByteArray()), Address.FromPublicKey("DDD".HexToByteArray()) }
                },
            F = 1,
            OffChainConfigVersion = 1,
            OffChainConfig = ByteString.Empty
        };
        var logEventInfo = MockLogEventInfo(configSet.ToLogEvent());
        var configSetLogEventProcessor = GetRequiredService<ConfigSetLogEventProcessor>();
        await configSetLogEventProcessor.HandleEventAsync(logEventInfo, logEventContext);
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);
    }
    
    protected async Task MockRequestStarted(int height)
    {
        var logEventContext = MockLogEventContext(height);
        var blockStateSetKey = await MockBlockState(logEventContext);

        var requestStarted = new RequestStarted()
        {
            RequestId = HashHelper.ComputeFrom("test@google.com"),
            RequestingContract = Address.FromPublicKey("AAA".HexToByteArray()),
            RequestingInitiator = Address.FromPublicKey("BBB".HexToByteArray()),
            SubscriptionId = 1,
            SubscriptionOwner = Address.FromPublicKey("CCC".HexToByteArray()),
            Commitment = HashHelper.ComputeFrom("Commitment").ToByteString(),
            RequestTypeIndex = 1
        };
        
        var logEventInfo = MockLogEventInfo(requestStarted.ToLogEvent());
        var requestStartedLogEventProcessor = GetRequiredService<RequestStartedLogEventProcessor>();
        await requestStartedLogEventProcessor.HandleEventAsync(logEventInfo, logEventContext);
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);
    }
    
    protected async Task MockTransmitted(int height)
    {
        var logEventContext = MockLogEventContext(height);
        var blockStateSetKey = await MockBlockState(logEventContext);

        var requestStarted = new Transmitted()
        {
            RequestId = HashHelper.ComputeFrom("test@google.com"),
            ConfigDigest = HashHelper.ComputeFrom("test"),
            EpochAndRound = 1,
            Transmitter = Address.FromPublicKey("CCC".HexToByteArray()),
        };
        
        var logEventInfo = MockLogEventInfo(requestStarted.ToLogEvent());
        var transmittedLogEventProcessor = GetRequiredService<TransmittedLogEventProcessor>();
        await transmittedLogEventProcessor.HandleEventAsync(logEventInfo, logEventContext);
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);
    }
    
}