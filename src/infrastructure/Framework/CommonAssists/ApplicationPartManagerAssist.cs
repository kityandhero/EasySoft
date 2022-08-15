﻿using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Framework.CommonAssists;

public static class ApplicationPartManagerAssist
{
    private static ApplicationPartManager? _manager;

    public static void SetApplicationPartManager(ApplicationPartManager? applicationPartManager)
    {
        _manager = applicationPartManager;
    }

    public static ApplicationPartManager? GetApplicationPartManager()
    {
        return _manager;
    }
}