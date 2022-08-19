using EasySoft.Core.ExchangeRegulation.Interfaces;

namespace EasySoft.Core.ExchangeRegulation.Query
{
    public interface IQuery<T> where T : IExchangeEntity
    {
        void Send(T entity);
    }
}