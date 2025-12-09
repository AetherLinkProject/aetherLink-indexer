namespace AetherLink.Indexer.Constants;

public static class EventWhitelist
{
    public static readonly List<string> Whitelist = new()
    {
        "TransactionFeeClaimed",
        "TransactionFeeCharged",
        "RentalCharged",
        "ParentChainIndexed",
        "MiningInformationUpdated",
        "ReceiptCreated",
        "ProposalReleased",
        "IrreversibleBlockFound",
        "SecretSharingInformation",
        "ProposalCreated",
        // CROSSCHAIN
        "SideChainBlockDataIndexed",
        "SideChainIndexed",
        "CrossChainIndexingDataProposedEvent"
    };
}