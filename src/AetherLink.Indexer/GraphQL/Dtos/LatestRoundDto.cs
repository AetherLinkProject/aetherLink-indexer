namespace AetherLink.Indexer.GraphQL.Dtos;

public class LatestRoundDto
{
    public string ChainId { get; set; }
    public long EpochAndRound { get; set; }
}