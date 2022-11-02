using EasySoft.IdGenerator.Options;
using Yitter.IdGenerator;

namespace EasySoft.IdGenerator.Assists;

public static class IdentifierAssist
{
    static IdentifierAssist()
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