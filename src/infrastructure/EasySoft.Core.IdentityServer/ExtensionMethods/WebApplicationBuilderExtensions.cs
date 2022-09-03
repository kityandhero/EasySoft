using EasySoft.Core.IdentityServer.Configure;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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