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

using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// Extensions for NameValueCollection
/// </summary>
public static class NameValueCollectionExtensions
{
    /// <summary>
    /// ToJson 将NameValueCollection按照"键":"值"的形式构建成Json字符串
    /// </summary>
    /// <param name="nvc"></param>
    /// <returns></returns>
    public static string ToJson(this NameValueCollection nvc)
    {
        return ConvertAssist.NameValueCollectionToJson(nvc);
    }

    /// <summary>
    /// 判断两个NameValueCollection的内容是否相同
    /// </summary>
    /// <param name="nvc1"></param>
    /// <param name="nvc2"></param>
    /// <returns></returns>
    public static bool ContentEqual(this NameValueCollection nvc1, NameValueCollection nvc2)
    {
        return nvc1.AllKeys.OrderBy(key => key).SequenceEqual(nvc2.AllKeys.OrderBy(key => key)) &&
               nvc1.AllKeys.All(key => nvc1[key] == nvc2[key]);
    }

    /// <summary>
    /// 合并
    /// </summary>
    /// <param name="nv">    </param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static NameValueCollection Merge(this NameValueCollection nv, NameValueCollection target)
    {
        foreach (var key in target.AllKeys) nv[key] = target[key];

        return nv;
    }

    /// <summary>
    /// Converts the NameValueCollection to a query string
    /// </summary>
    /// <param name="source">Input</param>
    /// <returns>The NameValueCollection expressed as a string</returns>
    public static string ToQueryString(this NameValueCollection source)
    {
        if (source.IsNull()) throw new ArgumentNullException(nameof(source));

        if (source.Count <= 0) return "";

        var builder = new StringBuilder();

        builder.Append('?');

        var splitter = "";

        foreach (string key in source.Keys)
        {
            builder.Append(splitter).Append($"{key.UrlEncode()}={source[key].UrlEncode()}");

            splitter = "&";
        }

        return builder.ToString();
    }
}