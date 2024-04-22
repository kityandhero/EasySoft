using EasySoft.Core.PermissionServer.Core.Entities.Bases;

namespace EasySoft.Core.PermissionServer.Core.Entities;

/// <summary>
/// 访问模块
/// </summary>
[Description("访问模块")]
public class AccessWay : BaseEntity, IAccessWayStore
{
    #region Properties

    /// <inheritdoc />  
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public string GuidTag { get; set; } = "";

    /// <inheritdoc />
    public string RelativePath { get; set; } = "";

    /// <inheritdoc />
    public int RelativePathLevel { get; set; }

    /// <inheritdoc />
    public string RelativeParentPath { get; set; } = "";

    /// <inheritdoc />
    public int RelativeParentPathLevel { get; set; }

    /// <inheritdoc />
    public string Expand { get; set; } = "";

    /// <inheritdoc />
    public string ResultType { get; set; } = "";

    /// <inheritdoc />
    public string Group { get; set; } = "";

    /// <inheritdoc />
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public int Status { get; set; }

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public long ModifyBy { get; set; }

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public string TriggerChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    #endregion

    #region Methods

    /// <inheritdoc />
    public string GetId()
    {
        return Id.ToString();
    }

    /// <inheritdoc />
    public string GetIdentificationName()
    {
        return ReflectionAssist.GetPropertyName(() => Id);
    }

    #endregion
}