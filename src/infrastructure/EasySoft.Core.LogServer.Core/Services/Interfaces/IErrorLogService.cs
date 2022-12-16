namespace EasySoft.Core.LogServer.Core.Services.Interfaces;

/// <summary>
/// ISecurityService
/// </summary>
public interface IErrorLogService : IBusinessService
{
    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="errorLogExchange"></param>
    /// <returns></returns>
    Task SaveAsync(ErrorLogExchange errorLogExchange);
}