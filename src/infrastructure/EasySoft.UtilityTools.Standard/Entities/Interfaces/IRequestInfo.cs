namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// 请求信息
/// </summary>
public interface IRequestInfo
{
    /// <summary>
    /// Host
    /// </summary>
    [Description("Host")]
    string Host { get; set; }

    /// <summary>
    /// Port
    /// </summary>
    [Description("Port")]
    int Port { get; set; }

    /// <summary>
    /// Url
    /// </summary>
    [Description("Url")]
    string Url { get; set; }

    /// <summary>
    /// UrlParams
    /// </summary>
    [Description("UrlParams")]
    string UrlParams { get; set; }

    /// <summary>
    /// Header
    /// </summary>
    [Description("Header")]
    string Header { get; set; }

    /// <summary>
    /// FormParam
    /// </summary>
    [Description("FormParam")]
    string FormParam { get; set; }

    /// <summary>
    /// PayloadParam
    /// </summary>
    [Description("PayloadParam")]
    string PayloadParam { get; set; }
}