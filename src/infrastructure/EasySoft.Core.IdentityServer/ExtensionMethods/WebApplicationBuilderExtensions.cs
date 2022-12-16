using EasySoft.Core.IdentityServer.Configure;

namespace EasySoft.Core.IdentityServer.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddIdentityServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim = true;

                IdentityServerConfigurator.GetAllIdentityServerOptionActions().ForEach(action => { action(options); });
            })
            .AddInMemoryIdentityResources(IdentityServerConfigurator.IdentityResources)
            .AddInMemoryApiScopes(IdentityServerConfigurator.ApiScopes)
            .AddInMemoryClients(IdentityServerConfigurator.Clients);

        return builder;
    }
}