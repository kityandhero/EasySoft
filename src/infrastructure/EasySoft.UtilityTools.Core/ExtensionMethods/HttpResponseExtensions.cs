using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Mime;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

/// <summary>
/// HttpResponseExtensions
/// </summary>
public static class HttpResponseExtensions
{
    /// <summary>
    /// Write the specified value as JSON to the response body. The response content-type will be set to
    /// the specified content-type.
    /// </summary>
    /// <typeparam name="TValue">The type of object to write.</typeparam>
    /// <param name="response">The response to write JSON to.</param>
    /// <param name="value">The value to write as JSON.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public static async Task WriteTypeAsJsonAsync<TValue>(
        this HttpResponse response,
        TValue value
    )
    {
        if (response == null) throw new ArgumentNullException(nameof(response));

        response.ContentType = MimeCollection.Json.ContentType;

        if (value != null)
        {
            await using var streamWriter = new StreamWriter(response.Body);

            await streamWriter.WriteAsync(JsonConvertAssist.Serialize(value));
        }
    }

    /// <summary>
    /// WriteObjectAsJsonAsync
    /// </summary>
    /// <param name="response"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task WriteObjectAsJsonAsync(
        this HttpResponse response,
        object value
    )
    {
        if (response == null) throw new ArgumentNullException(nameof(response));

        response.ContentType = MimeCollection.Json.ContentType;

        await using var streamWriter = new StreamWriter(response.Body);

        await streamWriter.WriteAsync(JsonConvertAssist.SerializeAndKeyToLower(value));
    }

    /// <summary>
    /// SetCookie
    /// </summary>
    /// <param name="response"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetCookie(this HttpResponse response, string key, string value)
    {
        response.SetCookie(key, value, new CookieOptions());
    }

    /// <summary>
    /// SetCookie
    /// </summary>
    /// <param name="response"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public static void SetCookie(this HttpResponse response, string key, string value, CookieOptions options)
    {
        response.Cookies.Append(key, value, options);
    }
}