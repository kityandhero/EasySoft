﻿namespace EasySoft.Core.Mvc.Framework.CommonAssists;

internal static class FlagAssist
{
    internal static bool TokenSecretOptionIsDefault { get; set; }
    internal static bool TokenSecretOptionInjectionComplete { get; set; }

    static FlagAssist()
    {
        TokenSecretOptionIsDefault = false;
        TokenSecretOptionInjectionComplete = false;
    }
}