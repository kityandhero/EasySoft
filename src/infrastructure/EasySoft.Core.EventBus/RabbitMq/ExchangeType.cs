namespace EasySoft.Core.EventBus.RabbitMq;

public enum ExchangeType
{
    //发布订阅模式
    Fanout,

    //路由模式
    Direct,

    //通配符模式
    Topic
}