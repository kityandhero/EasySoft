using EasySoft.Core.Config.ConfigCollection;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.Pretreatments;

public static class PrepareStartAssist
{
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
            typeof(GeneralConfig)
        };

        configureFileNameList.ForEach(item =>
        {
            var itemPath = $"{directory}\\configures\\{item.Name.ToLowerFirst()}.json";

            itemPath.CreateFile(JsonConvertAssist.SerializeAndKeyToLower(new { }));
        });
    }
}