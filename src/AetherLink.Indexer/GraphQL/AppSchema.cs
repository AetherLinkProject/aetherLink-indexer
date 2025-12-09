using AeFinder.Sdk;

namespace AetherLink.Indexer.GraphQL;

public class AppSchema : AppSchema<Query>
{
    public AppSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}