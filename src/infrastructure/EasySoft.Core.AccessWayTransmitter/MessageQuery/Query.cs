using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.Core.RabbitMQ;

namespace EasySoft.Core.AccessWayTransmitter.MessageQuery
{
    public class Query : BasicQuery<IAccessWayExchange>
    {
        protected override string RouteName => "AccessWayRoute";

        protected override string QueueName => "AccessWayQuery";

        public static string GetQueryName()
        {
            return new Query().GetQueueName();
        }
    }
}