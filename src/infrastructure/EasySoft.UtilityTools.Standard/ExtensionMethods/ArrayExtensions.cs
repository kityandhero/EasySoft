/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System.Globalization;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

/// <summary>
/// Array extensions
/// </summary>
public static class ArrayExtensions
{
    #region Functions

    #region GetPropertyValueCollection

    /// <summary>
    /// 取出特定的属性值的集合
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="propertyName">属性名</param>
    /// <param name="distinct">去除重复值</param>
    /// <example></example>
    /// <returns></returns>
    public static IList<TP> GetPropertyValueCollection<T, TP>(
        this T[] source,
        string propertyName,
        bool distinct = true
    )
    {
        if (source.Length == 0) return new List<TP>();

        if (propertyName.IsNullOrEmpty()) throw new Exception("属性名必须有意义，不能为null或空");

        PropertyInfo? p = null;

        var l = source.ToListFilterNullable();

        if (l.Any())
        {
            var first = l[0];

            if (first != null) p = first.GetType().GetProperty(propertyName);
        }
        else
        {
            return new List<TP>();
        }

        if (p == null) return new List<TP>();

        var t = default(TP);

        if (t == null) return new List<TP>();

        var type = t.GetType();

        if (p.PropertyType != type) return new List<TP>();

        var result = new List<TP>();

        foreach (var item in source)
        {
            var v = (TP)p.GetValue(item, null)!;

            if (distinct)
            {
                if (!result.Contains(v)) result.Add(v);
            }
            else
            {
                result.Add(v);
            }
        }

        return result;
    }

    #endregion GetPropertyValueCollection

    #region Get

    /// <summary>
    /// 根据条件获取对象 满足筛选条件的第一个元素
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="filter">筛选对象</param>
    /// <param name="propertyName">属性名</param>
    /// <example></example>
    /// <returns></returns>
    public static T? Get<T>(this T[] source, object filter, string? propertyName = null) where T : class
    {
        T?[] r = source.GetAll(filter, propertyName);

        return r.Length > 0 ? r[0] : null;
    }

    /// <summary>
    /// 根据条件获取对象 满足筛选条件的所有元素
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="filter">筛选对象</param>
    /// <param name="propertyName">属性名</param>
    /// <example></example>
    /// <returns></returns>
    public static T[] GetAll<T>(this T[] source, object filter, string? propertyName = null) where T : class
    {
        if (string.IsNullOrWhiteSpace(propertyName)) return Array.Empty<T>();

        if (source.Length == 0) return Array.Empty<T>();

        var result = new List<T>();

        var filterTypeIsDefinedBaseType = filter.IsDefinedBaseType();
        var first = source[0];
        var firstTypeIsDefinedBaseType = first.IsDefinedBaseType();

        if (filterTypeIsDefinedBaseType && firstTypeIsDefinedBaseType)
        {
            if (filter.GetType().FullName != first.GetType().FullName) return result.ToArray();

            if (source.Contains((T)filter)) result.AddRange(source.Where(one => one == filter));

            return result.ToArray();
        }

        if (!filterTypeIsDefinedBaseType && firstTypeIsDefinedBaseType)
        {
            var p = filter.GetType().GetProperty(propertyName);

            if (p?.PropertyType.FullName != first.GetType().FullName) return result.ToArray();

            var v = (T)p?.GetValue(filter, null)!;

            if (source.Contains(v)) result.AddRange(source.Where(one => one == filter));

            return result.ToArray();
        }

        if (!firstTypeIsDefinedBaseType && filterTypeIsDefinedBaseType)
        {
            var p = first.GetType().GetProperty(propertyName);

            if (p?.PropertyType.FullName != filter.GetType().FullName) return result.ToArray();

            foreach (var item in source)
            {
                var v = p?.GetValue(item, null);

                if (v != null && v.Equals(filter)) result.Add(item);
            }

            return result.ToArray();
        }

        if (filterTypeIsDefinedBaseType || firstTypeIsDefinedBaseType) return Array.Empty<T>();

        {
            if (filter.GetType().FullName == first.GetType().FullName)
            {
                if (!source.Contains((T)filter)) return result.ToArray();

                foreach (var item in source)
                    if (item.EqualsByProperty(filter))
                        result.Add(item);

                return result.ToArray();
            }

            var filterPropertyInfo = filter.GetType().GetProperty(propertyName);

            if (filterPropertyInfo == null) return Array.Empty<T>();

            var filterPropertyValue = filterPropertyInfo.GetValue(filter, null);

            var p = first.GetType().GetProperty(propertyName);

            if (p == null) return Array.Empty<T>();

            if (p.PropertyType.FullName != filterPropertyInfo.PropertyType.FullName) return result.ToArray();

            {
                foreach (var item in source)
                {
                    var v = p.GetValue(item, null);

                    if (v.IsDefinedBaseType())
                    {
                        if (v != null && v.Equals(filterPropertyValue)) result.Add(item);
                    }
                    else
                    {
                        if (filterPropertyValue != null && v.EqualsByProperty(filterPropertyValue)) result.Add(item);
                    }
                }
            }

            return result.ToArray();
        }
    }

    #endregion Get

    #region ExistByPropertyValue

    /// <summary>
    /// 判断对象是否存在
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="filter">筛选对象</param>
    /// <param name="propertyName">属性名</param>
    /// <example></example>
    /// <returns></returns>
    public static bool ExistByPropertyValue<T>(this T[] source, object filter, string? propertyName = null)
        where T : class
    {
        if (string.IsNullOrWhiteSpace(propertyName)) return false;

        if (source.Length == 0) return false;

        var filterTypeIsDefinedBaseType = filter.IsDefinedBaseType();
        var first = source[0];
        var firstTypeIsDefinedBaseType = first.IsDefinedBaseType();

        if (filterTypeIsDefinedBaseType && firstTypeIsDefinedBaseType)
            return filter.GetType().FullName == first.GetType().FullName && source.Contains((T)filter);

        if (!filterTypeIsDefinedBaseType && firstTypeIsDefinedBaseType)
        {
            if (propertyName.IsNullOrEmpty()) return false;

            // ReSharper disable once AssignNullToNotNullAttribute
            var p = filter.GetType().GetProperty(propertyName);

            if (p?.PropertyType.FullName != first.GetType().FullName) return false;

            var v = (T)p?.GetValue(filter, null)!;

            return source.Contains(v);
        }

        if (!firstTypeIsDefinedBaseType && filterTypeIsDefinedBaseType)
        {
            if (propertyName.IsNullOrEmpty()) return false;

            var p = first.GetType().GetProperty(propertyName);

            if (p?.PropertyType.FullName != filter.GetType().FullName) return false;

            return source.Select(item => p?.GetValue(item, null)).Any(v => v != null && v.Equals(filter));
        }

        if (!filterTypeIsDefinedBaseType && !firstTypeIsDefinedBaseType)
        {
            if (filter.GetType().FullName == first.GetType().FullName) return source.ContainsByProperty(filter);

            if (propertyName.IsNullOrEmpty()) return false;

            var filterPropertyInfo = filter.GetType().GetProperty(propertyName);

            if (filterPropertyInfo == null) return false;

            var filterPropertyValue = filterPropertyInfo.GetValue(filter, null);

            var p = first.GetType().GetProperty(propertyName);
            if (p == null) return false;

            if (p.PropertyType.FullName != filterPropertyInfo.PropertyType.FullName) return false;

            return source.Select(item => p.GetValue(item, null))
                .Any(v => v != null && v.Equals(filterPropertyValue));
        }

        return false;
    }

    #endregion ExistByPropertyValue

    #region SortByPropertyValue

    public static T[] SortByPropertyValue<T>(this IEnumerable<T> source,
        string getPropertyName,
        SortRule rule = SortRule.Asc,
        string? propertyName = null,
        int? sortCount = null)
    {
        var array = source.ToArray();

        return array.SortByPropertyValue(rule, propertyName, sortCount);
    }

    /// <summary>
    /// 按照属性值排序
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="propertyName">属性名</param>
    /// <param name="rule">排序规则</param>
    /// <param name="sortCount">针对前几位元素进行排序，默认全部元素</param>
    /// <example></example>
    /// <returns></returns>
    public static T[] SortByPropertyValue<T>(
        this T[] source,
        SortRule rule = SortRule.Asc,
        string? propertyName = null,
        int? sortCount = null
    )
    {
        var result = new List<T>();

        if (source.Length == 0) return source;

        if (sortCount is 0 or null) sortCount = source.Length;

        if (sortCount > source.Length) sortCount = source.Length;

        var t = source[0];
        var filterIsDefinedBaseType = t.IsDefinedBaseType();

        if (filterIsDefinedBaseType)
        {
            var valueList = source.Select(item => (IComparable)item!).ToList();

            for (var i = 1; i < sortCount; i++)
            {
                if (valueList[i].CompareTo(valueList[i - 1]) >= 0) continue;

                //若第i个元素大于i-1元素，直接插入。小于的话，移动有序表后插入
                var j = i - 1;
                var x = valueList[i]; //复制为哨兵，即存储待排序元素
                valueList[i] = valueList[i - 1]; //先后移一个元素
                while (j >= 0 && x.CompareTo(valueList[j]) < 0)
                {
                    //查找在有序表的插入位置
                    valueList[j + 1] = valueList[j];
                    j--; //元素后移
                }

                valueList[j + 1] = x; //插入到正确位置
            }

            result = valueList.ToList().Cast<T>().ToList();

            if (rule != SortRule.Desc) return result.ToArray();

            {
                var newResult = new List<T>();

                for (var index = result.Count - 1; index >= 0; index--)
                {
                    var item = result[index];

                    newResult.Add(item);
                }

                return newResult.ToArray();
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new Exception("非基础类型的比较条件，必须指定属性名");

            var type = source.ToListFilterNullable()[0]?.GetType();

            var property = type?.GetProperty(propertyName);

            if (property == null) throw new Exception("指定的属性不存在");

            var firstValue = property.GetValue(source[0], null);

            if (!firstValue.IsDefinedBaseType()) throw new Exception("指定的属性类型只支持基础类型");

            var test = firstValue as IComparable;
            if (test == null) throw new Exception("指定的属性类型不支持比较");

            var valueList = source.Select(item => (IComparable)property.GetValue(item, null)!)
                .ToListFilterNullable();

            var sortValueList = valueList.ToArray().SortByPropertyValue(
                rule,
                null,
                sortCount
            );

            if (sortValueList.Length == 0) return source;

            foreach (var item in sortValueList)
            foreach (var one in source)
            {
                var v = property.GetValue(one, null);

                if (v != null && !v.Equals(item)) continue;

                if (result.Contains(one)) continue;

                result.Add(one);
            }

            return result.ToArray();
        }
    }

    #endregion SortByPropertyValue

    #region Join

    /// <summary>
    /// JoinCore 用于把数组中的所有元素放入一个字符串,元素是通过指定的分隔符进行分隔
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="connector">连接符</param>
    /// <param name="template">格式化字符串</param>
    /// <param name="distinct">去除重复，默认去重</param>
    /// <returns></returns>
    private static string JoinCore<T>(IEnumerable<T> source, string connector, string template = "",
        bool distinct = true)
    {
        var result = "";
        var logList = new List<T>();

        foreach (var one in source)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(one))) continue;

            if (distinct)
                if (logList.Contains(one))
                    continue;

            if (template.IsNullOrEmpty())
                result += one + connector;
            else
                result += template.FormatValue(one?.ToString() ?? string.Empty) + connector;

            logList.Add(one);
        }

        if (connector.IsNullOrEmpty()) return result;

        var connectorLength = connector.Length;

        result = result.Remove(result.Length - connectorLength);

        return result;
    }

    /// <summary>
    /// Join 用于把数组中的所有元素放入一个字符串,元素是通过指定的分隔符进行分隔, 如果属性值不是Int,或者string,将会被忽略
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="propertyName">属性名称(非基础类型必填)</param>
    /// <param name="connector">连接符</param>
    /// <param name="template">格式化字符串</param>
    /// <param name="distinct">去除重复，默认去重</param>
    /// <example></example>
    /// <returns></returns>
    public static string Join<T>(
        this T[] source,
        string connector,
        string template = "",
        bool distinct = true,
        string propertyName = ""
    )
    {
        if (source.Length == 0) return "";

        var first = source.ToListFilterNullable().ToArray()[0];

        if (first == null) return "";

        var type = first.GetType();
        var c = Type.GetTypeCode(type);

        if (c != TypeCode.Object) return JoinCore(source, connector, template, distinct);

        var p = first.GetType().GetProperty(propertyName);

        if (p == null) return "";

        var list = new List<object>();

        foreach (var item in source)
        {
            var v = p.GetValue(item, null);

            if (v != null) list.Add(v);
        }

        return JoinCore(list, connector, template, distinct);
    }

    #endregion Join

    /// <summary>
    /// 读取数组
    /// </summary>
    /// <param name="src">源数组</param>
    /// <param name="offset">起始位置</param>
    /// <param name="count">复制字节数</param>
    /// <returns>返回复制的总字节数</returns>
    public static byte[] ReadBytes(this byte[] src, int offset = 0, int count = 0)
    {
        // 即使是全部，也要复制一份，而不只是返回原数组，因为可能就是为了复制数组
        if (count <= 0) count = src.Length - offset;

        var bts = new byte[count];

        Buffer.BlockCopy(src, offset, bts, 0, bts.Length);

        return bts;
    }

    /// <summary>
    /// 一个数组是否以另一个数组开头
    /// </summary>
    /// <param name="source"></param>
    /// <param name="buffer">缓冲区</param>
    /// <returns></returns>
    public static bool StartsWith(this byte[] source, byte[] buffer)
    {
        if (source.Length < buffer.Length) return false;

        // ReSharper disable LoopCanBeConvertedToQuery
        for (var i = 0; i < buffer.Length; i++)
            if (source[i] != buffer[i])
                return false;

        return true;
    }

    /// <summary>
    /// 一个数组是否以另一个数组结尾
    /// </summary>
    /// <param name="source"></param>
    /// <param name="buffer">缓冲区</param>
    /// <returns></returns>
    public static bool EndsWith(this byte[] source, byte[] buffer)
    {
        if (source.Length < buffer.Length) return false;

        var p = source.Length - buffer.Length;

        // ReSharper disable LoopCanBeConvertedToQuery
        for (var i = 0; i < buffer.Length; i++)

            // ReSharper restore LoopCanBeConvertedToQuery
            if (source[p + i] != buffer[i])
                return false;

        return true;
    }

    /// <summary>
    /// 在字节数组中查找另一个字节数组的位置，不存在则返回-1
    /// </summary>
    /// <param name="source">字节数组</param>
    /// <param name="buffer">另一个字节数组</param>
    /// <param name="offset">偏移</param>
    /// <param name="length">查找长度</param>
    /// <returns></returns>
    public static long IndexOf(this byte[] source, byte[] buffer, long offset = 0, long length = 0)
    {
        return IndexOf(source, 0, 0, buffer, offset, length);
    }

    /// <summary>
    /// 在字节数组中查找另一个字节数组的位置，不存在则返回-1
    /// </summary>
    /// <param name="source">字节数组</param>
    /// <param name="start">源数组起始位置</param>
    /// <param name="count">查找长度</param>
    /// <param name="buffer">另一个字节数组</param>
    /// <param name="offset">偏移</param>
    /// <param name="length">查找长度</param>
    /// <returns></returns>
    public static long IndexOf(this byte[] source, long start, long count, byte[] buffer, long offset = 0,
        long length = 0)
    {
        if (start < 0) start = 0;
        if (count <= 0 || count > source.Length - start) count = source.Length;
        if (length <= 0 || length > buffer.Length - offset) length = buffer.Length - offset;

        // 已匹配字节数
        long win = 0;
        for (var i = start; i + length - win <= count; i++)
            if (source[i] == buffer[offset + win])
            {
                win++;

                // 全部匹配，退出
                if (win >= length) return i - length + 1 - start;
            }
            else
            {
                //win = 0; // 只要有一个不匹配，马上清零
                // 不能直接清零，那样会导致数据丢失，需要逐位探测，窗口一个个字节滑动
                i = i - win;
                win = 0;
            }

        return -1;
    }

    /// <summary>
    /// 比较两个字节数组大小。相等返回0，不等则返回不等的位置，如果位置为0，则返回1。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="buffer">缓冲区</param>
    /// <returns></returns>
    public static int CompareTo(this byte[] source, byte[] buffer)
    {
        return CompareTo(source, 0, 0, buffer);
    }

    /// <summary>
    /// 比较两个字节数组大小。相等返回0，不等则返回不等的位置，如果位置为0，则返回1。
    /// </summary>
    /// <param name="source"></param>
    /// <param name="start"></param>
    /// <param name="count">数量</param>
    /// <param name="buffer">缓冲区</param>
    /// <param name="offset">偏移</param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static int CompareTo(this byte[] source, long start, long count, byte[] buffer, long offset = 0,
        long length = 0)
    {
        if (source == buffer) return 0;

        if (start < 0) start = 0;
        if (count <= 0 || count > source.Length - start) count = source.Length - start;
        if (length <= 0 || length > buffer.Length - offset) length = buffer.Length - offset;

        // 逐字节比较
        for (var i = 0; i < count && i < length; i++)
        {
            var rs = source[start + i].CompareTo(buffer[offset + i]);
            if (rs != 0) return i > 0 ? i : 1;
        }

        // 比较完成。如果长度不相等，则较长者较大
        if (count != length) return count > length ? 1 : -1;

        return 0;
    }

    /// <summary>
    /// 字节数组分割
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="sps"></param>
    /// <returns></returns>
    public static IEnumerable<byte[]> Split(this byte[] buf, byte[] sps)
    {
        int p;
        var idx = 0;
        while (true)
        {
            p = (int)buf.IndexOf(idx, 0, sps);
            if (p < 0) break;

            yield return buf.ReadBytes(idx, p);

            idx += p + sps.Length;
        }

        if (idx < buf.Length)
        {
            p = buf.Length - idx;
            yield return buf.ReadBytes(idx, p);
        }
    }

    #region 十六进制编码

    /// <summary>
    /// 把字节数组编码为十六进制字符串
    /// </summary>
    /// <param name="data">字节数组</param>
    /// <param name="offset">偏移</param>
    /// <param name="count">数量</param>
    /// <returns></returns>
    public static string ToHex(this byte[]? data, int offset = 0, int count = 0)
    {
        if (data == null || data.Length < 1) return "";

        if (count <= 0) count = data.Length - offset;

        //return BitConverter.ToString(data).Replace("-", null);
        // 上面的方法要替换-，效率太低
        var cs = new char[count * 2];

        // 两个索引一起用，避免乘除带来的性能损耗
        for (int i = 0, j = 0; i < count; i++, j += 2)
        {
            var b = data[offset + i];
            cs[j] = GetHexValue(b / 0x10);
            cs[j + 1] = GetHexValue(b % 0x10);
        }

        return new string(cs);
    }

    /// <summary>
    /// 把字节数组编码为十六进制字符串，带有分隔符和分组功能
    /// </summary>
    /// <param name="data">字节数组</param>
    /// <param name="separate">分隔符</param>
    /// <param name="groupSize">分组大小，为0时对每个字节应用分隔符，否则对每个分组使用</param>
    /// <returns></returns>
    public static string ToHex(this byte[]? data, string separate, int groupSize = 0)
    {
        if (data == null || data.Length < 1) return "";

        if (groupSize < 0) groupSize = 0;

        if (groupSize == 0)
        {
            // 没有分隔符
            if (string.IsNullOrEmpty(separate)) return data.ToHex();

            // 特殊处理
            if (separate == "-") return BitConverter.ToString(data);
        }

        var count = data.Length;
        var len = count * 2;

        if (!string.IsNullOrEmpty(separate)) len += (count - 1) * separate.Length;

        if (groupSize > 0)
        {
            // 计算分组个数
            var g = (count - 1) / groupSize;
            len += g * 2;

            // 扣除间隔
            if (!string.IsNullOrEmpty(separate)) len -= g * separate.Length;
        }

        var sb = new StringBuilder(len);

        for (var i = 0; i < count; i++)
        {
            if (sb.Length > 0)
            {
                if (i % groupSize == 0)
                    sb.AppendLine();
                else
                    sb.Append(separate);
            }

            var b = data[i];

            sb.Append(GetHexValue(b / 0x10));
            sb.Append(GetHexValue(b % 0x10));
        }

        return sb.ToString();
    }

    private static char GetHexValue(int i)
    {
        if (i < 10) return (char)(i + 0x30);
        return (char)(i - 10 + 0x41);
    }

    /// <summary>
    /// 把十六进制字符串解码字节数组
    /// </summary>
    /// <param name="data">字节数组</param>
    /// <param name="startIndex">起始位置</param>
    /// <param name="length">长度</param>
    /// <returns></returns>
    [Obsolete("ToHex")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static byte[] FromHex(this string data, int startIndex = 0, int length = 0)
    {
        return ToHex(data, startIndex, length);
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="data">Hex编码的字符串</param>
    /// <param name="startIndex">起始位置</param>
    /// <param name="length">长度</param>
    /// <returns></returns>
    public static byte[] ToHex(this string data, int startIndex = 0, int length = 0)
    {
        if (string.IsNullOrEmpty(data)) return Array.Empty<byte>();

        // 过滤特殊字符
        data = data.Trim()
            .Replace("-", null)
            .Replace("0x", null)
            .Replace("0X", null)
            .Replace(" ", null)
            .Replace("\r", null)
            .Replace("\n", null)
            .Replace(",", null);

        if (length <= 0) length = data.Length - startIndex;

        var bts = new byte[length / 2];
        for (var i = 0; i < bts.Length; i++)
            bts[i] = byte.Parse(data.Substring(startIndex + 2 * i, 2), NumberStyles.HexNumber);

        return bts;
    }

    #endregion 十六进制编码

    /// <summary>
    /// 合并两个数组
    /// </summary>
    /// <param name="src">源数组</param>
    /// <param name="des">目标数组</param>
    /// <param name="offset">起始位置</param>
    /// <param name="count">字节数</param>
    /// <returns></returns>
    public static byte[] Combine(this byte[] src, byte[] des, int offset = 0, int count = 0)
    {
        if (count <= 0) count = src.Length - offset;

        var buf = new byte[src.Length + count];
        Buffer.BlockCopy(src, 0, buf, 0, src.Length);
        Buffer.BlockCopy(des, offset, buf, src.Length, count);
        return buf;
    }

    #region Clear

    /// <summary>
    /// Clears the array completely
    /// </summary>
    /// <param name="array">Array to clear</param>
    /// <returns>The final array</returns>
    /// <example>
    /// <code>
    ///int[] TestObject = new int[] { 1, 2, 3, 4, 5, 6 };
    ///TestObject.Clear();
    /// </code>
    /// </example>
    public static Array Clear(this Array array)
    {
        Array.Clear(array, 0, array.Length);

        return array;
    }

    /// <summary>
    /// Clears the array completely
    /// </summary>
    /// <param name="array">Array to clear</param>
    /// <typeparam name="TArrayType">Array type</typeparam>
    /// <returns>The final array</returns>
    /// <example>
    /// <code>
    ///int[] TestObject = new int[] { 1, 2, 3, 4, 5, 6 };
    ///TestObject.Clear();
    /// </code>
    /// </example>
    public static TArrayType[] Clear<TArrayType>(this TArrayType[] array)
    {
        return (TArrayType[])((Array)array).Clear();
    }

    #endregion Clear

    #region Convert

    /// <summary>
    ///转换为List
    /// </summary>
    /// <typeparam name="TArrayType"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static List<TArrayType> ToList<TArrayType>(this TArrayType[] array)
    {
        var result = new List<TArrayType>();

        // ReSharper disable LoopCanBeConvertedToQuery
        foreach (var a in array)
            // ReSharper restore LoopCanBeConvertedToQuery
            result.Add(a);

        return result;
    }

    #endregion Convert

    #region Concat

    /// <summary>
    /// Combines two arrays and returns a new array containing both values
    /// </summary>
    /// <typeparam name="TArrayType">Type of the data in the array</typeparam>
    /// <param name="array1">Array 1</param>
    /// <param name="additions">Arrays to concat onto the first item</param>
    /// <returns>A new array containing both arrays' values</returns>
    /// <example>
    /// <code>
    ///int[] TestObject1 = new int[] { 1, 2, 3 };
    ///int[] TestObject2 = new int[] { 4, 5, 6 };
    ///int[] TestObject3 = new int[] { 7, 8, 9 };
    ///TestObject1 = TestObject1.Combine(TestObject2, TestObject3);
    /// </code>
    /// </example>
    public static TArrayType[] Concat<TArrayType>(this TArrayType[] array1, params TArrayType[][] additions)
    {
        if (array1.IsNull()) throw new ArgumentNullException("array1");

        if (additions.IsNull()) throw new ArgumentNullException("additions");

        var result = new TArrayType[array1.Length + additions.Sum(x => x.Length)];
        var offset = array1.Length;
        Array.Copy(array1, 0, result, 0, array1.Length);
        foreach (var t in additions)
        {
            Array.Copy(t, 0, result, offset, t.Length);
            offset += t.Length;
        }

        return result;
    }

    #endregion Concat

    #endregion Functions
}