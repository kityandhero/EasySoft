using System.Collections.Specialized;
using System.Text;
using Microsoft.AspNetCore.Http;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.ExtensionMethods;
using EasySoft.UtilityTools.Mime;

namespace EasySoft.Core.Mvc.Framework.ExtensionMethods
{
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
                result[item.Key] = item.Value;
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
    }
}