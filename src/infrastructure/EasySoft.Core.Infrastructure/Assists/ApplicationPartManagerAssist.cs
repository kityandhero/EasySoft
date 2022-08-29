using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace EasySoft.Core.Infrastructure.Assists;

public static class ApplicationPartManagerAssist
{
    private static ApplicationPartManager? _applicationPartManager;

    public static void SetApplicationPartManager(ApplicationPartManager? applicationPartManager)
    {
        if (_applicationPartManager != null)
        {
            throw new Exception("applicationPartManager has been set, it disallow set more than once.");
        }

        _applicationPartManager = applicationPartManager;
    }

    public static ApplicationPartManager GetApplicationPartManager()
    {
        if (_applicationPartManager == null)
        {
            throw new Exception("applicationPartManager has not set yet");
        }

        return _applicationPartManager;
    }
}