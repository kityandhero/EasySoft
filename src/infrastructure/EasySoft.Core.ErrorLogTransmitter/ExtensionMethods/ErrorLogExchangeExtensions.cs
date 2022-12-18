using EasySoft.Core.ErrorLogTransmitter.Entities;
using EasySoft.Core.ErrorLogTransmitter.Enums;
using EasySoft.UtilityTools.Standard.Entities;

namespace EasySoft.Core.ErrorLogTransmitter.ExtensionMethods;

/// <summary>
/// ErrorLogExchangeExtensions
/// </summary>
public static class ErrorLogExchangeExtensions
{
    /// <summary>
    /// Fill
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="ex"></param>
    /// <param name="operatorId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static ErrorLogExchange Fill(
        this ErrorLogExchange entity,
        Exception ex,
        long operatorId = 0,
        RequestInfo? request = null
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

            entity.UserId = operatorId;
            entity.Type = ErrorLogExchangeType.Exception.ToInt();
            entity.Degree = ErrorLogExchangeDegree.Error.ToInt();
            entity.CreateBy = 0;
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