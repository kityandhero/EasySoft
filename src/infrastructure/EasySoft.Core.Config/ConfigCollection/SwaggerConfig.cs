namespace EasySoft.Core.Config.ConfigCollection;

public class SwaggerConfig
{
    public static readonly SwaggerConfig Instance = new();

    public string Switch { get; set; }

    public SwaggerConfig()
    {
        Switch = "0";
    }
}