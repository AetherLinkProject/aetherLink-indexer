using AElf;
using AElf.CSharp.Core.Extension;
using AElf.Types;
using AElfIndexer.Client;
using AElfIndexer.Grains.State.Client;
using AetherLink.Contracts.Oracle;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using AetherLink.Indexer.Processors;
using Nethereum.Hex.HexConvertors.Extensions;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Tests.Processors;

public sealed class TransmittedLogEventProcessorTests : AetherLinkIndexerDappTests
{
    private readonly IAElfIndexerClientEntityRepository<TransmittedIndex, LogEventInfo> _repository;
    private readonly IObjectMapper _objectMapper;

    public TransmittedLogEventProcessorTests()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _repository = GetRequiredService<IAElfIndexerClientEntityRepository<TransmittedIndex, LogEventInfo>>();
    }

    [Fact]
    public async Task Transmit_Success_Test()
    {
        var logEventContext = MockLogEventContext(20);
        var blockStateSetKey = await MockBlockState(logEventContext);

        var requestStarted = new Transmitted
        {
            RequestId = HashHelper.ComputeFrom("test_request_id"),
            ConfigDigest = HashHelper.ComputeFrom("test"),
            EpochAndRound = 1,
            Transmitter = Address.FromPublicKey("CCC".HexToByteArray()),
        };

        var logEventInfo = MockLogEventInfo(requestStarted.ToLogEvent());
        var transmittedLogEventProcessor = GetRequiredService<TransmittedLogEventProcessor>();
        await transmittedLogEventProcessor.HandleEventAsync(logEventInfo, logEventContext);
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);

        var result = await Query.TransmittedQueryAsync(_repository, _objectMapper,
            new TransmittedInput
            {
                FromBlockHeight = 10,
                ToBlockHeight = 200,
            });
        result.Count.ShouldBe(1);
    }

    [Fact]
    public async Task Query_Transmitted_LogEvent_Test()
    {
        await MockTransmitted();
        var result = await Query.TransmittedQueryAsync(_repository, _objectMapper,
            new TransmittedInput
            {
                FromBlockHeight = 10,
                ToBlockHeight = 200,
            });
        result.Count.ShouldBe(1);

        await MockTransmitted(blockHeight: 120, nextEpoch: 2);
        var result2 = await Query.TransmittedQueryAsync(_repository, _objectMapper,
            new TransmittedInput
            {
                FromBlockHeight = 10,
                ToBlockHeight = 200,
            });
        result2.Count.ShouldBe(2);
    }

    [Fact]
    public async Task Query_Epoch_LogEvent_Test()
    {
        await MockTransmitted();
        var result = await Query.OracleLatestEpochQueryAsync(_repository, _objectMapper,
            new RequestStartEpochQueryInput
            {
                BlockHeight = 100
            });
        result.Epoch.ShouldBe(1);

        var result2 = await Query.OracleLatestEpochQueryAsync(_repository, _objectMapper,
            new RequestStartEpochQueryInput
            {
                BlockHeight = 10
            });
        result2.Epoch.ShouldBe(0);

        // add new transmitted event in 120 block height
        await MockTransmitted(blockHeight: 120, nextEpoch: 2);
        var result3 = await Query.OracleLatestEpochQueryAsync(_repository, _objectMapper,
            new RequestStartEpochQueryInput
            {
                BlockHeight = 120
            });
        result3.Epoch.ShouldBe(2);

        var result4 = await Query.OracleLatestEpochQueryAsync(_repository, _objectMapper,
            new RequestStartEpochQueryInput
            {
                BlockHeight = 110
            });
        result4.Epoch.ShouldBe(1);
    }

    private async Task MockTransmitted(string chainId = "TEST", long blockHeight = 20,
        string userEmail = "default_request_id", string configDigest = "test", string transmitter = "CCC",
        long nextEpoch = 1)
    {
        var logEventContext = MockLogEventContext(blockHeight);
        var blockStateSetKey = await MockBlockState(logEventContext);
        var requestStarted = new Transmitted
        {
            RequestId = HashHelper.ComputeFrom(userEmail),
            ConfigDigest = HashHelper.ComputeFrom(configDigest),
            EpochAndRound = nextEpoch,
            Transmitter = Address.FromPublicKey(transmitter.HexToByteArray()),
        };

        var logEventInfo = MockLogEventInfo(requestStarted.ToLogEvent());
        var transmittedLogEventProcessor = GetRequiredService<TransmittedLogEventProcessor>();
        await transmittedLogEventProcessor.HandleEventAsync(logEventInfo, logEventContext);
        transmittedLogEventProcessor.GetContractAddress(chainId);
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);
    }
}