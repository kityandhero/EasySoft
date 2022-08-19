using EasySoft.Core.ExchangeRegulation.Query;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;
using EasySoft.Core.RabbitMQ;

namespace EasySoft.Core.GeneralLogTransmitter.MessageQuery
{
    /// <summary>
    /// 库存变更队列
    /// </summary>
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