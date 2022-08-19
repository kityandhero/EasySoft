﻿using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ErrorLogTransmitter.Producers;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.Core.Web.Framework.Results;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.Entity;
using EasySoft.UtilityTools.Enums;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Web.Framework.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly ILog _logger;

    public GlobalExceptionFilter(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
        _logger = LogManager.GetLogger(typeof(GlobalExceptionFilter));
    }

    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled)
        {
            return;
        }

        var result = new ApiResult(ReturnCode.Error.ToInt(), false, "服务器发生未处理的异常");

        if (_hostEnvironment.IsDevelopment())
        {
            result.Message += "," + context.Exception.Message;
            result.Data = context.Exception.StackTrace;
        }

        _logger.Error(result);

        if (GeneralConfigAssist.GetRemoteErrorLogEnable())
        {
            var errorLogProducer = AutofacAssist.Instance.Resolve<IErrorLogProducer>();

            var requestInfo = new RequestInfo
            {
                Host = context.HttpContext.Request.GetHost(),
                Url = context.HttpContext.Request.GetUrl(),
                UrlParams = JsonConvertAssist.Serialize(context.HttpContext.Request.GetUrlParams()),
                Header = JsonConvertAssist.Serialize(context.HttpContext.Request.GetHeaders()),
                FormParam = JsonConvertAssist.Serialize(context.HttpContext.Request.GetFromParams()),
                PayloadParam = JsonConvertAssist.Serialize(context.HttpContext.Request.GetPayloadParams()),
            };

            errorLogProducer.Send(context.Exception, 0, requestInfo);
        }

        context.Result = new JsonResult(
            result,
            JsonConvertAssist.CreateJsonSerializerSettings()
        );

        context.ExceptionHandled = true; //异常已处理
    }
}