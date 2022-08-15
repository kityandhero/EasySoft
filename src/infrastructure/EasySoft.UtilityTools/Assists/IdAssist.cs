using System;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.UtilityTools.Assists
{
    public static class IdAssist
    {
        public static string CreateUUID()
        {
            return Guid.NewGuid().ToString().Remove("-");
        }
    }
}