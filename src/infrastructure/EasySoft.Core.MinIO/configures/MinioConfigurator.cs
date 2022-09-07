namespace EasySoft.Core.MinIO.configures;

public sealed class Configurator : IConfigurator
{
    private string _endpoint = "";
    private string _accessKey = "";
    private string _secretKey = "";
    private bool _ssl;

    public Configurator SetEndpoint(string endpoint)
    {
        _endpoint = endpoint;

        return this;
    }

    public string GetEndpoint()
    {
        return _endpoint;
    }

    public Configurator SetAccessKey(string accessKey)
    {
        _accessKey = accessKey;

        return this;
    }

    public string GetAccessKey()
    {
        return _accessKey;
    }

    public Configurator SetSecretKey(string secretKey)
    {
        _secretKey = secretKey;

        return this;
    }

    public string GetSecretKey()
    {
        return _secretKey;
    }

    public Configurator SetSsl(bool ssl)
    {
        _ssl = ssl;

        return this;
    }

    public bool GetSsl()
    {
        return _ssl;
    }
}