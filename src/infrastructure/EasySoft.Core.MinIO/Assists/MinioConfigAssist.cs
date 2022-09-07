using EasySoft.Core.MinIO.configures;

namespace EasySoft.Core.MinIO.Assists;

public static class ConfigAssist
{
    private static readonly Configurator Configurator = new();

    public static Configurator GetConfigurator()
    {
        return Configurator;
    }

    public static Configurator GetEndpoint(string endpoint)
    {
        Configurator.SetEndpoint(endpoint);

        return Configurator;
    }

    public static Configurator SetAccessKey(string accessKey)
    {
        Configurator.SetAccessKey(accessKey);

        return Configurator;
    }
    
    public static Configurator SetSecretKey(string secretKey)
    {
        Configurator.SetSecretKey(secretKey);

        return Configurator;
    }

    public static Configurator SetSsl(bool ssl)
    {
        Configurator.SetSsl(ssl);

        return Configurator;
    }
}