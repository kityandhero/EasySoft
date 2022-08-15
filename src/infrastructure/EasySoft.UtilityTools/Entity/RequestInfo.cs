namespace EasySoft.UtilityTools.Entity;

public class RequestInfo
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string Url { get; set; }

    public string UrlParams { get; set; }

    public string Header { get; set; }

    public string FormParam { get; set; }

    public string PayloadParam { get; set; }

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