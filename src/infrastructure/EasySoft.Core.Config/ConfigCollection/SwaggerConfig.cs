namespace EasySoft.Core.Config.ConfigCollection;

public class SwaggerConfig
{
    public static readonly SwaggerConfig Instance = new();

    public string Enable { get; set; }

    public SwaggerConfig()
    {
        Enable = "0";
    }
}