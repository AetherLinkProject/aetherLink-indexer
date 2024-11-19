namespace AetherLink.Indexer.GraphQL.Dtos;

public class RampRequestDto
{
    public string ChainId { get; set; }
    public string TransactionId { get; set; }
    public long BlockHeight { get; set; }
    public string MessageId { get; set; }
    public long TargetChainId { get; set; }
    public long SourceChainId { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Message { get; set; }
    public TokenSwapConfigDto TokenAmount { get; set; }
    public long Epoch { get; set; }
    public long StartTime { get; set; }
}