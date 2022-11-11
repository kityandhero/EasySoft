using EasySoft.Core.Hangfire.ConfigAssist;
using EasySoft.Core.Hangfire.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Hangfire.Assists;

public static class HangfireAssist
{
    public static StorageType GetStorageType()
    {
        var storage = HangfireConfigAssist.GetStorageType();

        switch (storage)
        {
            case "MemoryStorage":
                return StorageType.MemoryStorage;

            case "SqlServer":
                return StorageType.SqlServer;

            default:
                throw new UnsupportedException(
                    $"hangfire storage {storage} has not support, available storage is {GetAllAvailableStorageType()}"
                );
        }
    }

    internal static string GetAllAvailableStorageType()
    {
        return EnumAssist.GetNameList<StorageType>().Join("/");
    }
}