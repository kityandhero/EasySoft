using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.Mime;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace EasySoft.UtilityTools.ExtensionMethods;

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
    public static Task WriteAsNewtonsoftJsonAsync<TValue>(
        this HttpResponse response,
        TValue value
    )
    {
        if (response == null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        response.ContentType = MimeCollection.Json.ContentType;

        if (value != null)
        {
            using var streamWriter = new StreamWriter(response.Body);

            return streamWriter.WriteAsync(JsonConvertAssist.Serialize(value));
        }
        else
        {
            return Task.CompletedTask;
        }
    }
}