using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Producers;

namespace EasySoft.Core.Web.Framework.Kernel.Filters;

/// <summary>
/// GlobalExceptionFilter
/// </summary>
public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _hostEnvironment;

    public GlobalExceptionFilter(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled)
        {
            return;
        }

        LogAssist.GetLogger()
            .LogError(
                0,
                context.Exception,
                "{Message}",
                context.Exception.Message
            );

        if (context.Exception.InnerException != null)
        {
            LogAssist.GetLogger()
                .LogError(
                    0,
                    context.Exception.InnerException,
                    "{Message}",
                    context.Exception.InnerException.Message
                );
        }

        if (context.Exception is TokenException)
        {
            var resultTokenExpired = new ApiResult(
                ReturnCode.TokenExpired.ToInt(),
                false,
                context.Exception.Message
            );

            context.Result = new JsonResult(
                resultTokenExpired,
                JsonConvertAssist.CreateJsonSerializerSettings()
            );

            context.ExceptionHandled = true;

            return;
        }

        var result = new ApiResult(
            ReturnCode.Error.ToInt(),
            false,
            "服务发生错误"
        );

        if (_hostEnvironment.IsDevelopment())
        {
            result.Message += ": " + context.Exception.Message;
            result.Data = context.Exception.StackTrace;
        }

        var infrastructureConfigureAssist = ServiceProviderAssist.Resolve<InfrastructureConfigureAssist>();

        var transmissionErrorConfigValue = infrastructureConfigureAssist.GetUseErrorLogTransceiver();

        if (transmissionErrorConfigValue.Value)
        {
            var errorLogProducer = ServiceProviderAssist.Resolve<IErrorLogProducer>();

            var requestInfo = context.HttpContext.BuildRequestInfo();

            errorLogProducer.SendAsync(
                context.Exception,
                0,
                requestInfo
            );
        }

        context.Result = new JsonResult(
            result,
            JsonConvertAssist.CreateJsonSerializerSettings()
        );

        context.ExceptionHandled = true;
    }
}