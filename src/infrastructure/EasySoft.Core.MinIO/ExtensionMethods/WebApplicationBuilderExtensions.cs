using EasySoft.Core.MinIO.Assists;

namespace EasySoft.Core.MinIO.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddMinIO(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            var configurator = ConfigAssist.GetConfigurator();

            var minioClient = new MinioClient().WithEndpoint(configurator.GetEndpoint())
                .WithCredentials(configurator.GetAccessKey(), configurator.GetSecretKey());

            if (configurator.GetSsl())
            {
                minioClient = minioClient.WithSSL();
            }

            containerBuilder.RegisterInstance(minioClient.Build()).As<MinioClient>().SingleInstance();
        });

        return builder;
    }
}