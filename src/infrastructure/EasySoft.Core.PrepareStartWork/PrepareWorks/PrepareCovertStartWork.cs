using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.Pretreatments;

namespace EasySoft.Core.PrepareStartWork.PrepareWorks;

internal class PrepareCovertStartWork : IPrepareCovertStartWork
{
    public void DoWork()
    {
        PrepareStartAssist.ToWork();

        AgileConfigAssist.Init();
        AppSettingAssist.Init();
        DatabaseConfigAssist.Init();
        ConsulConfigCenterConfigAssist.Init();
        ConsulRegistrationCenterConfigAssist.Init();
        DevelopConfigAssist.Init();
        ElasticSearchConfigAssist.Init();
        HangfireConfigAssist.Init();
        JobConfigAssist.Init();
        LogConfigAssist.Init();
        MaintainConfigAssist.Init();
        MongoConfigAssist.Init();
        OcelotConfigAssist.Init();
        PayCallbackConfigAssist.Init();
        RabbitMQConfigAssist.Init();
        RedisConfigAssist.Init();
        ServiceConfigAssist.Init();
        SwaggerConfigAssist.Init();
    }
}