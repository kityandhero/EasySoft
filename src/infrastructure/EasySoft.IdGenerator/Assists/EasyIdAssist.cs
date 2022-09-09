using EasySoft.IdGenerator.Options;
using Yitter.IdGenerator;

namespace EasySoft.IdGenerator.Assists;

public static class EasyIdAssist
{
    static EasyIdAssist()
    {
        var easyOptions = OptionBuilder.CreateEasyOptions();

        YitIdHelper.SetIdGenerator(easyOptions);
    }

    public static long Create()
    {
        var newId = YitIdHelper.NextId();

        return newId;
    }
}