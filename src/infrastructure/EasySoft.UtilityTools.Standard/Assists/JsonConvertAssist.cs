using System;
using System.IO;
using System.Linq;
using EasySoft.UtilityTools.Standard.JsonConverters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EasySoft.UtilityTools.Standard.Assists;

public static class JsonConvertAssist
{
    public static JsonSerializerSettings CreateJsonSerializerSettings(bool camelCase = true)
    {
        return CreateJsonSerializerSettings(camelCase, Array.Empty<JsonConverter>());
    }

    public static JsonSerializerSettings CreateJsonSerializerSettings(
        bool camelCase = true,
        params JsonConverter[] converters
    )
    {
        var converterAdjust = converters.ToList();

        converterAdjust.Add(new LongConverter());

        var setting = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Include,
            Formatting = Formatting.Indented,
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            Converters = converterAdjust
        };

        if (camelCase)
            setting.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

        return setting;
    }

    public static T? DeserializeObject<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }

    public static string Serialize(object data)
    {
        return JsonConvert.SerializeObject(data);
    }

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