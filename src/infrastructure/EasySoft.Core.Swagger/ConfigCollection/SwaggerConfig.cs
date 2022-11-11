namespace EasySoft.Core.Swagger.ConfigCollection;

public class SwaggerConfig
{
    public static readonly SwaggerConfig Instance = new();

    public string Switch { get; set; }

    public string Title { get; set; }

    public string Version { get; set; }

    public string Description { get; set; }

    public string ContactName { get; set; }

    public string ContactUrl { get; set; }

    public string ContactEmail { get; set; }

    public string LicenseName { get; set; }

    public string LicenseUrl { get; set; }

    public string OpenApiServerUrl { get; set; }

    public string OpenApiServerDescription { get; set; }

    public SwaggerConfig()
    {
        Switch = "0";

        Title = "";
        Description = "";
        Version = "1.0";

        ContactName = "";
        ContactUrl = "";
        ContactEmail = "";

        LicenseName = "";
        LicenseUrl = "";

        OpenApiServerUrl = "";
        OpenApiServerDescription = "";
    }
}