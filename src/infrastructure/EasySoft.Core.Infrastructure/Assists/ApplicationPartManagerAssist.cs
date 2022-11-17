namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// ApplicationPartManagerAssist
/// </summary>
public static class ApplicationPartManagerAssist
{
    private static ApplicationPartManager? _applicationPartManager;

    /// <summary>
    /// SetApplicationPartManager
    /// </summary>
    /// <param name="applicationPartManager"></param>
    /// <exception cref="Exception"></exception>
    public static void SetApplicationPartManager(ApplicationPartManager? applicationPartManager)
    {
        if (_applicationPartManager != null)
            throw new Exception("applicationPartManager has been set, it disallow set more than once.");

        _applicationPartManager = applicationPartManager;
    }

    /// <summary>
    /// GetApplicationPartManager
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static ApplicationPartManager GetApplicationPartManager()
    {
        if (_applicationPartManager == null) throw new Exception("applicationPartManager has not set yet");

        return _applicationPartManager;
    }
}