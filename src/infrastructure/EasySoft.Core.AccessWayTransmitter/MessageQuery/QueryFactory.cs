using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.Core.ExchangeRegulation.Query;

namespace EasySoft.Core.AccessWayTransmitter.MessageQuery
{
    public class QueryFactory : IQueryFactory<IAccessWayExchange>
    {
        public IQuery<IAccessWayExchange> CreateQuery()
        {
            var query = new Query();

            return query;
        }
    }
}