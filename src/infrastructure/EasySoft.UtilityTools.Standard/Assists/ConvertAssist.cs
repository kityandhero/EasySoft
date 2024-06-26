﻿using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// ConvertAssist
/// </summary>
public static class ConvertAssist
{
    private static readonly char[] MetaDigit =
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
        'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };

    /// <summary>
    /// 将指定基数的数字的 System.String 表示形式转换为等效的 64 位有符号整数。
    /// </summary>
    /// <param name="value">包含数字的 System.String。</param>
    /// <param name="digit">value 中数字的基数，它必须是[2,36]</param>
    /// <returns>等效于 value 中的数字的 64 位有符号整数。- 或 - 如果 value 为 null，则为零。</returns>
    private static long X2H(string value, int digit)
    {
        value = value.Trim();
        if (string.IsNullOrEmpty(value)) return 0L;

        var sDigits = new string(MetaDigit, 0, digit);
        long result = 0;

        value = value.ToUpper();

        for (var i = 0; i < value.Length; i++)
        {
            if (!sDigits.Contains(value[i].ToString()))
                throw new ArgumentException(
                    $"The argument \"{value[i]}\" is not in {digit} system."
                );

            try
            {
                result += (long)Math.Pow(digit, i) *
                          GetCharIndex(MetaDigit, value[value.Length - i - 1]);
            }
            catch
            {
                throw new OverflowException("运算溢出.");
            }
        }

        return result;
    }

    /// <summary>
    /// 十六进制字符串转换为Byte[]
    /// </summary>
    /// <param name="hexString"></param>
    /// <returns></returns>
    public static byte[] HexToByte(string hexString)
    {
        var returnBytes = new byte[hexString.Length / 2];

        for (var i = 0; i < returnBytes.Length; i++) returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

        return returnBytes;
    }

    private static int GetCharIndex(char[] arr, char value)
    {
        for (var i = 0; i < arr.Length; i++)
            if (arr[i] == value)
                return i;

        return 0;
    }

    /// <summary>
    /// long转化指定位数进制的字符串表示
    /// </summary>
    /// <param name="value">要转换的数据</param>
    /// <param name="digit">进制</param>
    /// <returns></returns>
    private static string LongToTargetDigit(long value, int digit)
    {
        int digitIndex;
        var longPositive = Math.Abs(value);
        var radix = digit;
        var outDigits = new char[63];

        for (digitIndex = 0; digitIndex <= 64; digitIndex++)
        {
            if (longPositive == 0) break;

            outDigits[outDigits.Length - digitIndex - 1] =
                MetaDigit[longPositive % radix];
            longPositive /= radix;
        }

        return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
    }

    /// <summary>
    /// 任意进制转换互相转换
    /// </summary>
    /// <param name="value">转换目标</param>
    /// <param name="fromDigit"></param>
    /// <param name="toDigit"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string DigitToDigit(string value, int fromDigit, int toDigit)
    {
        if (string.IsNullOrEmpty(value.Trim())) return string.Empty;

        if (fromDigit < 2 || fromDigit > 36)
            throw new ArgumentException(
                $"The fromBase radix \"{fromDigit}\" is not in the range 2..36."
            );

        if (toDigit < 2 || toDigit > 36)
            throw new ArgumentException(
                $"The toBase radix \"{toDigit}\" is not in the range 2..36."
            );

        var m = X2H(value, fromDigit);
        var r = LongToTargetDigit(m, toDigit);
        return r;
    }

    /// <summary>
    /// LongToThirtySixDigit
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string LongToThirtySixDigit(long value)
    {
        return DigitToDigit(value.ToString(), 10, 36);
    }

    /// <summary>
    /// ThirtySixDigitToLong
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static long ThirtySixDigitToLong(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new Exception("空字符串不能参与进制转换");

        return Convert.ToInt64(DigitToDigit(value, 36, 10));
    }

    /// <summary>
    /// ImageToBase64
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string ImageToBase64(Image file)
    {
        using (var memoryStream = new MemoryStream())
        {
            file.SaveAsPng(memoryStream);
            var imageBytes = memoryStream.ToArray();

            return Convert.ToBase64String(imageBytes);
        }
    }

    /// <summary>
    /// StringToBase64QRCodeImage
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string StringToBase64QRCodeImage(string v)
    {
        return QrCodeAssist.GetRCode(v);
    }

    #region 将时间转换成UNIX时间戳

    /// <summary>
    /// 获取时间戳（13位 毫秒）
    /// </summary>
    /// <returns></returns>
    public static long GetTimeStamp(DateTime dt)
    {
        var ts = dt.ToUniversalTime() - new DateTime(
            1970,
            1,
            1,
            0,
            0,
            0,
            DateTimeKind.Utc
        );

        return Convert.ToInt64(ts.TotalSeconds * 1000);
    }

    /// <summary>
    /// 将时间转换成UNIX时间戳
    /// </summary>
    /// <param name="dt">时间</param>
    /// <returns>UNIX时间戳</returns>
    public static long ConvertToUnixTime(DateTime dt)
    {
        return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
    }

    #endregion 将时间转换成UNIX时间戳

    /// <summary>
    /// StringTo
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static ExecutiveResult<T> StringTo<T>(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return new ExecutiveResult<T>(ReturnCode.NoData);

        var typeCode = Type.GetTypeCode(typeof(T));

        switch (typeCode)
        {
            case TypeCode.Boolean:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        value.Equals("true", StringComparison.InvariantCultureIgnoreCase),
                        typeof(T)
                    )
                };

            case TypeCode.Int64:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToInt64(value),
                        typeof(T)
                    )
                };

            case TypeCode.UInt64:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToUInt64(value),
                        typeof(T)
                    )
                };

            case TypeCode.DateTime:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToDateTime(value),
                        typeof(T)
                    )
                };

            case TypeCode.Decimal:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToDecimal(value),
                        typeof(T)
                    )
                };

            case TypeCode.Double:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToDouble(value),
                        typeof(T)
                    )
                };

            case TypeCode.Int16:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToInt16(value),
                        typeof(T)
                    )
                };

            case TypeCode.Int32:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToInt32(value),
                        typeof(T)
                    )
                };

            case TypeCode.Single:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToSingle(value),
                        typeof(T)
                    )
                };

            case TypeCode.String:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToString(value),
                        typeof(T)
                    )
                };

            case TypeCode.UInt16:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToUInt16(value),
                        typeof(T)
                    )
                };

            case TypeCode.UInt32:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = (T)Convert.ChangeType(
                        Convert.ToUInt32(value),
                        typeof(T)
                    )
                };

            case TypeCode.Object:
                return new ExecutiveResult<T>(ReturnCode.Ok)
                {
                    Data = JsonConvertAssist.DeserializeObject<T>(value)
                };

            default:
                throw new Exception("type has not yet supported");
        }
    }

    /// <summary>
    /// ObjectTo
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? ObjectTo<T>(object? value, object? defaultValue = null)
    {
        defaultValue ??= default(T);

        var result = defaultValue != null ? (T)defaultValue : default;

        var convertType = typeof(T);

        if (convertType.IsGenericType &&
            convertType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            if (value == null || value.ToString()!.Trim().Length == 0) return default;

            var nullableConverter = new NullableConverter(convertType);

            convertType = nullableConverter.UnderlyingType;

            try
            {
                result = (T)Convert.ChangeType(value, convertType);

                return result;
            }
            catch (Exception)
            {
                return default;
            }
        }

        if (value != null) result = (T)Convert.ChangeType(value, typeof(T));

        return result;
    }

    #region 时间转与UNIX时间戳转换

    #region 将UNIX时间戳转换成时间

    /// <summary>
    /// 将UNIX时间戳转换成协调世界时（UTC）
    /// </summary>
    /// <param name="unixTime">UNIX时间戳</param>
    /// <returns>时间</returns>
    public static DateTime UnixTimeToUniversalTime(long unixTime)
    {
        var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var date = start.AddSeconds(unixTime).ToUniversalTime();

        return date;
    }

    /// <summary>
    /// 将UNIX时间戳转换成本地时间
    /// </summary>
    /// <param name="unixTime">UNIX时间戳</param>
    /// <returns>时间</returns>
    public static DateTime UnixTimeToLocalTime(long unixTime)
    {
        var t = UnixTimeToUniversalTime(unixTime);

        return t.ToLocalTime();
    }

    /// <summary>
    /// 将UNIX时间戳转换成协调世界时（UTC）
    /// </summary>
    /// <param name="unixTimeString">UNIX时间戳</param>
    /// <returns>时间</returns>
    public static DateTime UnixTimeToUniversalTime(string unixTimeString)
    {
        var unixTime = Convert.ToInt64(unixTimeString);

        return UnixTimeToUniversalTime(unixTime);
    }

    /// <summary>
    /// 将UNIX时间戳转换成本地时间
    /// </summary>
    /// <param name="unixTimeString">UNIX时间戳</param>
    /// <returns>时间</returns>
    public static DateTime UnixTimeToLocalTime(string unixTimeString)
    {
        var unixTime = Convert.ToInt64(unixTimeString);

        return UnixTimeToLocalTime(unixTime);
    }

    #endregion 将UNIX时间戳转换成时间

    #endregion 时间转与UNIX时间戳转换

    /// <summary>
    /// 转换类型名为有好名称
    /// </summary>
    /// <param name="typeCode">       </param>
    /// <param name="changeLongToInt"></param>
    /// <returns></returns>
    public static string TypeCodeToFriendlyTypeName(TypeCode typeCode, bool changeLongToInt = false)
    {
        string result;

        switch (typeCode)
        {
            case TypeCode.String:
                result = "string";
                break;

            case TypeCode.Int16:
                result = "short";
                break;

            case TypeCode.Int32:
                result = "int";
                break;

            case TypeCode.Int64:
                result = changeLongToInt ? "int" : "long";
                break;

            case TypeCode.Boolean:
                result = "bool";
                break;

            case TypeCode.Char:
                result = "char";
                break;

            case TypeCode.Decimal:
                result = "decimal";
                break;

            case TypeCode.Double:
                result = "double";
                break;

            case TypeCode.Object:
                result = "object";
                break;

            case TypeCode.Single:
                result = "float";
                break;

            case TypeCode.UInt16:
                result = "ushort";
                break;

            case TypeCode.UInt32:
                result = "uint";
                break;

            case TypeCode.UInt64:
                result = "ulong";
                break;

            default:
                result = typeCode.ToString();
                break;
        }

        return result;
    }

    /// <summary>
    /// JObjectToNameValueCollection
    /// </summary>
    /// <returns></returns>
    public static ExpandoObject DictionaryToExpandoObject(IDictionary<string, object>? dic)
    {
        var eo = new ExpandoObject();

        if (dic == null || dic.Count == 0) return eo;

        return dic.Aggregate(
            eo,
            (current, d) => current.AddKeyValuePair(new KeyValuePair<string, object?>(d.Key, d.Value))
        );
    }

    /// <summary>
    /// JObjectToNameValueCollection
    /// </summary>
    /// <returns></returns>
    public static IDictionary<string, object?> ExpandoObjectToDictionary(ExpandoObject dic)
    {
        return dic;
    }

    /// <summary>
    /// JObjectToNameValueCollection
    /// </summary>
    /// <param name="jo">JObject</param>
    /// <returns></returns>
    public static IDictionary<string, object> JObjectToDictionary(JObject jo)
    {
        var dictionary = new Dictionary<string, object>();

        if (jo.Count == 0) return dictionary;

        var stopName = jo.First;

        while (stopName != null)
        {
            var jp = stopName.Value<JProperty>();

            if (jp == null) continue;

            if (jp.Value is JValue jv)
                dictionary[jp.Name] = jv.Value ?? "";
            else
                switch (jp.Value)
                {
                    case JArray jArray:
                    {
                        var list = new List<object>();

                        foreach (var item in jArray)
                            if (!(item is JObject v))
                            {
                                list.Add(item);
                            }
                            else
                            {
                                var d = JObjectToDictionary(v);

                                if (d.Count > 0) list.Add(d);
                            }

                        dictionary[jp.Name] = list;

                        break;
                    }
                    case JObject j:

                        var dic = JObjectToDictionary(j);

                        if (dic.Count > 0) dictionary[jp.Name] = dic;

                        break;
                }

            stopName = stopName.Next;
        }

        return dictionary;
    }

    /// <summary>
    /// JObjectToNameValueCollection
    /// </summary>
    /// <param name="jo">JObject</param>
    /// <returns></returns>
    public static ExpandoObject JObjectToExpandoObject(JObject jo)
    {
        var dic = JObjectToDictionary(jo);

        return DictionaryToExpandoObject(dic);
    }

    /// <summary>
    /// JObjectToNameValueCollection
    /// </summary>
    /// <param name="jo">JObject</param>
    /// <returns></returns>
    public static NameValueCollection JObjectToNameValueCollection(JObject jo)
    {
        var nv = new NameValueCollection();

        var dic = JObjectToDictionary(jo);

        foreach (var key in dic.Keys) nv[key] = dic[key].ToString();

        return nv;
    }

    /// <summary>
    /// NameValueCollectionToJson 如果含有Html，则html格式代码将被清除
    /// </summary>
    /// <param name="nv">NameValueCollection</param>
    /// <returns></returns>
    public static string NameValueCollectionToJson(NameValueCollection? nv)
    {
        if (nv == null) return "{}";

        if (nv.Count <= 0) return "{}";

        var result = new Hashtable();

        foreach (var key in nv.AllKeys)
            if (!string.IsNullOrWhiteSpace(key))
                result[key] = nv[key];

        return JsonConvert.SerializeObject(result);
        //var list = (from key in nv.AllKeys let value = nv[key] select "\"{0}\":\"{1}\"".FormatValue(key, value.ContainsHtml() ? value.FilterHtmlTag() : value)).ToList();
        //return "{" + list.Join(",") + "}";
    }

    /// <summary>
    /// Json字符串转化为NameValueCollection
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static NameValueCollection JsonToNameValueCollection(string json)
    {
        var o = JsonConvert.DeserializeObject<JObject>(json);

        return o == null ? new NameValueCollection() : JObjectToNameValueCollection(o);
    }

    /// <summary>
    /// NameValueCollection转化为Dictionary
    /// </summary>
    /// <param name="nv"></param>
    /// <returns></returns>
    public static IDictionary<string, T?> NameValueCollectionToDictionary<T>(NameValueCollection nv) where T : class
    {
        var r = new Dictionary<string, T?>();

        if (nv.Count <= 0) return r;

        foreach (var key in nv.AllKeys)
        {
            var v = nv[key];

            if (key != null) r.Add(key, v as T);
        }

        return r;
    }

    /// <summary>
    /// NameValueCollection转化为Dictionary
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static NameValueCollection DictionaryToNameValueCollection<T>(IDictionary<string, T> dic)
    {
        var r = new NameValueCollection();

        if (dic.Count <= 0) return r;

        foreach (var key in dic.Keys) r.Add(key, dic[key]?.ToString());

        return r;
    }

    /// <summary>
    /// Json字符串转化为Dictionary
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static IDictionary<string, object> JsonToDictionary(string json)
    {
        var o = JsonConvert.DeserializeObject<JObject>(json);

        return o == null ? new Dictionary<string, object>() : JObjectToDictionary(o);
    }

    /// <summary>
    /// Json字符串转化为Dictionary
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static SortedDictionary<string, T> DictionaryToSortedDictionary<T>(IDictionary<string, T> dic)
    {
        var r = new SortedDictionary<string, T>();

        foreach (var item in dic) r.Add(item.Key, item.Value);

        return r;
    }

    /// <summary>
    /// 把数组所有元素，按照“参数=参数值”的模式拼接成字符串
    /// </summary>
    /// <param name="dic">需要拼接的数组</param>
    /// <returns>拼接完成以后的字符串</returns>
    public static string DictionaryToLinkString<T>(Dictionary<string, T> dic)
    {
        var stringBuilder = new StringBuilder();

        foreach (var temp in dic) stringBuilder.Append(temp.Key + "=" + temp.Value + "&");

        //去掉最後一個&字符
        var nLen = stringBuilder.Length;
        stringBuilder.Remove(nLen - 1, 1);

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Convert
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="actionT"></param>
    /// <returns></returns>
    public static Action<object>? ConvertAction<T>(Action<T>? actionT)
    {
        if (actionT == null) return null;

        return o => actionT((T)o);
    }

    #region DataBase

    /// <summary>
    /// CreateDataTableFromDataReader
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    private static DataTable CreateDataTableFromDataReader(IDataReader reader)
    {
        var table = new DataTable();
        var fieldCount = reader.FieldCount;

        for (var i = 0; i < fieldCount; i++) table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));

        table.BeginLoadData();

        var values = new object[fieldCount];

        while (reader.Read())
        {
            reader.GetValues(values);
            table.LoadDataRow(values, true);
        }

        table.EndLoadData();

        return table;
    }

    /// <summary>
    /// DataReader转换为dataset
    /// </summary>
    /// <param name="reader">DataReader</param>
    /// <returns></returns>
    public static DataSet DataReaderToDataSet(IDataReader reader)
    {
        var ds = new DataSet();

        ds.Tables.Add(CreateDataTableFromDataReader(reader));

        while (reader.NextResult()) ds.Tables.Add(CreateDataTableFromDataReader(reader));

        return ds;
    }

    /// <summary>
    /// DataReaderToDataTable
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static DataTable DataReaderToDataTable(IDataReader reader)
    {
        var ds = DataReaderToDataSet(reader);

        return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
    }

    /// <summary>
    /// DataReaderToJson
    /// </summary>
    /// <param name="reader">    </param>
    /// <param name="keyToLower"></param>
    /// <returns></returns>
    public static string DataReaderToJson(IDataReader reader, bool keyToLower = true)
    {
        var d = DataReaderToDataTable(reader);

        var json = keyToLower ? JsonConvertAssist.SerializeAndKeyToLower(d) : JsonConvert.SerializeObject(d);

        return json;
    }

    /// <summary>
    /// DataTableToJson
    /// </summary>
    /// <param name="dataTable"> </param>
    /// <param name="keyToLower"></param>
    /// <returns></returns>
    public static string DataTableToJson(DataTable dataTable, bool keyToLower = true)
    {
        var dr = dataTable.CreateDataReader();

        return DataReaderToJson(dr, keyToLower);
    }

    /// <summary>
    /// DataReaderToDictionaryList
    /// </summary>
    /// <param name="reader">    </param>
    /// <param name="keyToLower"></param>
    /// <returns></returns>
    public static IList<IDictionary<string, object?>> DataReaderToDictionaryList(
        IDataReader reader,
        bool keyToLower = true
    )
    {
        var result = new List<IDictionary<string, object?>>();

        var json = DataReaderToJson(reader, keyToLower);
        var list = JsonConvert.DeserializeObject<List<object?>>(json);

        if (list == null) return result;

        result.AddRange(
            list.ToListFilterNullable()
                .Select(JsonConvert.SerializeObject)
                .Select(JsonToDictionary)
                .Where(dic => dic.Count > 0)!
        );

        return result;
    }

    /// <summary>
    /// DataTableToDictionaryList
    /// </summary>
    /// <param name="dataTable"> </param>
    /// <param name="keyToLower"></param>
    /// <returns></returns>
    public static IList<IDictionary<string, object?>> DataTableToDictionaryList(
        DataTable dataTable,
        bool keyToLower = true
    )
    {
        var dr = dataTable.CreateDataReader();

        return DataReaderToDictionaryList(dr, keyToLower);
    }

    /// <summary>
    /// DataReaderToExpandoObjectList
    /// </summary>
    /// <param name="reader">    </param>
    /// <param name="keyToLower"></param>
    /// <returns></returns>
    public static List<ExpandoObject> DataReaderToExpandoObjectList(IDataReader reader, bool keyToLower = true)
    {
        var result = new List<ExpandoObject>();

        var list = DataReaderToDictionaryList(reader);

        foreach (var dic in list)
        {
            var eo = new ExpandoObject();

            eo.AddDictionary(dic);

            result.Add(eo);
        }

        return result;
    }

    /// <summary>
    /// DataTableToExpandoObjectList
    /// </summary>
    /// <param name="dataTable"> </param>
    /// <param name="keyToLower"></param>
    /// <returns></returns>
    public static List<ExpandoObject> DataTableToExpandoObjectList(DataTable dataTable, bool keyToLower = true)
    {
        if (dataTable.Rows.Count <= 0) return new List<ExpandoObject>();

        var dr = dataTable.CreateDataReader();

        return DataReaderToExpandoObjectList(dr, keyToLower);
    }

    /// <summary>
    /// 检测值类型
    /// </summary>
    /// <returns></returns>
    private static object? CheckValue(TypeCode typeCode, object? value, bool longToInt = true)
    {
        object? result = null;

        switch (typeCode)
        {
            case TypeCode.String:
                result = value is DBNull ? "" : value;
                break;

            case TypeCode.Int16:
                result = value is DBNull ? 0 : value;
                break;

            case TypeCode.Int32:
                result = value is DBNull
                    ? 0
                    : longToInt
                        ? value.IsInt64() ? Convert.ToInt32(value) : value
                        : value;
                break;

            case TypeCode.Int64:
                result = value is DBNull ? 0 : value;
                break;

            case TypeCode.DateTime:
                result = value is DBNull ? DateTime.MinValue : value;
                break;

            case TypeCode.Boolean:
                result = value is DBNull ? false : value;
                break;

            case TypeCode.Char:
                result = value is DBNull ? "" : value;
                break;

            case TypeCode.Decimal:
                result = value is DBNull ? 0 : value;
                break;

            case TypeCode.Double:
                result = value is DBNull ? 0 : value;
                break;

            case TypeCode.Object:
                result = value is DBNull ? null : value;
                break;

            case TypeCode.Single:
                result = value is DBNull ? 0 : value;
                break;

            case TypeCode.UInt16:
                result = value is DBNull ? 0 : value;
                break;

            case TypeCode.UInt32:
                result = value is DBNull
                    ? 0
                    : longToInt
                        ? value.IsInt64() ? Convert.ToUInt32(value) : value
                        : value;
                break;

            case TypeCode.UInt64:
                result = value is DBNull ? 0 : value;
                break;
        }

        return result;
    }

    /// <summary>
    /// DataTable转换为Model(忽略不匹配项)
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="table">     要转换的数据表</param>
    /// <param name="longToInt"> 遇到long类型自动转换为int</param>
    /// <param name="ignoreCase">忽略大小写(默认忽略大小写)</param>
    /// <returns></returns>
    public static IList<TModel> DataTableToModel<TModel>(
        DataTable? table,
        bool longToInt = true,
        bool ignoreCase = true
    )
    {
        if (table == null) return new List<TModel>();

        var list = new List<TModel>(); //里氏替换原则
        TModel t;
        PropertyInfo[] properties;
        string tempName;

        if (!ignoreCase)
        {
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<TModel>(); ////创建指定类型的实例

                if (t == null) continue;

                properties = t.GetType().GetProperties(); //得到类的属性

                foreach (var pro in properties)
                    try
                    {
                        tempName = pro.Name;

                        if (!table.Columns.Contains(tempName)) continue;

                        var typeCode = Type.GetTypeCode(pro.PropertyType);
                        var value = row[tempName];
                        var realValue = CheckValue(typeCode, value);

                        pro.SetValue(t, realValue, null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(
                            "property {0} has error:{1}".FormatValue(pro.Name, ex.Message)
                        );
                    }

                list.Add(t);
            }
        }
        else
        {
            var colList = (from DataColumn dc in table.Columns select dc.ColumnName.ToLower()).ToList();

            //List<string> colList = (from DataColumn col in table.Columns select col.ColumnName.ToLower()).ToList();

            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<TModel>(); ////创建指定类型的实例

                if (t == null) continue;

                properties = t.GetType().GetProperties(); //得到类的属性

                foreach (var pro in properties)
                    try
                    {
                        tempName = pro.Name.ToLower();

                        if (!colList.Contains(tempName)) continue;

                        var typeCode = Type.GetTypeCode(pro.PropertyType);
                        var value = row[tempName];
                        var realValue = CheckValue(typeCode, value);

                        pro.SetValue(t, realValue, null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(
                            "property {0} has error:{1}".FormatValue(pro.Name, ex.Message)
                        );
                    }

                list.Add(t);
            }
        }

        return list;
    }

    #endregion DataBase

    /// <summary>
    /// 转换为32位整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static int ToInt(object input)
    {
        return ToIntOrNull(input) ?? 0;
    }

    /// <summary>
    /// 转换为32位可空整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static int? ToIntOrNull(object input)
    {
        var success = int.TryParse(input.SafeString(), out var result);

        if (success) return result;

        try
        {
            var temp = ToDoubleOrNull(input, 0);

            if (temp == null) return null;

            return Convert.ToInt32(temp);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 转换为64位整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static long ToLong(object input)
    {
        return ToLongOrNull(input) ?? 0;
    }

    /// <summary>
    /// 转换为64位可空整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static long? ToLongOrNull(object input)
    {
        var success = long.TryParse(input.SafeString(), out var result);

        if (success) return result;

        try
        {
            var temp = ToDecimalOrNull(input, 0);

            if (temp == null) return null;

            return Convert.ToInt64(temp);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 转换为32位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static float ToFloat(object input, int? digits = null)
    {
        return ToFloatOrNull(input, digits) ?? 0;
    }

    /// <summary>
    /// 转换为32位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static float? ToFloatOrNull(object input, int? digits = null)
    {
        var success = float.TryParse(input.SafeString(), out var result);

        if (!success) return null;

        if (digits == null) return result;

        return (float)Math.Round(result, digits.Value);
    }

    /// <summary>
    /// 转换为64位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static double ToDouble(object input, int? digits = null)
    {
        return ToDoubleOrNull(input, digits) ?? 0;
    }

    /// <summary>
    /// 转换为64位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static double? ToDoubleOrNull(object input, int? digits = null)
    {
        var success = double.TryParse(input.SafeString(), out var result);

        if (!success) return null;

        if (digits == null) return result;

        return Math.Round(result, digits.Value);
    }

    /// <summary>
    /// 转换为128位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static decimal ToDecimal(object input, int? digits = null)
    {
        return ToDecimalOrNull(input, digits) ?? 0;
    }

    /// <summary>
    /// 转换为128位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static decimal? ToDecimalOrNull(object input, int? digits = null)
    {
        var success = decimal.TryParse(input.SafeString(), out var result);

        if (!success) return null;

        if (digits == null) return result;

        return Math.Round(result, digits.Value);
    }

    /// <summary>
    /// 转换为布尔值
    /// </summary>
    /// <param name="input">输入值</param>
    public static bool ToBool(object input)
    {
        return ToBoolOrNull(input) ?? false;
    }

    /// <summary>
    /// 转换为可空布尔值
    /// </summary>
    /// <param name="input">输入值</param>
    public static bool? ToBoolOrNull(object input)
    {
        var value = GetBool(input);

        if (value != null) return value.Value;

        return bool.TryParse(input.SafeString(), out var result) ? result : null;
    }

    /// <summary>
    /// 获取布尔值
    /// </summary>
    private static bool? GetBool(object input)
    {
        switch (input.SafeString().ToLower())
        {
            case "0":
                return false;
            case "否":
                return false;
            case "不":
                return false;
            case "no":
                return false;
            case "fail":
                return false;
            case "1":
                return true;
            case "是":
                return true;
            case "ok":
                return true;
            case "yes":
                return true;
            default:
                return null;
        }
    }

    /// <summary>
    /// 转换为日期
    /// </summary>
    /// <param name="input">输入值</param>
    public static DateTime ToDate(object input)
    {
        return ToDateOrNull(input) ?? DateTime.MinValue;
    }

    /// <summary>
    /// 转换为可空日期
    /// </summary>
    /// <param name="input">输入值</param>
    public static DateTime? ToDateOrNull(object input)
    {
        return DateTime.TryParse(input.SafeString(), out var result) ? result : null;
    }

    /// <summary>
    /// 转换为Guid
    /// </summary>
    /// <param name="input">输入值</param>
    public static Guid ToGuid(object input)
    {
        return ToGuidOrNull(input) ?? Guid.Empty;
    }

    /// <summary>
    /// 转换为可空Guid
    /// </summary>
    /// <param name="input">输入值</param>
    public static Guid? ToGuidOrNull(object input)
    {
        return Guid.TryParse(input.SafeString(), out var result) ? result : null;
    }

    /// <summary>
    /// TypeToDbType
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static DbType TypeToDbType(Type type)
    {
        DbType dbt;

        try
        {
            dbt = (DbType)Enum.Parse(typeof(DbType), type.Name);
        }
        catch
        {
            dbt = DbType.Object;
        }

        return dbt;
    }

    /// <summary>
    /// DbTypeToType
    /// </summary>
    /// <param name="dbType"></param>
    /// <returns></returns>
    public static Type DbTypeToType(DbType dbType)
    {
        var toReturn = typeof(DBNull);

        switch (dbType)
        {
            case DbType.UInt64:
                toReturn = typeof(ulong);
                break;

            case DbType.Int64:
                toReturn = typeof(long);
                break;

            case DbType.Int32:
                toReturn = typeof(int);
                break;

            case DbType.UInt32:
                toReturn = typeof(uint);
                break;

            case DbType.Single:
                toReturn = typeof(float);
                break;

            case DbType.Date:
            case DbType.DateTime:
            case DbType.Time:
                toReturn = typeof(DateTime);
                break;

            case DbType.String:
            case DbType.StringFixedLength:
            case DbType.AnsiString:
            case DbType.AnsiStringFixedLength:
                toReturn = typeof(string);
                break;

            case DbType.UInt16:
                toReturn = typeof(ushort);
                break;

            case DbType.Int16:
                toReturn = typeof(short);
                break;

            case DbType.SByte:
                toReturn = typeof(byte);
                break;

            case DbType.Object:
                toReturn = typeof(object);
                break;

            case DbType.VarNumeric:
            case DbType.Decimal:
                toReturn = typeof(decimal);
                break;

            case DbType.Currency:
                toReturn = typeof(double);
                break;

            case DbType.Binary:
                toReturn = typeof(byte[]);
                break;

            case DbType.Double:
                toReturn = typeof(double);
                break;

            case DbType.Guid:
                toReturn = typeof(Guid);
                break;

            case DbType.Boolean:
                toReturn = typeof(bool);
                break;
        }

        return toReturn;
    }
}