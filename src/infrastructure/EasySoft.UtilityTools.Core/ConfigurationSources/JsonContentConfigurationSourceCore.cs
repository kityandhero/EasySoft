using EasySoft.UtilityTools.Core.Providers;
using Microsoft.Extensions.Configuration;

namespace EasySoft.UtilityTools.Core.ConfigurationSources;

public abstract class JsonContentConfigurationSourceCore : IConfigurationSource
{
    /// <summary>
    /// Used to access the contents of the file.
    /// </summary>
    public IContentProvider? ContentProvider { get; set; }

    /// <summary>
    /// Determines whether the source will be loaded if the underlying file changes.
    /// </summary>
    public bool ReloadOnChange { get; set; }

    /// <summary>
    /// Number of milliseconds that reload will wait before calling Load.  This helps
    /// avoid triggering reload before a file is completely written. Default is 250.
    /// </summary>
    public int ReloadDelay { get; set; } = 250;

    public abstract IConfigurationProvider Build(IConfigurationBuilder builder);
}