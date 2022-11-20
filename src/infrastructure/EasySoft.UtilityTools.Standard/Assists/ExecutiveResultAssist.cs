using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// ExecutiveResultAssist
/// </summary>
public static class ExecutiveResultAssist
{
    public static ExecutiveResult CreateOk()
    {
        return new ExecutiveResult(ReturnCode.Ok);
    }

    public static ExecutiveResult CreateParamError()
    {
        return new ExecutiveResult(ReturnCode.ParamError);
    }

    public static ExecutiveResult CreateNoData()
    {
        return new ExecutiveResult(ReturnCode.NoData);
    }

    public static ExecutiveResult CreateNoChange()
    {
        return new ExecutiveResult(ReturnCode.NoChange);
    }
}