using EasySoft.Core.Infrastructure.Assists;
using LogDashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.LogDashboard.ExtensionMethods;

public static class WebApplicationExtensions
{
    public static WebApplication UseAdvanceHangfire(this WebApplication application)
    {
        if (EnvironmentAssist.GetEnvironment().IsDevelopment())
        {
            application.UseDeveloperExceptionPage();
        }

        application.UseLogDashboard();

        return application;
    }
}