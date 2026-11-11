using System.Runtime;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Mime;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// RequestInfoExtensions
/// </summary>
public static class RequestInfoExtensions
{
    /// <summary>
    /// GetUrlParams
    /// </summary>
    /// <param name="requestInfo"></param>
    /// <returns></returns>
    public static NameValueCollection GetUrlParams(this IRequestInfo requestInfo)
    {
        var urlParams = new NameValueCollection();

        if (requestInfo != null && string.IsNullOrWhiteSpace(requestInfo.Url))
        {
            urlParams = requestInfo.Url.FormatParamToNameValueCollection();
        }

        return urlParams;
    }

    /// <summary>
    /// GetHost
    /// </summary>
    /// <param name="requestInfo"></param>
    /// <returns></returns>
    public static string GetHost(this IRequestInfo requestInfo)
    {
        throw new AmbiguousImplementationException();
    }

    /// <summary>
    /// GetPort
    /// </summary>
    /// <param name="requestInfo"></param>
    /// <returns></returns>
    public static int GetPort(this IRequestInfo requestInfo)
    {
        throw new AmbiguousImplementationException();
    }

    /// <summary>
    /// GetHeaders
    /// </summary>
    /// <param name="requestInfo"></param>
    /// <returns></returns>
    public static NameValueCollection GetHeaders(this IRequestInfo requestInfo)
    {
        throw new AmbiguousImplementationException();
    }

    /// <summary>
    /// GetFromParams
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="AmbiguousImplementationException"></exception>
    public static NameValueCollection GetFromParams(this IRequestInfo request)
    {
        throw new AmbiguousImplementationException();
    }

    /// <summary>
    /// GetPayloadParams
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="AmbiguousImplementationException"></exception>
    public static NameValueCollection GetPayloadParams(this IRequestInfo request)
    {
        throw new AmbiguousImplementationException();

        // var payloadParams = new NameValueCollection();
        //
        // if (request != null && request.ContentType.Contains(MimeCollection.Json.ContentType))
        // {
        //     Stream inputStream = request.InputStream;
        //
        //     if (inputStream.Length != 0L)
        //     {
        //         using (var streamReader = new StreamReader(
        //                    inputStream,
        //                    Encoding.UTF8,
        //                    true,
        //                    1024 /*0x0400*/,
        //                    true
        //                ))
        //         {
        //             payloadParams = ConvertCollection.JsonToNameValueCollection(streamReader.ReadToEnd());
        //             request.InputStream.Position = 0L;
        //         }
        //     }
        // }

        // return payloadParams;
    }
}