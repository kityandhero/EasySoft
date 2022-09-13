﻿using System.Collections;
using System.Dynamic;
using EasySoft.UtilityTools.Core.Results;
using EasySoft.UtilityTools.Standard.Assists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;

namespace EasySoft.Core.Web.Framework.Attributes;

public sealed class WebApiResultFilterAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ApiResult result)
        {
            dynamic d = new ExpandoObject();

            d.code = result.Code;
            d.success = result.Success;
            d.message = result.Message;

            if (result.Data != null)
            {
                if (result.Data is JObject jData)
                {
                    d.data = jData;
                }
                else
                {
                    IEnumerable? list = result.Data as IList;

                    if (list != null)
                    {
                        d.list = list;
                    }
                    else
                    {
                        d.data = result.Data;
                    }
                }
            }

            if (result.ExtraData != null)
            {
                d.extra = result.ExtraData;
            }

            context.Result = new JsonResult(
                d,
                JsonConvertAssist.CreateJsonSerializerSettings(result.CamelCase)
            );
        }
        else
        {
            base.OnResultExecuting(context);
        }
    }
}