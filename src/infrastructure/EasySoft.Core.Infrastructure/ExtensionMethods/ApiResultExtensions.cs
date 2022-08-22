using System.Collections;
using System.Dynamic;
using EasySoft.Core.Infrastructure.Results;
using EasySoft.UtilityTools.Standard.Assists;
using Newtonsoft.Json.Linq;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class ApiResultExtensions
{
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

    public static string SerializeAndKeyToLower(this ApiResult result)
    {
        return JsonConvertAssist.SerializeAndKeyToLower(result.ToExpandoObject());
    }
}