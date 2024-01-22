using AElfIndexer.Client.GraphQL;

namespace AetherLink.Indexer.GraphQL;

public class AetherLinkIndexerSchema : AElfIndexerClientSchema<Query>
{
    public AetherLinkIndexerSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}