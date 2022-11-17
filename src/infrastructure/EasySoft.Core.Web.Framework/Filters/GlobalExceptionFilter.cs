namespace EasySoft.Core.Web.Framework.Filters;

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
        if (context.ExceptionHandled) return;

        LogAssist.GetLogger().LogError(0, context.Exception, "{Message}", context.Exception.Message);

        if (context.Exception.InnerException != null)
            LogAssist.GetLogger().LogError(
                0,
                context.Exception.InnerException,
                "{Message}",
                context.Exception.InnerException.Message
            );

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

        var result = new ApiResult(ReturnCode.Error.ToInt(), false, "服务发生错误");

        if (_hostEnvironment.IsDevelopment())
        {
            result.Message += ": " + context.Exception.Message;
            result.Data = context.Exception.StackTrace;
        }

        if (GeneralConfigAssist.GetRemoteErrorLogSwitch())
        {
            var errorLogProducer = AutofacAssist.Instance.Resolve<IErrorLogProducer>();

            var requestInfo = context.HttpContext.BuildRequestInfo();

            errorLogProducer.Send(context.Exception, 0, requestInfo);
        }

        context.Result = new JsonResult(
            result,
            JsonConvertAssist.CreateJsonSerializerSettings()
        );

        context.ExceptionHandled = true;
    }
}