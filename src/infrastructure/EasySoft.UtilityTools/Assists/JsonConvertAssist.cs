using System;
using Newtonsoft.Json;
using System.Linq;
using EasySoft.UtilityTools.JsonConverters;

namespace EasySoft.UtilityTools.Assists
{
    public static class JsonConvertAssist
    {
        public static JsonSerializerSettings CreateJsonSerializerSettings()
        {
            return CreateJsonSerializerSettings(Array.Empty<JsonConverter>());
        }

        public static JsonSerializerSettings CreateJsonSerializerSettings(params JsonConverter[] converters)
        {
            var converterAdjust = converters.ToList();

            converterAdjust.Add(new LongConverter());

            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                Formatting = Formatting.Indented,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                Converters = converterAdjust
            };
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
            return JsonConvert.SerializeObject(data, CreateJsonSerializerSettings());
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
            return JsonConvert.SerializeObject(data, CreateJsonSerializerSettings(converters));
        }
    }
}