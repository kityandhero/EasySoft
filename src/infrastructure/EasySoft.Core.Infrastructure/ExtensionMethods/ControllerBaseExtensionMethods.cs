using System.Collections.Specialized;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class ControllerBaseExtensionMethods
{
    /// <summary>
    /// 获取整合参数并转换为NameValueCollection
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static NameValueCollection GetIntegratedParams(this ControllerBase c)
    {
        var request = c.Request;

        return request.GetIntegratedParams();
    }

    /// <summary>
    /// 获取整合参数所有name集合
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static List<string> GetAllParamNames(this ControllerBase c)
    {
        var nv = GetIntegratedParams(c);

        return nv.AllKeys.ToListFilterNullable();
    }

    /// <summary>
    /// 检测指定的参数名是否存在
    /// </summary>
    /// <param name="c"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool ExistParamName(this ControllerBase c, string name)
    {
        var names = GetAllParamNames(c);

        return names.Contains(name);
    }

    public static string GetParamValue(this ControllerBase c, string param)
    {
        var request = c.Request;

        var nv = request.GetIntegratedParams();

        var result = "";

        if ((nv.AllKeys.Contains(param)))
        {
            result = nv[param];
        }

        return result ?? "";
    }
}