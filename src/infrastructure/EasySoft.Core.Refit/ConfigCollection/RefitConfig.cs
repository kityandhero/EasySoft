namespace EasySoft.Core.Refit.ConfigCollection;

/// <summary>
/// SwaggerConfig
/// </summary>
public class RefitConfig
{
    /// <summary>
    /// Instance
    /// </summary>
    public static readonly RefitConfig Instance = new();

    /// <summary>
    /// Switch
    /// </summary>
    public string Switch { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// ContactName
    /// </summary>
    public string ContactName { get; set; }

    /// <summary>
    /// ContactUrl
    /// </summary>
    public string ContactUrl { get; set; }

    /// <summary>
    /// ContactEmail
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    /// LicenseName
    /// </summary>
    public string LicenseName { get; set; }

    /// <summary>
    /// LicenseUrl
    /// </summary>
    public string LicenseUrl { get; set; }

    /// <summary>
    /// OpenApiServerUrl
    /// </summary>
    public string OpenApiServerUrl { get; set; }

    /// <summary>
    /// OpenApiServerDescription
    /// </summary>
    public string OpenApiServerDescription { get; set; }

    /// <summary>
    /// SwaggerConfig
    /// </summary>
    public RefitConfig()
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