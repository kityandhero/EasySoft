using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.UtilityTools.Standard.Entities.Implements;

/// <inheritdoc />
public class RequestInfo : IRequestInfo
{
    /// <inheritdoc />
    public string Host { get; set; } = "";

    /// <inheritdoc />
    public int Port { get; set; }

    /// <inheritdoc />
    public string Url { get; set; } = "";

    /// <inheritdoc />
    public string UrlParams { get; set; } = "";

    /// <inheritdoc />
    public string Header { get; set; } = "";

    /// <inheritdoc />
    public string FormParam { get; set; } = "";

    /// <inheritdoc />
    public string PayloadParam { get; set; } = "";
}