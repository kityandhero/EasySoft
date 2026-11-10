using EasySoft.Core.MinIO.Assists;
using Microsoft.Extensions.DependencyInjection;

namespace EasySoft.Core.MinIO.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// AddMinIO
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddMinIO(this WebApplicationBuilder builder)
    {
        var configurator = ConfigAssist.GetConfigurator();

        var minioClient = new MinioClient()
            .WithEndpoint(configurator.GetEndpoint())
            .WithCredentials(configurator.GetAccessKey(), configurator.GetSecretKey());

        if (configurator.GetSsl())
        {
            minioClient = minioClient.WithSSL();
        }

        builder.Services.AddSingleton<IMinioClient>(minioClient.Build());

        return builder;
    }
}