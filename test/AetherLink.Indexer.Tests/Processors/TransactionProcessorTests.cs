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
using Transaction = AeFinder.Sdk.Processor.Transaction;

namespace AetherLink.Indexer.Processors;

public class TransactionProcessorTests : AetherLinkIndexerTestBase
{
    private readonly IObjectMapper _objectMapper;
    private readonly TransactionProcessor _processor;
    private readonly IReadOnlyRepository<TransactionEventIndex> _repository;

    public TransactionProcessorTests()
    {
        _objectMapper = GetRequiredService<IObjectMapper>();
        _processor = GetRequiredService<TransactionProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<TransactionEventIndex>>();
    }

    [Fact]
    public async Task Transmit_Success_Test()
    {
        var ctx = await MockTransaction();
        
        var result = await Query.TransactionEventQueryAsync(_repository, _objectMapper,
            new() { ChainId = "AELF", FromBlockHeight = 10, ToBlockHeight = 200 });
        result.Count.ShouldBe(3);
        result.First().BlockHeight.ShouldBe(ctx.Block.BlockHeight);
        result.First().ChainId.ShouldBe("AELF");
        foreach (var r in result)
        {
            r.MethodName.ShouldBe("TestMethod");
            switch (r.Index)
            {
                case 1:
                    r.ContractAddress.ShouldBe(GenerateAddress("AAA"));
                    r.EventName.ShouldBe("TestEvent1");
                    r.Index.ShouldBe(1);
                    break;
                case 2:
                    r.ContractAddress.ShouldBe(GenerateAddress("BBB"));
                    r.EventName.ShouldBe("TestEvent2");
                    r.Index.ShouldBe(2);
                    break;
                case 3:
                    r.ContractAddress.ShouldBe(GenerateAddress("CCC"));
                    r.EventName.ShouldBe("TestEvent3");
                    r.Index.ShouldBe(3);
                    break;
            }
        }
    }

    private async Task<TransactionContext> MockTransaction()
    {
        var transaction = new Transaction
        {
            TransactionId = "4e07408562bedb8b60ce05c1decfe3ad16b72230967de01f640b7e4729b49fce",
            From = "2EM5uV6bSJh6xJfZTUa1pZpYsYcCUAdPvZvFUJzMDJEx3rbioz",
            To = "2ktxGpyiYCjFU5KwuXtbBckczX6uPmEtesJEsQPqMukcHZFY9a",
            MethodName = "TestMethod",
            Status = TransactionStatus.Mined,
            Index = 1,
            LogEvents = new()
            {
                new() { ContractAddress = GenerateAddress("AAA"), EventName = "TestEvent1", Index = 1 },
                new() { ContractAddress = GenerateAddress("BBB"), EventName = "TestEvent2", Index = 2 },
                new() { ContractAddress = GenerateAddress("CCC"), EventName = "TestEvent3", Index = 3 }
            }
        };

        var context = GenerateTransactionContext();
        await _processor.ProcessAsync(transaction, context);
        return context;
    }

    private string GenerateAddress(string pub) => Address.FromPublicKey(pub.HexToByteArray()).ToString().Trim('"');
}