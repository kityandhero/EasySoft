using EasySoft.Core.Mvc.Framework.ConfigInterface;

namespace EasySoft.Core.Mvc.Framework.ConfigCollection;

public class PayCallbackConfig : IConfig
{
    public static readonly PayCallbackConfig Instance = new();

    public string CallbackHost { get; set; }

    public PayCallbackConfig()
    {
        CallbackHost = "";
    }
}