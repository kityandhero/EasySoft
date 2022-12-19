using EasySoft.UtilityTools.Core.Exceptions;
using EasySoft.UtilityTools.Core.Results;
using EasySoft.UtilityTools.Standard.Entities;
using EasySoft.UtilityTools.Standard.Entities.Implementations;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// ControllerBaseExtensions
/// </summary>
public static class ControllerBaseExtensions
{
    /// <summary>
    /// Wrapper ExecutiveResult
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static IApiResult WrapperExecutiveResult(
        this ControllerBase controller,
        ExecutiveResult result
    )
    {
        if (!result.Success) return controller.Fail(result.Code);

        return controller.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// Wrapper ExecutiveResult
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static IApiResult WrapperExecutiveResult(
        this ControllerBase controller,
        ExecutiveResult<object> result
    )
    {
        return !result.Success ? controller.Fail(result.Code) : controller.Success(result.Data);
    }

    /// <summary>
    /// Wrapper ExecutiveResult
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static IApiResult WrapperExecutiveResult(
        this ControllerBase controller,
        ExecutiveResult<ExpandoObject> result
    )
    {
        return !result.Success ? controller.Fail(result.Code) : controller.Success(result.Data);
    }

    /// <summary>
    /// Wrapper ExecutiveResult
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="result"></param>
    /// <param name="keyToLower"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IApiResult WrapperExecutiveResult<T>(
        this ControllerBase controller,
        ExecutiveResult<T> result,
        bool keyToLower = true
    )
    {
        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(keyToLower ? result.Data.ToExpandoObject() : result.Data);
    }

    /// <summary>
    /// Wrapper ExecutiveResult
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="result"></param>
    /// <param name="dataHandler"></param>
    /// <param name="extra"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IApiResult WrapperExecutiveResult<T>(
        this ControllerBase controller,
        ExecutiveResult<T> result,
        Func<T, object> dataHandler,
        object? extra = null
    )
    {
        var data = result.Data != null ? dataHandler(result.Data) : null;

        return !result.Success
            ? controller.Fail(result.Code)
            : controller.Success(data, extra, false);
    }

    /// <summary>
    /// Data
    /// </summary>
    private static IApiResult Data(
        this ControllerBase controller,
        ReturnCode code = ReturnCode.Ok,
        bool success = true,
        string message = "success",
        object? data = null,
        object? extraData = null,
        bool camelCase = true
    )
    {
        var result = new ApiResult(code, success, message, data, extraData);

        result.SetCamelCase(camelCase);

        return result;
    }

    /// <summary>
    /// Success
    /// </summary>
    public static IApiResult Success(
        this ControllerBase controller,
        object? data = null,
        object? extraData = null,
        bool camelCase = true
    )
    {
        return controller.Data(ReturnCode.Ok, true, "success", data, extraData, camelCase);
    }

    /// <summary>
    /// Fail
    /// </summary>
    public static IApiResult Fail(
        this ControllerBase controller,
        ReturnCode code,
        string message = "",
        object? data = null,
        object? extraData = null
    )
    {
        var messageAdjust = string.IsNullOrWhiteSpace(message) ? code.ToMessage().Message : message;

        return controller.Data(code, false, messageAdjust, data, extraData);
    }

    /// <summary>
    /// Fail
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="returnMessage"></param>
    /// <param name="data"></param>
    /// <param name="extraData"></param>
    /// <returns></returns>
    public static IApiResult Fail(
        this ControllerBase controller,
        ReturnMessage returnMessage,
        object? data = null,
        object? extraData = null
    )
    {
        return controller.Data((ReturnCode)returnMessage.Code, false, returnMessage.Message, data, extraData);
    }

    /// <summary>  
    /// PagedData
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="list">      数据列表</param>
    /// <param name="extraData"> 额外数据</param>
    /// <param name="camelCase"></param>
    public static IApiResult PagedData(
        this ControllerBase controller,
        List<object> list,
        object? extraData = null,
        bool camelCase = true
    )
    {
        return controller.Success(list, extraData, camelCase);
    }

    /// <summary>
    /// PagedData
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="list">      数据列表</param>
    /// <param name="pageNo">    页码</param>
    /// <param name="pageSize">  分页大小</param>
    /// <param name="total">     总数据数</param>
    /// <param name="other">     </param>
    /// <param name="camelCase"></param>
    public static IApiResult PagedData(
        this ControllerBase controller,
        List<object> list,
        int pageNo,
        int pageSize,
        long total,
        object? other = null,
        bool camelCase = true
    )
    {
        var extra = other == null
            ? (object)new
            {
                pageNo,
                pageSize,
                total
            }
            : new
            {
                pageNo,
                pageSize,
                total,
                other
            };

        return controller.PagedData(list, extra, camelCase);
    }

    /// <summary>
    /// 获取字符串参数
    /// </summary>
    /// <param name="c">           </param>
    /// <param name="param">       参数名称</param>
    /// <param name="pattern">     参数值验证正则表达式</param>
    /// <param name="minLength">   最小长度</param>
    /// <param name="maxLength">   最大大度</param>
    /// <param name="required">    是否必须</param>
    /// <param name="defaultValue">默认值</param>
    public static async Task<string> ParamAsync(
        this ControllerBase c,
        string param,
        string pattern = "^.*$",
        int minLength = 0,
        int maxLength = int.MaxValue,
        bool required = true,
        string defaultValue = ""
    )
    {
        var errors = new List<string>(4);
        var value = await c.GetParamValueAsync(param);

        if (value == string.Empty && defaultValue != string.Empty) value = defaultValue;

        if (string.IsNullOrWhiteSpace(value) && required)
        {
            if (value.Length < minLength) errors.Add($"[{param}]至少需要{minLength}个字符");

            if (value.Length > maxLength) errors.Add($"[{param}]不能多于{maxLength}个字符");

            if (!Regex.IsMatch(value, pattern)) errors.Add($"[{param}]输入字符格式不正确");
        }

        if (errors.Count > 0) throw new ParamException(param, true, errors.ToArray());

        return value;
    }

    /// <summary>
    /// 获取值类型参数，使用3个参数时请注意重载版本！
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="c">           </param>
    /// <param name="param">       参数名称</param>
    /// <param name="min">         最小值</param>
    /// <param name="max">         最大值</param>
    /// <param name="defaultValue">默认值</param>
    public static async Task<T> ParamAsync<T>(
        this ControllerBase c,
        string param,
        T min,
        T max,
        T defaultValue
    ) where T : struct
    {
        var errors = new List<string>(4);

        var input = await c.GetParamValueAsync(param);
        var value = defaultValue;

        if (string.IsNullOrEmpty(input))
        {
            input = null;
            value = defaultValue;
        }

        if (input != null)
            try
            {
                value = input.ConvertTo<T>();
            }
            catch
            {
                value = defaultValue;
            }

        if (value is IComparable<T> cValue)
        {
            if (cValue.CompareTo(min) < 0) errors.Add($"[{param}]不应小于{min}");

            if (cValue.CompareTo(max) > 0) errors.Add($"[{param}]不应大于{max}");
        }

        if (errors.Count > 0) throw new ParamException(param, true, errors.ToArray());

        return value;
    }

    /// <summary>
    /// 获取值类型参数
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="c"></param>
    /// <param name="param">       参数名称</param>
    /// <param name="min">         最小值</param>
    /// <param name="defaultValue">默认值</param>
    public static async Task<T> ParamAsync<T>(
        this ControllerBase c,
        string param,
        T min,
        T defaultValue = default
    ) where T : struct
    {
        var errors = new List<string>(4);

        var input = await c.GetParamValueAsync(param);
        var value = defaultValue;

        if (string.IsNullOrEmpty(input))
        {
            input = null;
            value = defaultValue;
        }

        if (input != null)
            try
            {
                value = input.ConvertTo<T>();
            }
            catch
            {
                value = defaultValue;
            }

        if (value is IComparable<T> cValue)
            if (cValue.CompareTo(min) < 0)
                errors.Add($"值不应小于{min}");

        if (errors.Count > 0) throw new ParamException(param, true, errors.ToArray());

        return value;
    }

    /// <summary>
    /// 验证字符串参数
    /// </summary>
    /// <param name="c"></param>
    /// <param name="param">    参数名称</param>
    /// <param name="value">    参数值</param>
    /// <param name="pattern">  参数值验证正则表达式</param>
    /// <param name="minLength">最小长度</param>
    /// <param name="maxLength">最大大度</param>
    /// <param name="required"> 是否必须</param>
    /// <param name="canEmpty"></param>
    public static void Check(
        this ControllerBase c,
        string param,
        string value,
        string pattern = "^.*$",
        int minLength = 0,
        int maxLength = int.MaxValue,
        bool required = true,
        bool canEmpty = true
    )
    {
        var errors = new List<string>(4);

        if (value == null && required) throw new ParamException(param, $"[{param}]不能为空");

        if (value == "" && canEmpty) return;

        if (value != null)
        {
            if (value.Length < minLength) errors.Add($"[{param}]至少需要{minLength}个字符");

            if (value.Length > maxLength) errors.Add($"[{param}]不能多于{maxLength}个字符");

            if (!Regex.IsMatch(value, pattern)) errors.Add($"[{param}]输入字符格式不正确");
        }

        if (errors.Count > 0) throw new ParamException(param, true, errors.ToArray());
    }

    /// <summary>
    /// 验证密码
    /// </summary>
    /// <param name="c"></param>
    /// <param name="param">参数名称</param>
    /// <param name="value">参数值</param>
    /// <param name="pattern"></param>
    public static void CheckPassword(this ControllerBase c, string param, string value, string pattern = "^.*$")
    {
        if (string.IsNullOrEmpty(value) || value.Length < 6) throw new ParamException(param, "密码需要至少6个字符！", true);

        if (!Regex.IsMatch(value, pattern)) throw new ParamException(param, "请使用多种字符组合的密码！", true);
    }

    /// <summary>
    /// 验证值类型
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="c"></param>
    /// <param name="param">参数名称</param>
    /// <param name="value">参数值</param>
    /// <param name="min">  最小值</param>
    public static void Check<T>(this ControllerBase c, string param, T value, T min)
    {
        var error = "";

        var cValue = value as IComparable<T>;

        if (cValue?.CompareTo(min) < 0) error = $"[{param}]不应小于{min}";

        if (error != null) throw new ParamException(param, true, new[] { error });
    }

    /// <summary>
    /// 验证值类型
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="c"></param>
    /// <param name="param">参数名称</param>
    /// <param name="value">参数值</param>
    /// <param name="min">  最小值</param>
    /// <param name="max">  最大值</param>
    public static void Check<T>(this ControllerBase c, string param, T value, T min, T max)
    {
        var errors = new List<string>(4);

        if (value is IComparable<T> cValue)
        {
            if (cValue.CompareTo(min) < 0) errors.Add($"[{param}]不应小于{min}");

            if (cValue.CompareTo(max) > 0) errors.Add($"[{param}]不应大于{max}");
        }

        if (errors.Count > 0) throw new ParamException(param, true, errors.ToArray());
    }

    /// <summary>
    /// 验证值类型范围
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="c"></param>
    /// <param name="param">参数名称</param>
    /// <param name="value">参数值</param>
    /// <param name="list"> 值范围列表</param>
    public static void CheckInList<T>(this ControllerBase c, string param, T value, params T[] list)
    {
        var error = "";

        if (!list.ToList().Contains(value)) error = $"[{param}]不在指定范围";

        if (error != null) throw new ParamException(param, true, error);
    }

    /// <summary>
    /// 验证值类型大小
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="c"></param>
    /// <param name="param1">参数1名称</param>
    /// <param name="param2">参数2名称</param>
    /// <param name="value1">参数1值</param>
    /// <param name="value2">参数2值</param>
    public static void CheckGreaterThan<T>(this ControllerBase c, string param1, string param2, T value1, T value2)
    {
        var error = "";

        var cValue = value1 as IComparable<T>;

        if (cValue?.CompareTo(value2) <= 0) error = $"[{param1}]不应小于[{param2}]";

        if (error != null) throw new ParamException(param1, true, new string[] { error });
    }

    /// <summary>
    /// Get Host
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static string GetHost(this ControllerBase c)
    {
        return c.Request.GetHost();
    }

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

    /// <summary>
    /// Get Param Value 
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<string> GetParamValueAsync(this ControllerBase controller, string param)
    {
        var request = controller.Request;

        var nv = await request.GetIntegratedParamsAsync();

        var result = "";

        if (nv.AllKeys.ToList().Contains(param)) result = nv[param];

        return result ?? "";
    }

    /// <summary>
    /// Build Request Info
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public static IRequestInfo BuildRequestInfo(this ControllerBase controller)
    {
        return controller.HttpContext.BuildRequestInfo();
    }

    /// <summary>
    /// Get Cookie
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetCookie(this ControllerBase controller, string key)
    {
        return controller.HttpContext.Request.Cookies[key] ?? "";
    }

    /// <summary>
    /// Set Cookie
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetCookie(this ControllerBase controller, string key, string value)
    {
        controller.HttpContext.SetCookie(key, value, new CookieOptions());
    }

    /// <summary>
    /// Set Cookie
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public static void SetCookie(this ControllerBase controller, string key, string value, CookieOptions options)
    {
        controller.HttpContext.Response.Cookies.Append(key, value, options);
    }
}