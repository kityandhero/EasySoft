namespace EasySoft.UtilityTools.Core.Interfaces;

/// <summary>
/// IServiceInfo
/// </summary>
public interface IServiceInfo
{
    /// <summary>
    /// xxx-webapi-188933
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// xxx-webapi
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// corsPolicy
    /// </summary>
    public string CorsPolicy { get; set; }

    /// <summary>
    ///  usr or cus or xxx
    /// </summary>
    public string ShortName { get; }

    /// <summary>
    /// Version
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// description
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// assembly  of start's project
    /// </summary>
    public Assembly StartAssembly { get; }
}