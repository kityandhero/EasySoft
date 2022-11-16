namespace EasySoft.UtilityTools.Core.ConfigurationSources;

/// <summary>
/// JsonContentConfigurationSourceCore
/// </summary>
public abstract class JsonContentConfigurationSourceCore : IConfigurationSource
{
    /// <summary>
    /// JsonContentConfigurationSourceCore
    /// </summary>
    protected JsonContentConfigurationSourceCore()
    {
        ReloadOnChange = false;
        ReloadDelay = 100;
    }

    /// <summary>
    /// Determines whether the source will be loaded if the underlying file changes.
    /// </summary>
    public bool ReloadOnChange { get; set; }

    /// <summary>
    /// Number of milliseconds that reload will wait before calling Load.  This helps
    /// avoid triggering reload before a file is completely written. Default is 250.
    /// </summary>
    public int ReloadDelay { get; set; }

    /// <summary>
    /// Build
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public abstract IConfigurationProvider Build(IConfigurationBuilder builder);
}