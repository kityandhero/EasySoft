using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace Framework.CommonAssists;

public static class ServiceAssist
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;

    public static string GetServerPath(string path = "")
    {
        return ServiceProvider.GetRequiredService<IWebHostEnvironment>().WebRootPath + path;
    }
}