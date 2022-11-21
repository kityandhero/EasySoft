namespace EasySoft.UtilityTools.Standard.Entity;

/// <summary>
/// RequestInfo
/// </summary>
public class RequestInfo
{
    /// <summary>
    /// Host
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Port
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// UrlParams
    /// </summary>
    public string UrlParams { get; set; }

    /// <summary>
    /// Header
    /// </summary>
    public string Header { get; set; }

    /// <summary>
    /// FormParam
    /// </summary>
    public string FormParam { get; set; }

    /// <summary>
    /// PayloadParam
    /// </summary>
    public string PayloadParam { get; set; }

    /// <summary>
    /// RequestInfo
    /// </summary>
    public RequestInfo()
    {
        Host = "";
        Url = "";
        UrlParams = "";
        Header = "";
        FormParam = "";
        PayloadParam = "";
    }
}