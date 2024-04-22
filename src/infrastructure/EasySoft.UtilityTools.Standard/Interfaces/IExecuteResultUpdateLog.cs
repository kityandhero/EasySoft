namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// IExecuteResultUpdateLog
/// </summary>
public interface IExecuteResultUpdateLog
{
    /// <summary>
    /// 执行日志标记
    /// </summary>
    string Tag { get; set; }

    /// <summary>
    /// 返回结果的类型
    /// </summary>
    string ResultType { get; set; }

    /// <summary>
    /// 执行结果序列化
    /// </summary>
    string Result { get; set; }

    /// <summary>
    /// 返回时间
    /// </summary>
    DateTime ResultTime { get; set; }
}