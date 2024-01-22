namespace AetherLink.Indexer.GraphQL.Dtos;

public class RequestInfoDto
{
    public string RequestId { get; set; }
    public string RequestingContract { get; set; }
    public string RequestingInitiator { get; set; }
    public string CallbackContractAddress { get; set; }
    public long SubscriptionId { get; set; }
    public string SubscriptionOwner { get; set; }
    public string Commitment { get; set; }
}