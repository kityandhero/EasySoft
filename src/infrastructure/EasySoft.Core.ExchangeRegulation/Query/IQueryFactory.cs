using EasySoft.Core.ExchangeRegulation.Interfaces;

namespace EasySoft.Core.ExchangeRegulation.Query
{
    public interface IQueryFactory<T> where T : IExchangeEntity
    {
        IQuery<T> CreateQuery();
    }
}