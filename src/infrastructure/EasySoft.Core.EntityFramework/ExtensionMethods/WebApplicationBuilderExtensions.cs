using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.EntityFramework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceDbContext<T>(
        this WebApplicationBuilder builder,
        Action<DbContextOptionsBuilder> action
    ) where T : DbContext
    {
        if (FlagAssist.GetEntityFrameworkSwitch())
        {
            return builder;
        }

        builder.Services.AddDbContext<T>(action);

        FlagAssist.SetEntityFrameworkSwitchOpen();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(application => { application.UseAdvanceMigrationsEndPoint(); })
        );

        return builder;
    }
}