﻿using System.IO;

namespace UtilityTools.Assists;

public static class PathAssist
{
    public static string Combine(string source, string target)
    {
        return Path.Combine(source, target).Replace("\\", "/");
    }
}