using AeFinder.Sdk;
using AElf;
using AElf.Types;
using AetherLink.Contracts.Oracle;
using aetherLink.indexer;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using Google.Protobuf;
using Nethereum.Hex.HexConvertors.Extensions;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Processors;

public class ConfigSetLogEventProcessorTests : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly ConfigSetLogEventProcessor _processor;
    private readonly IReadOnlyRepository<ConfigDigestIndex> _repository;

    public ConfigSetLogEventProcessorTests()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<ConfigSetLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<ConfigDigestIndex>>();
    }

    [Fact]
    public async Task Query_Test()
    {
        await MockConfigSet();
        var result = await Query.OracleConfigDigestQueryAsync(_repository, _objectMapper, new() { ChainId = "AELF" });
        result.ConfigDigest.ShouldNotBeEmpty();
        result.ChainId.ShouldBe("AELF");
    }

    private async Task MockConfigSet()
    {
        var logEvent = new ConfigSet
        {
            PreviousConfigBlockNumber = 1,
            ConfigDigest = HashHelper.ComputeFrom("default_config_digest"),
            ConfigCount = 2,
            Signers = new AddressList
            {
                Data = { Address.FromPublicKey("AAA".HexToByteArray()), Address.FromPublicKey("BBB".HexToByteArray()) }
            },
            Transmitters =
                new AddressList
                {
                    Data =
                    {
                        Address.FromPublicKey("CCC".HexToByteArray()), Address.FromPublicKey("DDD".HexToByteArray())
                    }
                },
            F = 1,
            OffChainConfigVersion = 1,
            OffChainConfig = ByteString.Empty
        };

        var logEventContext = GenerateLogEventContext(logEvent);
        await _processor.ProcessAsync(logEvent, logEventContext);
        _processor.GetContractAddress(logEventContext.ChainId);
    }
}