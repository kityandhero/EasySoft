using System;
using System.IO;
using System.Linq;
using EasySoft.UtilityTools.Standard.JsonConverters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// JsonConvertAssist
/// </summary>
public static class JsonConvertAssist
{
    /// <summary>
    /// CreateJsonSerializerSettings
    /// </summary>
    /// <param name="camelCase"></param>
    /// <returns></returns>
    public static JsonSerializerSettings CreateJsonSerializerSettings(bool camelCase = true)
    {
        return CreateJsonSerializerSettings(camelCase, Array.Empty<JsonConverter>());
    }

    /// <summary>
    /// AdjustJsonSerializerSettings
    /// </summary>
    /// <param name="serializerSettings"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static JsonSerializerSettings AdjustJsonSerializerSettings(
        JsonSerializerSettings serializerSettings,
        Action<JsonSerializerSettings>? action = null
    )
    {
        serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        serializerSettings.NullValueHandling = NullValueHandling.Include;
        serializerSettings.Formatting = Formatting.Indented;
        serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        serializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
        serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        action?.Invoke(serializerSettings);

        serializerSettings.Converters.Add(new LongConverter());

        return serializerSettings;
    }

    /// <summary>
    /// CreateJsonSerializerSettings
    /// </summary>
    /// <param name="camelCase"></param>
    /// <param name="converters"></param>
    /// <returns></returns>
    public static JsonSerializerSettings CreateJsonSerializerSettings(
        bool camelCase = true,
        params JsonConverter[] converters
    )
    {
        var serializerSettings = new JsonSerializerSettings();

        return AdjustJsonSerializerSettings(serializerSettings, o =>
        {
            if (converters.Any())
                converters.ToList().ForEach(c => { o.Converters.Add(c); });

            if (!camelCase)
                o.ContractResolver = null;
        });
    }

    /// <summary>
    /// DeserializeObject
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? DeserializeObject<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }

    /// <summary>
    /// Serialize
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Serialize(object data)
    {
        return JsonConvert.SerializeObject(data);
    }

    /// <summary>
    /// SerializeWithFormat
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string SerializeWithFormat(object data)
    {
        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }

    /// <summary>
    /// 序列化
    /// 基于Newtonsoft.Json
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string SerializeAndKeyToLower(object data)
    {
        return JsonConvert.SerializeObject(data, CreateJsonSerializerSettings(true));
    }

    /// <summary>
    /// 序列化
    /// 基于Newtonsoft.Json
    /// </summary>
    /// <param name="data"></param>
    /// <param name="converters"></param>
    /// <returns></returns>
    public static string SerializeAndKeyToLower(object data, params JsonConverter[] converters)
    {
        return JsonConvert.SerializeObject(data, CreateJsonSerializerSettings(true, converters));
    }

    /// <summary>
    /// FormatText
    /// </summary>
    /// <param name="source"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static string FormatText(string source, ILogger? logger = null)
    {
        try
        {
            var serializer = new JsonSerializer();

            TextReader tr = new StringReader(source);

            var jsonTextReader = new JsonTextReader(tr);
            var deserialize = serializer.Deserialize(jsonTextReader);

            if (deserialize == null) return source;

            var textWriter = new StringWriter();
            var jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };

            serializer.Serialize(jsonWriter, deserialize);

            return textWriter.ToString();
        }
        catch (Exception e)
        {
            logger?.LogError(e, "format error: {Message}", e.Message ?? "");

            return source;
        }
    }
}