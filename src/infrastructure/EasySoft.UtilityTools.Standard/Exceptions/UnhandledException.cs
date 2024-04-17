using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>   
/// 未处理的异常
/// </summary>
public class UnhandledException : Exception
{
    /// <summary>
    /// 错误描述
    /// </summary>
    public string Description { get; private set; } = "";

    /// <summary>
    /// 附属信息
    /// </summary>
    public string AncillaryInformation { get; private set; } = "";

    /// <summary>未处理的异常</summary>
    /// <param name="message">错误信息</param>
    public UnhandledException(string message) : base(message)
    {
    }

    /// <summary>
    /// 设置描述信息
    /// </summary>
    /// <param name="description">简介描述</param>
    /// <returns></returns>
    public UnhandledException SetDescription(string description)
    {
        Description = description;

        return this;
    }

    /// <summary>
    /// 设置附属信息
    /// </summary>
    /// <param name="ancillaryInformation">附属信息的罗列集合</param>
    /// <returns></returns>
    public UnhandledException SetAncillaryInformation(params string[] ancillaryInformation)
    {
        AncillaryInformation = StringAssist.MergeWithArrow(ancillaryInformation);

        return this;
    }
}