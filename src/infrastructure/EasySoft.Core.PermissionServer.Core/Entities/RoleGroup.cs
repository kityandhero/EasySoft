﻿using EasySoft.Core.PermissionServer.Core.Entities.Bases;

namespace EasySoft.Core.PermissionServer.Core.Entities;

/// <summary>
/// 角色组
/// </summary>
[Description("角色组")]
public class RoleGroup : BaseEntity, IChannelStore, IStatus, IIp, IOperate
{
    /// <summary>
    /// 角色组名
    /// </summary>
    [Description("角色组名")]
    public string Name { get; set; } = "";

    /// <summary>
    /// 自定义角色集合
    /// </summary>
    [Description("自定义角色集合")]
    public string CustomRoleCollection { get; set; } = "";

    /// <summary>
    /// 预设角色集合
    /// </summary>
    [Description("预设角色集合")]
    public string PresetRoleCollection { get; set; } = "";

    /// <summary>
    /// 超级管理角色组
    /// </summary>
    [Description("超级管理角色组")]
    public int WhetherSuper { get; set; } = Whether.No.ToInt();

    /// <inheritdoc />
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <inheritdoc />
    public int Status { get; set; }

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public long ModifyBy { get; set; }

    /// <inheritdoc />
    public DateTime ModifyTime { get; set; } = DateTimeOffset.Now.DateTime;
}