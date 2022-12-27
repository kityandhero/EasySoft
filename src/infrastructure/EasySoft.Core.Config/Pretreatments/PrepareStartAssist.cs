using EasySoft.Core.Config.ConfigCollection;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Media.Image;

namespace EasySoft.Core.Config.Pretreatments;

/// <summary>
/// PrepareStartAssist
/// </summary>
public static class PrepareStartAssist
{
    /// <summary>
    /// ToWork
    /// </summary>
    public static void ToWork()
    {
        CheckConfigures();
    }

    private static void CheckConfigures()
    {
        var directory = AppContextAssist.GetBaseDirectory();

        var configureFolderPath = $"{directory}/configures/";

        configureFolderPath.CreateDirectory();

        var configureFileNameList = new List<Type>
        {
            // typeof(SuperConfig),
            typeof(GeneralConfig)
        };

        configureFileNameList.ForEach(item =>
        {
            var itemPath = $"{directory}\\configures\\{item.Name.ToLowerFirst()}.json";

            if (item.FullName == typeof(SuperConfig).FullName)
                itemPath.CreateFile(JsonConvertAssist.SerializeAndKeyToLower(new SuperConfig
                {
                    Password = new Captcha().SetLetterCount(8).GetRandomString()
                }));
            else
                itemPath.CreateFile(JsonConvertAssist.SerializeAndKeyToLower(new { }));
        });
    }
}