using AeFinder.Sdk;
using AeFinder.Sdk.Processor;
using AElf;
using AElf.Types;
using AetherLink.Contracts.Oracle;
using aetherLink.indexer;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using AetherLink.Indexer.GraphQL.Input;
using Nethereum.Hex.HexConvertors.Extensions;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Processors;

public sealed class TransmittedLogEventProcessorTests : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly TransmittedLogEventProcessor _processor;
    private readonly IReadOnlyRepository<TransmittedIndex> _repository;

    public TransmittedLogEventProcessorTests()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<TransmittedLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<TransmittedIndex>>();
    }

    [Fact]
    public async Task Transmit_Success_Test()
    {
        var ctx = await MockTransmitted();
        var result = await Query.TransmittedQueryAsync(_repository, _objectMapper,
            new TransmittedInput
            {
                ChainId = "AELF",
                FromBlockHeight = 10,
                ToBlockHeight = 200,
            });
        result.Count.ShouldBe(1);
        result.First().BlockHeight.ShouldBe(ctx.Block.BlockHeight);
        result.First().ChainId.ShouldBe("AELF");
        result.First().Epoch.ShouldBe(1);
        result.First().RequestId.ShouldBe(HashHelper.ComputeFrom("default_request_id").ToHex());
    }

    [Fact]
    public async Task Query_Transmitted_LogEvent_Test()
    {
        var ctx = await MockTransmitted();
        var result = await Query.TransmittedQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result.Count.ShouldBe(1);
        result.First().BlockHeight.ShouldBe(ctx.Block.BlockHeight);
        result.First().ChainId.ShouldBe("AELF");
        result.First().Epoch.ShouldBe(1);
        result.First().RequestId.ShouldBe(HashHelper.ComputeFrom("default_request_id").ToHex());

        var ctx2 = await MockTransmitted(2, 120, "default_request_id_2");
        var result2 = await Query.TransmittedQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result2.Count.ShouldBe(2);
        result2[1].BlockHeight.ShouldBe(ctx2.Block.BlockHeight);
        result2[1].ChainId.ShouldBe("AELF");
        result2[1].Epoch.ShouldBe(2);
        result2[1].RequestId.ShouldBe(HashHelper.ComputeFrom("default_request_id_2").ToHex());
    }

    [Fact]
    public async Task Query_Epoch_LogEvent_Test()
    {
        await MockTransmitted();
        var result = await Query.OracleLatestEpochQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", BlockHeight = 110 });
        result.Epoch.ShouldBe(1);

        var result2 = await Query.OracleLatestEpochQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", BlockHeight = 10 });
        result2.Epoch.ShouldBe(0);

        await MockTransmitted(2, 120);
        var result3 = await Query.OracleLatestEpochQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", BlockHeight = 120 });
        result3.Epoch.ShouldBe(2);

        var result4 = await Query.OracleLatestEpochQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", BlockHeight = 110 });
        result4.Epoch.ShouldBe(1);
    }

    private async Task<LogEventContext> MockTransmitted(long nextEpoch = 1, long blockHeight = 100,
        string requestId = "default_request_id")
    {
        var logEvent = new Transmitted
        {
            RequestId = HashHelper.ComputeFrom(requestId),
            ConfigDigest = HashHelper.ComputeFrom("test"),
            EpochAndRound = nextEpoch,
            Transmitter = Address.FromPublicKey("CCC".HexToByteArray()),
        };

        var context = GenerateLogEventContext(logEvent);
        context.Block.BlockHeight = blockHeight;
        await _processor.ProcessAsync(logEvent, context);
        return context;
    }
}