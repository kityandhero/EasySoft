using System;
using Newtonsoft.Json;
using UtilityTools.ExtensionMethods;

namespace UtilityTools.JsonConverters
{
    public class LongConverter : JsonConverter
    {
        /// <summary>
        /// WriteJson
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var valueAdjust = value ?? new object();

            var typeCode = Type.GetTypeCode(valueAdjust.GetType());

            if (typeCode == TypeCode.Int64)
            {
                var v = Convert.ToInt64(valueAdjust);

                if (v is > int.MaxValue or < int.MinValue)
                {
                    writer.WriteValue(valueAdjust.ToString());

                    return;
                }
            }
            else if (typeCode == TypeCode.UInt64)
            {
                var v = Convert.ToUInt64(valueAdjust);

                if (v > uint.MaxValue)
                {
                    writer.WriteValue(valueAdjust.ToString());

                    return;
                }
            }

            writer.WriteValue(valueAdjust);
        }

        /// <summary>
        /// ReadJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer
        )
        {
            var v = (reader.Value as string).ToLong();

            return v;
        }

        /// <summary>
        /// CanConvert
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            // 只处理long和ulong两种类型的数据
            switch (objectType.FullName)
            {
                case "System.Int64":
                    return true;

                case "System.UInt64":
                    return true;

                default:
                    return false;
            }
        }
    }
}