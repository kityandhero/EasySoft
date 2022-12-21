using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// ExecutiveResultAssist
/// </summary>
public static class ExecutiveResultAssist
{
    /// <summary>
    /// CreateOk
    /// </summary>
    /// <returns></returns>
    public static ExecutiveResult CreateOk()
    {
        return new ExecutiveResult(ReturnCode.Ok);
    }

    /// <summary>
    /// CreateParamError
    /// </summary>
    /// <returns></returns>
    public static ExecutiveResult CreateParamError()
    {
        return new ExecutiveResult(ReturnCode.ParamError);
    }

    /// <summary>
    /// CreateNoData
    /// </summary>
    /// <returns></returns>
    public static ExecutiveResult CreateNoData()
    {
        return new ExecutiveResult(ReturnCode.NoData);
    }

    /// <summary>
    /// CreateNoChange
    /// </summary>
    /// <returns></returns>
    public static ExecutiveResult CreateNoChange()
    {
        return new ExecutiveResult(ReturnCode.NoChange);
    }
}