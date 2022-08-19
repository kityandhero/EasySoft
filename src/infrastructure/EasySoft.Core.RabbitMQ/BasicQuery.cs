using System.Text;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ExchangeRegulation.Interfaces;
using EasySoft.Core.ExchangeRegulation.Query;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace EasySoft.Core.RabbitMQ
{
    public class BasicQuery<T> : IQuery<T> where T : IExchangeEntity
    {
        private readonly string _queryPrefix = MessageQueueConfigAssist.GetPrefix();

        protected virtual string RouteName
        {
            get
            {
                var name = typeof(T).Name;

                return $"{name}QueryRoute";
            }
        }

        protected virtual string QueueName
        {
            get
            {
                var name = typeof(T).Name;

                return $"{name}Queue";
            }
        }

        public string GetQueueRouteName()
        {
            return $"{_queryPrefix}{RouteName}";
        }

        public string GetQueueName()
        {
            return $"{_queryPrefix}{QueueName}";
        }

        /// <summary>
        /// 发送
        /// </summary>
        public void Send(T entity)
        {
            using (var conn = MessageStorage.CreateConnection())
            {
                using (var im = conn.CreateModel())
                {
                    //定义路由
                    im.ExchangeDeclare(GetQueueRouteName(), ExchangeType.Direct, true);
                    //定义队列
                    im.QueueDeclare(GetQueueName(), true, false, false, null);
                    //绑定队列到路由
                    im.QueueBind(GetQueueName(), GetQueueRouteName(), ExchangeType.Direct, null);

                    var properties = im.CreateBasicProperties();

                    //队列持久化
                    properties.Persistent = true;

                    var json = JsonConvert.SerializeObject(entity);

                    var message = Encoding.UTF8.GetBytes(json);

                    //发送消息到队列
                    im.BasicPublish(GetQueueRouteName(), ExchangeType.Direct, properties, message);
                }
            }
        }
    }
}