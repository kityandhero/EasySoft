using EasySoft.Core.AppSecurityServer.Core.Entities.Bases;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Entities;

/// <summary>
/// 应用校验公钥
/// </summary>
public class AppPublicKey : BaseEntity, IAppPublicKey, ICreateTime, IModifyTime
{
    /// <inheritdoc />
    public string Key { get; set; } = "";

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; } = DateTimeOffset.Now.DateTime;
}