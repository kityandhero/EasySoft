using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.ExchangeRegulation.Query;

/// <summary>
/// IQuery
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IQuery<in T> where T : IQueueMessage
{
    /// <summary>
    /// Send
    /// </summary>
    /// <param name="entity"></param>
    void Send(T entity);
}