using System;
using UtilityTools.ExtensionMethods;

namespace UtilityTools.Assists
{
    public static class IdAssist
    {
        public static string CreateUUID()
        {
            return Guid.NewGuid().ToString().Remove("-");
        }
    }
}