namespace EasySoft.Core.MinIO.configures;

public interface IConfigurator
{
    public Configurator SetEndpoint(string endpoint);

    public string GetEndpoint();

    public Configurator SetAccessKey(string accessKey);

    public string GetAccessKey();

    public Configurator SetSecretKey(string secretKey);

    public string GetSecretKey();

    public Configurator SetSsl(bool ssl);

    public bool GetSsl();
}