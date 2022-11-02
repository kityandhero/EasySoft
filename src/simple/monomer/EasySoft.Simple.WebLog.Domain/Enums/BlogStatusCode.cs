using System.ComponentModel;

namespace EasySoft.Simple.WebLog.Domain.Enums;

public enum BlogStatusCode
{
    /// <summary>
    /// 下线
    /// </summary>
    [Description("下线")]
    Offline = 100,

    /// <summary>
    /// 上线
    /// </summary>
    [Description("上线")]
    Online = 200
}