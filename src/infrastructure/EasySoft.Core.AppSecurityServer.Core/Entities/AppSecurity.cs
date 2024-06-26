﻿using EasySoft.Core.AppSecurityServer.Core.Entities.Bases;
using EasySoft.Core.AppSecurityServer.Core.Interfaces;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Entities;

/// <summary>
/// 应用安全
/// </summary>
[Description("应用安全")]
public class AppSecurity : BaseEntity, IAppSecurity, ISuperRoleMaintain, IChannelStore, IIp, IOperate
{
    /// <inheritdoc />
    public string AppId { get; set; } = UniqueIdAssist.CreateUUID();

    /// <inheritdoc />
    public string AppSecret { get; set; } = UniqueIdAssist.CreateUUID().ToMd5();

    /// <inheritdoc />
    public DateTime SuperRoleRecentlyMaintainTime { get; set; } = ConstCollection.DbDefaultDateTime;

    /// <inheritdoc />
    public DateTime SuperRoleNextMaintainTime { get; set; } = ConstCollection.DbDefaultDateTime;

    /// <inheritdoc />
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

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