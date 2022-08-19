using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.Infrastructure.Assists;

public static class ServiceAssist
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;

    public static string GetServerPath(string path = "")
    {
        return ServiceProvider.GetRequiredService<IWebHostEnvironment>().WebRootPath + path;
    }
}