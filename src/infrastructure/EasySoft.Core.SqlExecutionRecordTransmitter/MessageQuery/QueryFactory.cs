using EasySoft.Core.ExchangeRegulation.Query;
using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.MessageQuery
{
    public class QueryFactory : IQueryFactory<ISqlExecutionRecordExchange>
    {
        public IQuery<ISqlExecutionRecordExchange> CreateQuery()
        {
            var query = new Query();

            return query;
        }
    }
}