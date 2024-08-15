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
using Google.Protobuf;
using Nethereum.Hex.HexConvertors.Extensions;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Tests.Processors;

public sealed class ConfigSetLogEventProcessorTests : AetherLinkIndexerDappTests
{
    private readonly IObjectMapper _objectMapper;
    private readonly IAElfIndexerClientEntityRepository<ConfigDigestIndex, LogEventInfo> _repository;

    public ConfigSetLogEventProcessorTests()
    {
        _repository = GetRequiredService<IAElfIndexerClientEntityRepository<ConfigDigestIndex, LogEventInfo>>();
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Query_Test()
    {
        await MockConfigSet();
        var result = await Query.OracleConfigDigestQueryAsync(_repository, _objectMapper,
            new OracleConfigDigestInput
            {
                ChainId = "tDVW"
            });
        result.ConfigDigest.ShouldNotBeEmpty();
    }

    private async Task MockConfigSet(string chainId = "TEST", int height = 20)
    {
        var logEventContext = MockLogEventContext(height);
        var blockStateSetKey = await MockBlockState(logEventContext);

        var configSet = new ConfigSet
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
        var logEventInfo = MockLogEventInfo(configSet.ToLogEvent());
        var configSetLogEventProcessor = GetRequiredService<ConfigSetLogEventProcessor>();
        await configSetLogEventProcessor.HandleEventAsync(logEventInfo, logEventContext);
        configSetLogEventProcessor.GetContractAddress(chainId);
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);
    }
}