using AeFinder.Sdk;
using AElf;
using AElf.Types;
using AetherLink.Contracts.Ramp;
using aetherLink.indexer;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using Nethereum.Hex.HexConvertors.Extensions;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace AetherLink.Indexer.Processors;

public class TokenSwapConfigUpdatedLogEventProcessorTest : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly TokenSwapConfigUpdatedLogEventProcessor _processor;
    private readonly IReadOnlyRepository<TokenSwapConfigInfoIndex> _repository;

    public TokenSwapConfigUpdatedLogEventProcessorTest()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<TokenSwapConfigUpdatedLogEventProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<TokenSwapConfigInfoIndex>>();
    }

    [Fact]
    public async Task Query_Token_Swap_Config_Updated_LogEvent_Test()
    {
        await MockTokenSwapConfigUpdated();
        var result = await Query.TokenSwapConfigQueryAsync(_repository, _objectMapper,
            new()
            {
                TargetChainId = 1100,
                TargetContractAddress = "ABC",
                TokenAddress = "AAA",
                OriginToken = "TEST1"
            });
        result.TargetChainId.ShouldBe(1100);
        result.TargetContractAddress.ShouldBe("ABC");
        result.TokenAddress.ShouldBe("AAA");
        result.OriginToken.ShouldBe("TEST1");
        result.SwapId.ShouldBe("SwapId1");

        var result2 = await Query.TokenSwapConfigQueryAsync(_repository, _objectMapper,
            new()
            {
                TargetChainId = 1100,
                TargetContractAddress = "EDF",
                TokenAddress = "EEE",
                OriginToken = "TEST2",
            });
        result2.TargetChainId.ShouldBe(1100);
        result2.TargetContractAddress.ShouldBe("EDF");
        result2.TokenAddress.ShouldBe("EEE");
        result2.OriginToken.ShouldBe("TEST2");
        result2.SwapId.ShouldBe("SwapId2");
    }

    private async Task MockTokenSwapConfigUpdated()
    {
        var logEvent = new TokenSwapConfigUpdated
        {
            ContractAddress = Address.FromPublicKey("AAA".HexToByteArray()),
            TokenSwapList = new()
            {
                TokenSwapInfoList =
                {
                    new TokenSwapInfo
                    {
                        TargetChainId = 1100,
                        TargetContractAddress = "ABC",
                        TokenAddress = "AAA",
                        OriginToken = "TEST1",
                        SwapId = "SwapId1"
                    },
                    new TokenSwapInfo
                    {
                        TargetChainId = 1100,
                        TargetContractAddress = "EDF",
                        TokenAddress = "EEE",
                        OriginToken = "TEST2",
                        SwapId = "SwapId2"
                    },
                }
            }
        };
        await _processor.ProcessAsync(logEvent, GenerateLogEventContext(logEvent));
    }
}