using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ErrorLogExtensions
/// </summary>
public static class ErrorLogExtensions
{
    /// <summary>
    /// 设置消息
    /// </summary>
    /// <param name="errorLog"></param>
    /// <param name="message"></param>
    public static void SetMessage(this IErrorLog errorLog, string message)
    {
        errorLog.Message = message ?? "";
    }

    /// <summary>
    /// 设置 OperatorId
    /// </summary>
    /// <param name="errorLog"></param>
    /// <param name="operatorId">用户标识</param>
    public static void SetOperatorId(this IErrorLog errorLog, long operatorId)
    {
        errorLog.OperatorId = operatorId;
    }

    /// <summary>
    /// 设置 Data
    /// </summary>
    /// <param name="errorLog"></param>
    /// <param name="data"></param>
    public static void SetData(this IErrorLog errorLog, string data)
    {
        errorLog.CustomData = data ?? "";
    }

    /// <summary>
    /// 填充信息
    /// </summary>
    /// <param name="errorLog"></param>
    /// <param name="ex"></param>
    /// <param name="operatorId">用户标识</param>
    /// <param name="requestInfo"></param>
    public static void Fill(
        this IErrorLog errorLog,
        UnhandledException ex,
        long operatorId = 0,
        IRequestInfo requestInfo = null
    )
    {
        errorLog.Fill(
            (Exception)ex,
            operatorId,
            requestInfo
        );

        errorLog.Description = ex.Description ?? "";
        errorLog.AncillaryInformation = ex.AncillaryInformation ?? "";
    }

    /// <summary>
    /// 填充信息
    /// </summary>
    /// <param name="errorLog"></param>
    /// <param name="ex"></param>
    /// <param name="operatorId">用户标识</param>
    /// <param name="request"></param>
    public static void Fill(
        this IErrorLog errorLog,
        Exception ex,
        long operatorId = 0,
        IRequestInfo request = null
    )
    {
        var url = "";
        var urlParams = "";
        var host = "";
        var port = 80;

        try
        {
            if (request != null)
            {
                if (request.Url != null)
                {
                    url = request.Url;
                    urlParams = ConvertAssist.NameValueCollectionToJson(request.GetUrlParams());
                    urlParams = urlParams == "[]" ? "" : urlParams;

                    if (request.Url != null)
                    {
                        host = request.GetHost();
                        port = request.GetPort();
                    }
                }

                var header = ConvertAssist.NameValueCollectionToJson(request.GetHeaders());
                var formParam = ConvertAssist.NameValueCollectionToJson(request.GetFromParams());

                formParam = formParam == "[]" ? "" : formParam ?? "";

                string payloadParam;

                try
                {
                    payloadParam = ConvertAssist.NameValueCollectionToJson(request.GetPayloadParams());
                    payloadParam = payloadParam == "[]" ? "" : payloadParam ?? "";
                }
                catch (Exception e)
                {
                    payloadParam = JsonConvertAssist.SerializeObject(
                        new List<object>
                        {
                            new
                            {
                                error = e.Message ?? ""
                            }
                        }
                    );
                }

                errorLog.Url = url;
                errorLog.UrlParams = urlParams ?? "";
                errorLog.Header = header ?? "";
                errorLog.Host = host;
                errorLog.Port = port;
                errorLog.FormParams = formParam ?? "";
                errorLog.PayloadParams = payloadParam ?? "";
            }

            errorLog.FillException(ex);

            errorLog.OperatorId = operatorId;
            errorLog.Type = (int)ErrorLogType.Exception;
            errorLog.TypeNote = ErrorLogType.Exception.GetCustomAttribute<DescriptionAttribute>().Description ?? "";
            errorLog.Degree = (int)ErrorLogDegree.Error;
            errorLog.DegreeNote = ErrorLogDegree.Error.GetCustomAttribute<DescriptionAttribute>().Description ?? "";
        }
        catch (Exception e)
        {
            errorLog.Message = e.Message ?? "";
            errorLog.StackTrace = e.StackTrace ?? "";
            errorLog.Source = e.Source ?? "";
        }
    }

    /// <summary>
    /// 填充信息
    /// </summary>
    /// <param name="errorLog"></param>
    /// <param name="ex"></param>
    public static void FillException(
        this IErrorLog errorLog,
        Exception ex
    )
    {
        if (ex == null)
        {
            return;
        }

        errorLog.ExceptionTypeName = ex.GetType().Name;
        errorLog.ExceptionTypeFullName = ex.GetType().FullName ?? "";
        errorLog.Message = ex.Message ?? "";
        errorLog.StackTrace = ex.StackTrace ?? "";
        errorLog.Source = ex.Source ?? "";

        if (ex.Data.Keys.Count <= 0)
        {
            return;
        }

        errorLog.CustomData = JsonConvertAssist.SerializeObject(ex.Data);
        errorLog.CustomDataType = (int)CustomValueType.JsonObject;
    }
}