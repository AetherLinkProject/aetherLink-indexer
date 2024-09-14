namespace AetherLink.Indexer.GraphQL.Dtos;

public class RampRequestDto
{
    public string ChainId { get; set; }
    public long BlockHeight { get; set; }
    public string MessageId { get; set; }
    public long TargetChainId { get; set; }
    public long SourceChainId { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Data { get; set; }
}