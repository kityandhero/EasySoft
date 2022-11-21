using System.Net.Http;
using System.Threading.Tasks;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

/// <summary>
/// HttpClientExtensions
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// PostAsync
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="url"></param>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static Task<HttpResponseMessage> PostAsync(
        this HttpClient httpClient,
        string url,
        IDictionary<string, object> dictionary
    )
    {
        var content = JsonConvertAssist.Serialize(dictionary);

        var stringContent = new StringContent(content, Encoding.UTF8);

        return httpClient.PostAsync(url, stringContent);
    }
}