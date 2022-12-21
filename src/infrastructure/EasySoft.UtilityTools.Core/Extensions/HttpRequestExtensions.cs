using EasySoft.UtilityTools.Standard.Entities;
using EasySoft.UtilityTools.Standard.Entities.Implements;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// HttpRequestExtensions
/// </summary>
public static class HttpRequestExtensions
{
    /// <summary>
    /// 获取Url参数并转换为NameValueCollection
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static NameValueCollection GetHeaders(this HttpRequest request)
    {
        return ConvertAssist.DictionaryToNameValueCollection(request.Headers);
    }

    /// <summary>
    /// 获取Url参数并转换为NameValueCollection
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static NameValueCollection GetUrlParams(this HttpRequest request)
    {
        var result = new NameValueCollection();

        if (string.IsNullOrWhiteSpace(request.QueryString.Value ?? "")) return result;

        var url = $"https://www.a.com?{request.QueryString.Value ?? ""}";

        result = url.FormatParamToNameValueCollection();

        return result;
    }

    /// <summary>
    /// 获取From参数并转换为NameValueCollection
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static NameValueCollection GetFromParams(this HttpRequest request)
    {
        var result = new NameValueCollection();

        if (!request.HasFormContentType) return result;

        var f = request.Form;

        foreach (var item in f) result[(string?)item.Key] = item.Value;

        return result;
    }

    /// <summary>
    /// 获取Payload参数并转换为NameValueCollection
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static async Task<NameValueCollection> GetPayloadParams(this HttpRequest request)
    {
        var result = new NameValueCollection();

        var contentType = request.ContentType;

        if (contentType == null || !contentType.Contains(MimeCollection.ApplicationJson.ContentType)) return result;

        //操作Request.Body之前加上EnableBuffering即可
        request.EnableBuffering();

        var stream = request.Body;

        if (stream.Length == 0) return result;

        using var streamReader = new StreamReader(
            stream,
            Encoding.UTF8,
            true,
            1024,
            true
        );

        var json = await streamReader.ReadToEndAsync();

        request.Body.Seek(0, SeekOrigin.Begin);

        result = ConvertAssist.JsonToNameValueCollection(json);

        return result;
    }

    /// <summary>
    /// 获取整合参数并转换为NameValueCollection
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static async Task<NameValueCollection> GetIntegratedParamsAsync(this HttpRequest request)
    {
        var result = GetUrlParams(request);

        result = result.Merge(await GetPayloadParams(request));

        // if(request.ContentType)

        result = result.Merge(GetFromParams(request));

        return result;
    }

    /// <summary>
    /// GetHost
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string GetHost(this HttpRequest request)
    {
        return request.Host.Value;
    }

    /// <summary>
    /// GetUrl
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string GetUrl(this HttpRequest request)
    {
        return request.GetDisplayUrl();
    }

    /// <summary>
    /// GetAbsolutePath
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string GetAbsolutePath(this HttpRequest request)
    {
        return $"{request.PathBase}{request.Path}";
    }

    /// <summary>
    /// BuildRequestInfo
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static IRequestInfo BuildRequestInfo(this HttpRequest request)
    {
        var requestInfo = new RequestInfo
        {
            Host = request.Host.Host,
            Url = request.GetDisplayUrl(),
            UrlParams = request.QueryString.ToString(),
            Header = request.Headers.ToString() ?? "",
            FormParam = !request.HasFormContentType ? "" : request.Form.ToString() ?? "",
            PayloadParam = request.GetPayloadParams().ToString() ?? ""
        };

        return requestInfo;
    }

    /// <summary>
    /// GetCookie
    /// </summary>
    /// <param name="request"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetCookie(this HttpRequest request, string key)
    {
        return request.Cookies[key] ?? "";
    }
}