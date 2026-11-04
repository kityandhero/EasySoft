namespace EasySoft.Core.IdentityServer.Configure;

public static class IdentityServerConfigurator
{
    private static readonly List<Action<IdentityServerOptions>> IdentityServerOptionActions = new();

    public static void AddIdentityServerOptionAction(Action<IdentityServerOptions> action)
    {
        IdentityServerOptionActions.Add(action);
    }

    public static IEnumerable<Action<IdentityServerOptions>> GetAllIdentityServerOptionActions()
    {
        return IdentityServerOptionActions;
    }

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new("server", new List<string> { "c1", "c2" })
    };

    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new("api", "MyApi")
    };

    public static IEnumerable<Client> Clients => new Client[]
    {
        new()
        {
            ClientId = "client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            AllowedScopes = { "api" }
        }
    };
}