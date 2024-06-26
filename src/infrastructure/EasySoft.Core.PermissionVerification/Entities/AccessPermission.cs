﻿namespace EasySoft.Core.PermissionVerification.Entities;

/// <summary>
/// AccessPermission
/// </summary>
public class AccessPermission
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>   
    /// Competence
    /// </summary>
    public string Competence { get; set; } = "";

    /// <summary>
    /// GuidTag
    /// </summary>
    public string GuidTag { get; set; } = "";

    /// <summary>
    /// GuidTag
    /// </summary>
    public string Group { get; set; } = "";

    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; set; } = "";

    /// <summary>
    /// Path
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    /// Channel
    /// </summary>
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    /// <summary>
    /// ChannelName
    /// </summary>
    public string ChannelName { get; set; } = "";
}