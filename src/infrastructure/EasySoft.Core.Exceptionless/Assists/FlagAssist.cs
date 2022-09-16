namespace EasySoft.Core.Exceptionless.Assists;

internal class FlagAssist
{
    private static bool _advanceExceptionlessInitializeComplete;

    public static void SetAdvanceExceptionlessInitializeCompleted()
    {
        _advanceExceptionlessInitializeComplete = true;
    }

    /// <summary>
    /// 获取应用是否已经 running
    /// </summary>
    /// <returns></returns>
    public static bool GetAdvanceExceptionlessInitializeWhetherCompleted()
    {
        return _advanceExceptionlessInitializeComplete;
    }
}