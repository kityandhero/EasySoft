using EasySoft.UtilityTools.Core.Results.Implements;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// ApiResultExtensions
/// </summary>
public static class ApiResultExtensions
{
    /// <summary>
    /// ToExpandoObject
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static object ToExpandoObject(this ApiResult result)
    {
        dynamic data = new ExpandoObject();

        data.code = result.Code;
        data.success = result.Success;
        data.message = result.Message;

        if (result.Data != null)
        {
            if (result.Data is JObject jData)
            {
                data.data = jData;
            }
            else
            {
                IEnumerable? list = result.Data as IList;

                if (list != null)
                {
                    data.list = list;
                }
                else
                {
                    data.data = result.Data;
                }
            }
        }

        if (result.ExtraData != null)
        {
            data.extra = result.ExtraData;
        }

        return data;
    }

    /// <summary>
    /// SerializeAndKeyToLower
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static string SerializeAndKeyToLower(this ApiResult result)
    {
        return JsonConvertAssist.SerializeAndKeyToLower(result.ToExpandoObject());
    }
}