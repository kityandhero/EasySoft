using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace EasySoft.Core.Infrastructure.Assists;

public static class ApplicationPartManagerAssist
{
    private static ApplicationPartManager? _manager;

    public static void SetApplicationPartManager(ApplicationPartManager? applicationPartManager)
    {
        _manager = applicationPartManager;
    }

    public static ApplicationPartManager GetApplicationPartManager()
    {
        if (_manager == null)
        {
            throw new Exception("applicationPartManager has not set yet");
        }

        return _manager;
    }
}