using Newtonsoft.Json;
using System.Collections.Generic;
using UtilityTools.JsonConverters;

namespace UtilityTools.Assists
{
    public static class JsonConvertAssist
    {
        public static JsonSerializerSettings CreateJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new LongConverter()
                }
            };
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
            var setting = CreateJsonSerializerSettings();

            return JsonConvert.SerializeObject(data, setting);
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
            var setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                Converters = converters
            };

            return JsonConvert.SerializeObject(data, setting);
        }
    }
}