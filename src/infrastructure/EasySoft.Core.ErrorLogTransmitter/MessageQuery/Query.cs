using EasySoft.Core.ErrorLogTransmitter.Interfaces;
using EasySoft.Core.RabbitMQ;

namespace EasySoft.Core.ErrorLogTransmitter.MessageQuery
{
    public class Query : BasicQuery<IErrorLogExchange>
    {
        protected override string RouteName => "ErrorLogRoute";

        protected override string QueueName => "ErrorLogQuery";

        public static string GetQueryName()
        {
            return new Query().GetQueueName();
        }
    }
}