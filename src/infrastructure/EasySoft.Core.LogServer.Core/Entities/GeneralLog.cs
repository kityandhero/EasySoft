﻿using EasySoft.Core.LogServer.Core.Entities.Bases;

namespace EasySoft.Core.LogServer.Core.Entities;

/// <summary>
/// 一般日志
/// </summary>
public class GeneralLog : BaseEntity, IGeneralLogStore
{
    #region Properties

    /// <inheritdoc />
    public string Message { get; set; } = "";

    /// <inheritdoc />
    public int MessageType { get; set; } = CustomValueType.PlainValue.ToInt();

    /// <inheritdoc />
    public string Description { get; set; } = "";

    /// <inheritdoc />
    public string AncillaryInformation { get; set; } = "";

    /// <inheritdoc />
    public string Content { get; set; } = "";

    /// <inheritdoc />
    public int ContentType { get; set; } = CustomValueType.PlainValue.ToInt();

    /// <inheritdoc />
    public int Type { get; set; } = GeneralLogType.Common.ToInt();

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
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

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