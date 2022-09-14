using AgileConfig.Client;

namespace EasySoft.Core.AgileConfigClient.Assists;

public static class AgileConfigClientAssist
{
    private static IConfigClient? _configClient;

    public static void SetConfigClient(IConfigClient configClient)
    {
        _configClient = configClient;
    }

    public static IConfigClient GetConfigClient()
    {
        if (_configClient == null)
        {
            throw new Exception("configClient has not set yet");
        }

        return _configClient;
    }
}