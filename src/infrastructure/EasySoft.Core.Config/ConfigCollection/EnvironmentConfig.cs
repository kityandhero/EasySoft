namespace EasySoft.Core.Config.ConfigCollection;

public class EnvironmentConfig
{
    public static readonly EnvironmentConfig Instance = new();

    public string CustomEnv { get; set; }

    public EnvironmentConfig()
    {
        CustomEnv = "";
    }
}