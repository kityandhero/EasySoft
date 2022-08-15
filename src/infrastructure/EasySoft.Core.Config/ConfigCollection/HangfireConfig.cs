namespace EasySoft.Core.Config.ConfigCollection;

public class HangfireConfig
{
    public static readonly HangfireConfig Instance = new();

    public string Enable { get; set; }

    public string Storage { get; set; }

    public HangfireConfig()
    {
        Enable = "0";
        Storage = "MemoryStorage";
    }
}