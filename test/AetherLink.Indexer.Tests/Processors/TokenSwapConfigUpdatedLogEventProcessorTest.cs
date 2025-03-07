using AeFinder.Sdk;
using AElf;
using AElf.Types;
using AetherLink.Contracts.Ramp;
using aetherLink.indexer;
using AetherLink.Indexer.Entities;
using AetherLink.Indexer.GraphQL;
using Google.Protobuf;
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
                SourceChainId = 9100,
                Receiver = "ABC",
                // TokenAddress = "AAA",
                Symbol = "TEST1"
            });
        result.TargetChainId.ShouldBe(1100);
        result.SourceChainId.ShouldBe(9100);
        result.Receiver.ShouldBe("ABC");
        result.TokenAddress.ShouldBe("AAA");
        result.Symbol.ShouldBe("TEST1");
        result.ExtraData.ShouldBe(ByteString.CopyFromUtf8("SwapId1").ToBase64());

        var result2 = await Query.TokenSwapConfigQueryAsync(_repository, _objectMapper,
            new()
            {
                TargetChainId = 1100,
                SourceChainId = 9100,
                Receiver = "EDF",
                TokenAddress = "EEE",
                // Symbol = "TEST2",
            });
        result2.TargetChainId.ShouldBe(1100);
        result2.SourceChainId.ShouldBe(9100);
        result2.Receiver.ShouldBe("EDF");
        result2.TokenAddress.ShouldBe("EEE");
        result2.Symbol.ShouldBe("TEST2");
        // result2.SwapId.ShouldBe("SwapId2");
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
                        SourceChainId = 9100,
                        Receiver = "ABC",
                        TokenAddress = "AAA",
                        Symbol = "TEST1",
                        ExtraData = ByteString.CopyFromUtf8("SwapId1")
                    },
                    new TokenSwapInfo
                    {
                        TargetChainId = 1100,
                        SourceChainId = 9100,
                        Receiver = "EDF",
                        TokenAddress = "EEE",
                        Symbol = "TEST2",
                        ExtraData = ByteString.CopyFromUtf8("SwapId2")
                    },
                }
            }
        };
        await _processor.ProcessAsync(logEvent, GenerateLogEventContext(logEvent));
    }
}