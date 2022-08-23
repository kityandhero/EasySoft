﻿using System;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Standard.Assists
{
    public static class IdAssist
    {
        public static string CreateUUID()
        {
            return Guid.NewGuid().ToString().Remove("-");
        }
    }
}