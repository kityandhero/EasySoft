using System.Collections.Specialized;
using System.IO;
using System.Text;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Entity;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

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

        if (string.IsNullOrWhiteSpace(request.QueryString.Value ?? ""))
        {
            return result;
        }

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

        if (!request.HasFormContentType)
        {
            return result;
        }

        var f = request.Form;

        foreach (var item in f)
        {
            result[(string?)item.Key] = item.Value;
        }

        return result;
    }

    /// <summary>
    /// 获取Payload参数并转换为NameValueCollection
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static NameValueCollection GetPayloadParams(this HttpRequest request)
    {
        var result = new NameValueCollection();

        var contentType = request.ContentType;

        if (contentType == null || !contentType.Contains(MimeCollection.Json.ContentType))
        {
            return result;
        }

        var stream = request.Body;

        if (stream.Length == 0)
        {
            return result;
        }

        using (var streamReader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
        {
            var json = streamReader.ReadToEnd();
            result = ConvertAssist.JsonToNameValueCollection(json);

            request.Body.Position = 0;
        }

        return result;
    }

    /// <summary>
    /// 获取整合参数并转换为NameValueCollection
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static NameValueCollection GetIntegratedParams(this HttpRequest request)
    {
        var result = GetUrlParams(request);

        result = result.Merge(GetPayloadParams(request));

        // if(request.ContentType)

        result = result.Merge(GetFromParams(request));

        return result;
    }

    public static string GetHost(this HttpRequest request)
    {
        return request.Host.Value;
    }

    public static string GetUrl(this HttpRequest request)
    {
        return request.GetDisplayUrl();
    }

    public static string GetAbsolutePath(this HttpRequest request)
    {
        return $"{request.PathBase}{request.Path}";
    }

    public static RequestInfo BuildRequestInfo(this HttpRequest request)
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

    public static string GetCookie(this HttpRequest request, string key)
    {
        return request.Cookies[key] ?? "";
    }
}