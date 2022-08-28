using EasySoft.Core.RabbitMQ;
using EasySoft.Core.SqlExecutionRecordTransmitter.Interfaces;

namespace EasySoft.Core.SqlExecutionRecordTransmitter.MessageQuery
{
    public class Query : BasicQuery<ISqlExecutionRecordExchange>
    {
        protected override string RouteName => "SqlExecutionRecordRoute";

        protected override string QueueName => "SqlExecutionRecordQuery";

        public static string GetQueryName()
        {
            return new Query().GetQueueName();
        }
    }
}