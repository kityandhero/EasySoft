using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ErrorLogMessageExtensions
/// </summary>
public static class ErrorLogMessageExtensions
{
    /// <summary>
    /// Fill
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="ex"></param>
    /// <param name="operatorId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static IErrorLogMessage Fill(
        this IErrorLogMessage entity,
        Exception ex,
        long operatorId = 0,
        IRequestInfo? request = null
    )
    {
        try
        {
            entity.ExceptionTypeName = ex.GetType().Name;
            entity.ExceptionTypeFullName = ex.GetType().FullName ?? "";
            entity.Message = ex.Message;
            entity.StackTrace = ex.StackTrace ?? "";
            entity.Source = ex.Source ?? "";

            if (ex.Data.Keys.Count > 0)
            {
                entity.CustomData = JsonConvert.SerializeObject(ex.Data);
                entity.CustomDataType = (int)CustomValueType.JsonObject;
            }

            if (request != null)
            {
                entity.Url = request.Url;
                entity.UrlParams = request.UrlParams;
                entity.Header = request.Header;
                entity.Host = request.Host;
                entity.Port = request.Port;
                entity.FormParams = request.FormParam;
                entity.PayloadParams = request.PayloadParam;
            }

            entity.OperatorId = operatorId;
            entity.Type = ErrorLogType.Exception.ToInt();
            entity.Degree = ErrorLogDegree.Error.ToInt();
        }
        catch (Exception e)
        {
            entity.Message = e.Message;
            entity.StackTrace = e.StackTrace ?? "";
            entity.Source = e.Source ?? "";
        }

        return entity;
    }
}