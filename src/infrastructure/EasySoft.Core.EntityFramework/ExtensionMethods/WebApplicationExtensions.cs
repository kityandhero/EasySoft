using EasySoft.Core.Infrastructure.Assists;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.EntityFramework.ExtensionMethods;

public static class WebApplicationExtensions
{
    internal static WebApplication UseAdvanceMigrationsEndPoint(
        this WebApplication application
    )
    {
        if (EnvironmentAssist.GetEnvironment().IsDevelopment())
        {
            application.UseMigrationsEndPoint();
        }

        return application;
    }
}