using EasySoft.Core.Infrastructure.Assists;
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

        ApplicationConfigActionAssist.AddWebApplicationAction(application =>
        {
            application.UseAdvanceMigrationsEndPoint();
        });

        return builder;
    }
}