using Microsoft.Extensions.Configuration;

namespace EasySoft.UtilityTools.Core.ConfigurationSources;

public abstract class JsonContentConfigurationSourceCore : IConfigurationSource
{
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

    public abstract IConfigurationProvider Build(IConfigurationBuilder builder);
}