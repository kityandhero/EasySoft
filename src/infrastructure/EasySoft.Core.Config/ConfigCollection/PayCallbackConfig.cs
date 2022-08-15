using EasySoft.Core.Config.ConfigInterface;

namespace EasySoft.Core.Config.ConfigCollection;

public class PayCallbackConfig : IConfig
{
    public static readonly PayCallbackConfig Instance = new();

    public string CallbackHost { get; set; }

    public PayCallbackConfig()
    {
        CallbackHost = "";
    }
}