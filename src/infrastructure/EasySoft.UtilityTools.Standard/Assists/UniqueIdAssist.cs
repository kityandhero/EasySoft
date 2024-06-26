﻿using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// UniqueIdAssist
/// </summary>
public static class UniqueIdAssist
{
    /// <summary>
    /// CreateUUID
    /// </summary>
    /// <returns></returns>
    public static string CreateUUID()
    {
        return Guid.NewGuid().ToString().Remove("-");
    }
}