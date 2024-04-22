using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.ExchangeRegulation.Query;

/// <summary>
/// IQueryFactory
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IQueryFactory<in T> where T : IQueueMessage
{
    /// <summary>
    /// CreateQuery
    /// </summary>
    /// <returns></returns>
    IQuery<T> CreateQuery();
}