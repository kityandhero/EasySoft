using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Encryption;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods
{
    public static class StringExtensions
    {
        #region In

        /// <summary>
        /// 字符串包含于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In(this string source, params string[] array)
        {
            return CollectionAssist.In(source, array);
        }

        /// <summary>
        /// 字符串包含于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool In(
            this string source,
            IEnumerable<string> list,
            StringComparison comparison = StringComparison.Ordinal
        )
        {
            return CollectionAssist.In(source, list, comparison);
        }

        #endregion In

        #region Md5

        /// <summary>
        /// 转换为md5字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToMd5(this string source)
        {
            return Md5Assist.ToMd5(source);
        }

        #endregion

        #region CutOut

        /// <summary>
        /// 截取指定个数字符串
        /// </summary>
        /// <param name="source">字符源</param>
        /// <param name="length">保留字符个数</param>
        /// <param name="showEllipsis">省去的字符显示的符号，默认"..."</param>
        /// <param name="direction">计数开始方向，默认从左边开始计数</param>
        /// <returns></returns>
        public static string CutOut(
            this string source,
            int length,
            string showEllipsis = "...",
            StringCutDirection direction = StringCutDirection.FromLeft
        )
        {
            if (source.IsNullOrEmpty())
            {
                return source;
            }

            var fullLength = source.Length;

            if (direction == StringCutDirection.FromRight)
            {
                return length >= fullLength
                    ? source
                    : string.Concat(showEllipsis, source.AsSpan(fullLength - length, length).ToString());
            }

            return length >= fullLength ? source : string.Concat(source.AsSpan(0, length).ToString(), showEllipsis);
        }

        #endregion CutOut

        public static bool CheckNameInEnum<T>(this string v, params T[] excludeItems) where T : struct
        {
            if (excludeItems.Length <= 0)
            {
                var values = EnumAssist.GetNameList<T>();

                return values.Contains(v);
            }
            else
            {
                var excludeList = (excludeItems).Select(o => o.ToString()).ToList();

                var values = EnumAssist.GetNameList<T>().Where(o => !excludeList.Contains(o)).ToList();

                return values.Contains(v);
            }
        }

        public static T GetEnumByKeyValueDefinitionTag<T>(this string tag) where T : struct
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                throw new Exception("无效的Tag值");
            }

            var list = EnumAssist.GetIntValues<T>();

            foreach (var item in list)
            {
                var name = EnumAssist.GetNameByValue<T>(item);

                Enum.TryParse(name, out T t);

                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                var type = t.GetType();

                var fieldInfo = type.GetField(name);

                if (fieldInfo == null)
                {
                    continue;
                }

                var keyValueDefinitionAttribute = fieldInfo.GetAttribute<KeyValueDefinitionAttribute>(
                    "",
                    false,
                    MemberTypes.Field
                );

                if (keyValueDefinitionAttribute != null && keyValueDefinitionAttribute.Tag == tag)
                {
                    return t;
                }
            }

            throw new Exception("未找到匹配项");
        }

        /// <summary>
        /// 当为空字符串时，用默认值代替
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string DefaultWhenNullOrEmpty(this string source, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(source) ? defaultValue : source;
        }

        /// <summary>
        /// 转换首字母小写  
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToLowerFirst(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            if (string.IsNullOrEmpty(source.TrimStart()))
            {
                return source;
            }

            var firstChar = source.GetByIndex(0).ToLower();

            return source.ReplaceByIndex(0, firstChar);
        }

        /// <summary>
        /// 转换string为int
        /// </summary>
        /// <param name="source">要转换的变量</param>
        /// <returns></returns>
        public static int ToInt(this string source)
        {
            if (source.IsInt(out var value))
            {
                return value;
            }

            throw new Exception("该字符串不能转换为int类型");
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToInt32(this string source)
        {
            if (source.IsInt(out var value))
            {
                return value;
            }

            throw new Exception("该字符串不能转换为Int32类型");
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToLong(this string source)
        {
            if (source.IsLong(out var value))
            {
                return value;
            }

            throw new Exception("该字符串不能转换为long类型");
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToInt64(this string source)
        {
            if (source.IsLong(out var value))
            {
                return value;
            }

            throw new Exception("该字符串不能转换为Int64类型");
        }

        #region ToDateTime()

        /// <summary>
        /// 转换string为int
        /// </summary>
        /// <param name="source">要转换的变量</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string source)
        {
            if (source.IsDateTime())
            {
                return Convert.ToDateTime(source);
            }

            throw new Exception("该字符串不能转换为DateTime类型");
        }

        #endregion

        #region URL

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(this string? url)
        {
            return WebUtility.UrlEncode(url) ?? "";
        }

        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(this string url)
        {
            return WebUtility.UrlDecode(url);
        }

        /// <summary>
        /// 获取URL指定参数
        /// </summary>
        /// <param name="url">      todo: describe url parameter on GetUrlParam</param>
        /// <param name="paramName">todo: describe paramName parameter on GetUrlParam</param>
        public static string GetUrlParam(this string url, string paramName)
        {
            url.ParseUrl(out _, out var nv);

            return nv[paramName] ?? "";
        }

        /// <summary>
        /// SetUrlParam
        /// </summary>
        /// <param name="url">      </param>
        /// <param name="urlParams"></param>
        /// <returns></returns>
        public static string SetUrlParam(this string url, NameValueCollection urlParams)
        {
            var u = new Uri(url);

            var baseUrl = u.Scheme + "://" + u.Host + (u.Port != 80 ? ":" + u.Port : "") + u.AbsolutePath;

            var nv = url.FormatParamToNameValueCollection();

            foreach (string key in urlParams.Keys)
            {
                var existKey = false;

                foreach (string alreadyKey in nv.Keys)
                {
                    if (alreadyKey != key)
                    {
                        continue;
                    }

                    nv[alreadyKey] = urlParams[key];
                    existKey = true;

                    break;
                }

                if (!existKey)
                {
                    nv[key] = urlParams[key];
                }
            }

            var result = baseUrl + "?";
            var builder = new StringBuilder();

            builder.Append(result);

            foreach (string k in nv.Keys)
            {
                var v = nv[k];
                builder.Append(k + "=" + WebUtility.UrlEncode(v) + "&");
            }

            result = builder.ToString();

            result = result.Substring(0, result.Length - 1);

            return result;
        }

        /// <summary>
        /// 添加URL参数
        /// </summary>
        /// <param name="url">      todo: describe url parameter on AddUrlParam</param>
        /// <param name="urlParams">todo: describe urlParams parameter on AddUrlParam</param>
        public static string AddUrlParam(this string url, NameValueCollection urlParams)
        {
            return SetUrlParam(url, urlParams);
        }

        /// <summary>
        /// 添加URL参数
        /// </summary>
        /// <param name="url">      todo: describe url parameter on AddUrlParam</param>
        /// <param name="paramName">todo: describe paramName parameter on AddUrlParam</param>
        /// <param name="value">    todo: describe value parameter on AddUrlParam</param>
        public static string AddUrlParam(this string url, string paramName, string value)
        {
            var nv = new NameValueCollection
            {
                [paramName] = value
            };

            return AddUrlParam(url, nv);
        }

        /// <summary>
        /// UpdateUrlParam
        /// </summary>
        /// <param name="url">      </param>
        /// <param name="urlParams"></param>
        /// <returns></returns>
        public static string UpdateUrlParam(this string url, NameValueCollection urlParams)
        {
            return SetUrlParam(url, urlParams);
        }

        /// <summary>
        /// 更新URL参数
        /// </summary>
        /// <param name="url">      todo: describe url parameter on UpdateUrlParam</param>
        /// <param name="paramName">todo: describe paramName parameter on UpdateUrlParam</param>
        /// <param name="value">    todo: describe value parameter on UpdateUrlParam</param>
        public static string UpdateUrlParam(this string url, string paramName, string value)
        {
            var nv = new NameValueCollection
            {
                [paramName] = value
            };

            return AddUrlParam(url, nv);
        }

        /// <summary>
        /// </summary>
        /// <param name="url">       </param>
        /// <param name="paramNames"></param>
        /// <returns></returns>
        public static string RemoveUrlParam(this string url, IEnumerable<string> paramNames)
        {
            var u = new Uri(url);

            var baseUrl = u.Scheme + "://" + u.Host + (u.Port != 80 ? ":" + u.Port : "") + u.AbsolutePath;

            var nv = url.FormatParamToNameValueCollection();

            foreach (var p in paramNames)
            {
                foreach (string alreadyKey in nv.Keys)
                {
                    if (alreadyKey == p)
                    {
                        nv.Remove(alreadyKey);
                        break;
                    }
                }
            }

            var result = baseUrl + "?";
            var builder = new StringBuilder();

            builder.Append(result);

            foreach (string k in nv.Keys)
            {
                builder.Append(k + "=" + WebUtility.UrlEncode(nv[k]) + "&");
            }

            result = builder.ToString();

            result = result.Substring(0, result.Length - 1);

            return result;
        }

        /// <summary>
        ///RemoveUrlParam
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static string RemoveUrlParam(this string url, string paramName)
        {
            var paramList = new List<string>() { paramName };

            return RemoveUrlParam(url, paramList);
        }

        /// <summary>
        /// 删除URL参数
        /// </summary>
        /// <param name="url">      todo: describe url parameter on DeleteUrlParam</param>
        /// <param name="paramName">todo: describe paramName parameter on DeleteUrlParam</param>
        public static string DeleteUrlParam(this string url, string paramName)
        {
            return RemoveUrlParam(url, paramName);
        }

        /// <summary>
        /// 分析URL所属的域
        /// </summary>
        /// <param name="fromUrl">  </param>
        /// <param name="domain">   </param>
        /// <param name="subDomain"></param>
        public static void GetUrlDomain(this string fromUrl, out string domain, out string subDomain)
        {
            try
            {
                var builder = new UriBuilder(fromUrl);

                fromUrl = builder.ToString();

                var u = new Uri(fromUrl);

                if (u.IsWellFormedOriginalString())
                {
                    if (u.IsFile)
                    {
                        subDomain = domain = "客户端本地文件路径";
                    }
                    else
                    {
                        var authority = u.Authority;
                        var ss = u.Authority.Split('.');
                        if (ss.Length == 2)
                        {
                            authority = "www." + authority;
                        }

                        var index = authority.IndexOf('.', 0);

                        domain = authority.Substring(index + 1, authority.Length - index - 1).Replace("comhttp", "com");
                        subDomain = authority.Replace("comhttp", "com");

                        if (ss.Length >= 2)
                        {
                            return;
                        }

                        domain = "不明路径";
                        subDomain = "不明路径";
                    }
                }
                else
                {
                    if (u.IsFile)
                    {
                        subDomain = domain = "客户端本地文件路径";
                    }
                    else
                    {
                        subDomain = domain = "不明路径";
                    }
                }
            }
            catch
            {
                subDomain = domain = "不明路径";
            }
        }

        /// <summary>
        /// FormatParamToNameValueCollection
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static NameValueCollection FormatParamToNameValueCollection(this string url)
        {
            url.ParseUrl(out _, out var nv);

            return nv;
        }

        /// <summary>
        /// 分析 url 字符串中的参数信息
        /// </summary>
        /// <param name="url">    输入的 URL</param>
        /// <param name="baseUrl">输出 URL 的基础部分</param>
        /// <param name="nvc">    输出分析后得到的 (参数名,参数值) 的集合</param>
        /// <param name="keyToLowerFirst"></param>
        public static void ParseUrl(
            this string url,
            out string baseUrl,
            out NameValueCollection nvc,
            bool keyToLowerFirst = true
        )
        {
            if (url.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(url));
            }

            nvc = new NameValueCollection();

            baseUrl = "";

            if (url == "")
            {
                return;
            }

            var questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }

            baseUrl = url.Substring(0, questionMarkIndex);

            if (questionMarkIndex == url.Length - 1)
            {
                return;
            }

            var ps = url.Substring(questionMarkIndex + 1);

            // 开始分析参数对
            var re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            var mc = re.Matches(ps);

            foreach (Match m in mc)
            {
                nvc.Add(keyToLowerFirst ? m.Result("$2").ToLowerFirst() : m.Result("$2"), m.Result("$3"));
            }
        }

        #endregion URL

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="input">格式化字符串</param>
        /// <param name="param">格式化参数</param>
        /// <returns></returns>
        public static string FormatValue(this string input, params object[] param)
        {
            return string.IsNullOrEmpty(input) ? input : string.Format(input, param);
        }

        /// <summary>
        /// 获取字符串中指定位
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="index">指定位置</param>
        /// <returns></returns>
        public static string GetByIndex(this string source, int index)
        {
            if (index > source.Length - 1)
            {
                return "";
            }

            if (index == 0)
            {
                if (source.Length == 1)
                {
                    return source;
                }

                var strTemp = source.Remove(1);
                return strTemp;
            }

            if (index == source.Length - 1)
            {
                var strTemp = source.Remove(0, index);
                return strTemp;
            }
            else
            {
                var strTemp = source.Remove(index + 1);
                strTemp = strTemp.Remove(0, index);
                return strTemp;
            }
        }

        /// <summary>
        /// 替换指定的某位字符
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="index">指定起始位置</param>
        /// <param name="newString">要替换成的字符串</param>
        /// <param name="length">替换的位数</param>
        /// <returns></returns>
        public static string ReplaceByIndex(this string source, int index, string newString, int length = 1)
        {
            if (length < 0)
            {
                throw new Exception("替换长度不能小于0");
            }

            if (index + length + 1 > source.Length)
            {
                length = source.Length - index;
            }

            if (index > source.Length - 1)
            {
                return source;
            }

            var replaceString = "";

            if (index == 0)
            {
                var strTemp = source.Remove(0, length);

                replaceString = "";

                for (var i = 0; i < length; i++)
                {
                    replaceString += newString;
                }

                return replaceString + strTemp;
            }

            if (index == source.Length - 1)
            {
                var strTemp = source.Remove(index);
                return strTemp + newString;
            }

            var strTempFirst = source.Remove(index);
            for (var i = 0; i < length; i++)
            {
                replaceString += newString;
            }

            var strTempLast = source.Remove(0, index + length);
            return strTempFirst + replaceString + strTempLast;
        }

        /// <summary>
        /// 按照分隔符分割为Array
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <param name="option">分割配置</param>
        /// <returns></returns>
        public static string[] SplitToArray(this string input, string delimiter, SplitOption option = SplitOption.None)
        {
            var temp = input.SplitToList(delimiter, option);

            return temp.ToArray();
        }

        /// <summary>
        /// 按照分隔符分割为List
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <param name="option">分割配置</param>
        /// <returns></returns>
        public static List<string> SplitToList(this string input, string delimiter,
            SplitOption option = SplitOption.None)
        {
            var result = new List<string>();

            if (input.IsNullOrEmpty())
            {
                return result;
            }

            var array = new Regex(delimiter).Split(input);

            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var s in array)

                // ReSharper restore LoopCanBeConvertedToQuery
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    continue;
                }

                if (option == SplitOption.FilterSame)
                {
                    if (result.Contains(s))
                    {
                        continue;
                    }
                }

                result.Add(s);
            }

            return result;
        }

        /// <summary>
        /// 在 2，8，10，16进制之间进行转换；
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fromBase"></param>
        /// <param name="toBase"></param>
        /// <returns></returns>
        public static string ToBase(this string input, int fromBase, int toBase)
        {
            if (!input.IsInt())
            {
                return "";
            }

            try
            {
                var intValue = Convert.ToInt32(input, fromBase); //先转成10进制
                var result = Convert.ToString(intValue, toBase); //再转成目标进制

                if (toBase != 2)
                {
                    return result;
                }

                var resultLength = result.Length; //获取二进制的长度

                switch (resultLength)
                {
                    case 7:
                        result = "0" + result;
                        break;

                    case 6:
                        result = "00" + result;
                        break;

                    case 5:
                        result = "000" + result;
                        break;

                    case 4:
                        result = "0000" + result;
                        break;

                    case 3:
                        result = "00000" + result;
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"转换进制时发生异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 是否为 int 类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsInt(this string source)
        {
            return VerifyAssist.IsInt(source);
        }

        /// <summary>
        /// 是否为 int 类型
        /// </summary>
        /// <param name="source">要检验的变量</param>
        /// <param name="value">转换后的值</param>
        /// <returns></returns>
        public static bool IsInt(this string source, out int value)
        {
            return VerifyAssist.IsInt(source, out value);
        }

        /// <summary>
        /// 检验是否为null或空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// 检验去空格后是否为空
        /// </summary>  
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsTrimEmpty(this string source)
        {
            return source.IsNullOrEmpty() || source.Trim().IsNullOrEmpty();
        }

        /// <summary>
        /// 过滤sql危险字符
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FilterSql(this string source)
        {
            return source.Replace("'", "''");
        }

        /// <summary>
        /// 拼接路径, 兼容Linux
        /// </summary>
        /// <param name="path"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string Combine(this string path, string target)
        {
            return PathAssist.Combine(path, target);
        }

        #region Remove

        /// <summary>
        /// 移除复合筛选条件的字符串中的元素并返回移除后的新字符串
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="filter">筛选条件</param>
        /// <param name="ignoringCase">忽略大小写</param>
        /// <returns>返回移除符合筛选条件后的新字符串</returns>
        public static string Remove(this string input, List<char> filter, bool ignoringCase = true)
        {
            var newFilter = new List<char>();

            if (ignoringCase)
            {
                newFilter.AddRange(from chr in filter
                    select chr.ToString().ToLower()
                    into newChar
                    where !newChar.IsTrimEmpty()
                    select Convert.ToChar(newChar));
            }
            else
            {
                newFilter = filter;
            }

            var a = input.ToArray();
            var result =
                (from chr in a
                    let cc = ignoringCase ? Convert.ToChar(chr.ToString(CultureInfo.InvariantCulture).ToLower()) : chr
                    where !newFilter.Contains(cc)
                    select chr).Aggregate("", (current, chr) => current + chr);

            return result;
        }

        /// <summary>
        /// Removes everything that is in the filter text from the input.
        /// </summary>
        /// <param name="input">Input text</param>
        /// <param name="filter">Regex expression of text to remove</param>
        /// <returns>Everything not in the filter text.</returns>
        public static string Remove(this string input, string filter)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(filter))
            {
                return input;
            }

            return new Regex(filter).Replace(input, "");
        }

        /// <summary>
        /// Removes everything that is in the filter text from the input.
        /// </summary>
        /// <param name="input">Input text</param>
        /// <param name="filter">Predefined filter to use (can be combined as they are flags)</param>
        /// <returns>Everything not in the filter text.</returns>
        public static string Remove(this string input, StringFilter filter)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            var value = BuildFilter(filter);
            return input.Remove(value);
        }

        #endregion

        #region RemoveEnd

        /// <summary>
        /// Removes everything that is in the filter text from the input.
        /// </summary>
        /// <param name="input">Input text</param>
        /// <param name="filter">Regex expression of text to remove</param>
        /// <returns>Everything not in the filter text.</returns>
        public static string RemoveEnd(this string input, string filter)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(filter))
            {
                return input;
            }

            return new Regex($"{filter}$").Replace(input, "");
        }

        #endregion

        #region Private Functions

        private static string BuildFilter(StringFilter filter)
        {
            var filterValue = "";
            var separator = "";
            if (filter.HasFlag(StringFilter.Alpha))
            {
                filterValue += separator + "[a-zA-Z]";
                separator = "|";
            }

            if (filter.HasFlag(StringFilter.Numeric))
            {
                filterValue += separator + "[0-9]";
                separator = "|";
            }

            if (filter.HasFlag(StringFilter.FloatNumeric))
            {
                filterValue += separator + @"[0-9\.]";
                separator = "|";
            }

            if (filter.HasFlag(StringFilter.ExtraSpaces))
            {
                filterValue += separator + @"[ ]{2,}";
            }

            return filterValue;
        }

        #endregion
    }
}