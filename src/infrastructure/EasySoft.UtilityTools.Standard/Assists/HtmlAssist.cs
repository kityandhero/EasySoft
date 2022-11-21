using System.Text.RegularExpressions;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// HtmlAssist
/// </summary>
public static class HtmlAssist
{
    ///<summary> 取得HTML中所有图片的 URL。 </summary>
    /// <param name="sHtmlText">HTML代码</param>
    /// <returns>图片的URL列表</returns>
    public static ArrayList GetHtmlImageUrlList(string sHtmlText)
    {
        // 定义正则表达式用来匹配 img 标签
        var regImg =
            new Regex(
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>",
                RegexOptions.IgnoreCase
            );

        // 搜索匹配的字符串
        var matches = regImg.Matches(sHtmlText);

        var objArrayList = new ArrayList();

        // 取得匹配项列表
        foreach (Match match in matches)
        {
            ////判断文件是否存在
            //if (System.IO.File.Exists("D:/WebSite/www.liangzivip.com/manage.yurukeji.com.cn" + match.Groups["imgUrl"].Value.Replace(CustomGlobals.ImageHost, "")))
            //{
            //    objArrayList.Add(match.Groups["imgUrl"].Value);
            //}
            var url = match.Groups["imgUrl"].Value;

            objArrayList.Add(url);
            // objArrayList.Add(url.Replace(".yurukeji.com.cn", ".yurukeji.cn"));
        }

        return objArrayList;
    }
}