﻿using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.UtilityTools.Standard.Entities.Implements;

namespace EasySoft.Core.AccessWayTransmitter.Entities;

/// <summary>
/// AccessWayExchange
/// </summary>
public class AccessWayExchange : BaseExchange, IAccessWayExchange
{
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
    public int TriggerChannel { get; set; }
}