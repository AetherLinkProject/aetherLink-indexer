namespace AetherLink.Indexer.Constants;

public static class EventWhitelist
{
    public static readonly List<string> Whitelist = new()
    {
        "MiningInformationUpdated",
        "ReceiptCreated",
        "ProposalReleased",
        "IrreversibleBlockFound",
        "SecretSharingInformation",
        "ProposalCreated"
    };
}