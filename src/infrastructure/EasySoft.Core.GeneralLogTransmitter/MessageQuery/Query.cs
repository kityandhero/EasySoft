using EasySoft.Core.GeneralLogTransmitter.Interfaces;
using EasySoft.Core.RabbitMQ;

namespace EasySoft.Core.GeneralLogTransmitter.MessageQuery
{
    public class Query : BasicQuery<IGeneralLogExchange>
    {
        protected override string RouteName => "GeneralLogRoute";

        protected override string QueueName => "GeneralLogQuery";

        public static string GetQueryName()
        {
            return new Query().GetQueueName();
        }
    }
}