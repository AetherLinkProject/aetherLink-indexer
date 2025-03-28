namespace AetherLink.Indexer.GraphQL.Dtos;

public class RampCommitReportAcceptedDto
{
    public string ChainId { get; set; }
    public string TransactionId { get; set; }
    public long SourceChainId { get; set; }
    public long TargetChainId { get; set; }
    public string MessageId { get; set; }
    public long BlockHeight { get; set; }
}