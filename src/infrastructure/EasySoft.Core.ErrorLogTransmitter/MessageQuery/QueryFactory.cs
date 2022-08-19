using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.Core.ExchangeRegulation.Query;

namespace EasySoft.Core.ErrorLogTransmitter.MessageQuery
{
    public class QueryFactory : IQueryFactory<IErrorLogExchange>
    {
        public IQuery<IErrorLogExchange> CreateQuery()
        {
            var query = new Query();

            return query;
        }
    }
}