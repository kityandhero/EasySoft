using EasySoft.Core.ExchangeRegulation.Query;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;

namespace EasySoft.Core.GeneralLogTransmitter.MessageQuery
{
    public class QueryFactory : IQueryFactory<IGeneralLogExchange>
    {
        public IQuery<IGeneralLogExchange> CreateQuery()
        {
            var query = new Query();

            return query;
        }
    }
}