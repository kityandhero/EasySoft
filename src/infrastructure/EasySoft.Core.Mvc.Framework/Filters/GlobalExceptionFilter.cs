using EasySoft.Core.Mvc.Framework.Results;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.Enums;

namespace EasySoft.Core.Mvc.Framework.Filters;

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

        context.Result = new JsonResult(
            result,
            JsonConvertAssist.CreateJsonSerializerSettings()
        );

        context.ExceptionHandled = true; //异常已处理
    }
}