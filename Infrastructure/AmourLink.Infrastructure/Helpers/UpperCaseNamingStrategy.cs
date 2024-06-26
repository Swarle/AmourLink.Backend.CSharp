using Newtonsoft.Json.Serialization;

namespace AmourLink.Infrastructure.Helpers;

public class UpperCaseNamingStrategy : SnakeCaseNamingStrategy
{
    protected override string ResolvePropertyName(string name)
    {
        return base.ResolvePropertyName(name).ToUpperInvariant();
    }
}