using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using EasySoft.UtilityTools.Standard.Entity;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

public static class ControllerBaseExtensions
{
    /// <summary>
    /// 获取整合参数并转换为NameValueCollection
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public static async Task<NameValueCollection> GetIntegratedParamsAsync(this ControllerBase controller)
    {
        var request = controller.Request;

        return await request.GetIntegratedParamsAsync();
    }

    /// <summary>
    /// 获取整合参数所有name集合
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public static async Task<List<string>> GetAllParamNamesAsync(this ControllerBase controller)
    {
        var nv = await GetIntegratedParamsAsync(controller);

        return nv.AllKeys.ToListFilterNullable();
    }

    /// <summary>
    /// 检测指定的参数名是否存在
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static async Task<bool> ExistParamNameAsync(this ControllerBase controller, string name)
    {
        var names = await GetAllParamNamesAsync(controller);

        return names.Contains(name);
    }

    public static async Task<string> GetParamValueAsync(this ControllerBase controller, string param)
    {
        var request = controller.Request;

        var nv = await request.GetIntegratedParamsAsync();

        var result = "";

        if (nv.AllKeys.ToList().Contains(param)) result = nv[param];

        return result ?? "";
    }

    public static RequestInfo BuildRequestInfo(this ControllerBase controller)
    {
        return controller.HttpContext.BuildRequestInfo();
    }

    public static string GetCookie(this ControllerBase controller, string key)
    {
        return controller.HttpContext.Request.Cookies[key] ?? "";
    }

    public static void SetCookie(this ControllerBase controller, string key, string value)
    {
        controller.HttpContext.SetCookie(key, value, new CookieOptions());
    }

    public static void SetCookie(this ControllerBase controller, string key, string value, CookieOptions options)
    {
        controller.HttpContext.Response.Cookies.Append(key, value, options);
    }
}