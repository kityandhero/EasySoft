namespace EasySoft.Core.Config.ConfigCollection;

public class HangfireConfig
{
    public static readonly HangfireConfig Instance = new();

    public string Switch { get; set; }

    public string StorageType { get; set; }

    public string StorageConnection { get; set; }

    public HangfireConfig()
    {
        Switch = "0";
        StorageType = "MemoryStorage";
        StorageConnection = "";
    }
}