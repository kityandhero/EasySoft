using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Core.ExchangeRegulation.Query;

/// <summary>
/// IQueryFactory
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IQueryFactory<in T> where T : IExchangeEntity
{
    /// <summary>
    /// CreateQuery
    /// </summary>
    /// <returns></returns>
    IQuery<T> CreateQuery();
}