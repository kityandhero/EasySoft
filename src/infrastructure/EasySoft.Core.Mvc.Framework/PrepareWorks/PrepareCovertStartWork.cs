using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Config.Pretreatments;

namespace EasySoft.Core.Mvc.Framework.PrepareWorks;

internal class PrepareCovertStartWork : IPrepareCovertStartWork
{
    public PrepareCovertStartWork()
    {
    }

    public void DoWork()
    {
        PrepareStartAssist.ToWork();

        AppSettingAssist.Init();
        BusinessConfigAssist.Init();
        DatabaseConfigAssist.Init();
        DevelopConfigAssist.Init();
        ElasticSearchConfigAssist.Init();
        HangfireConfigAssist.Init();
        JobConfigAssist.Init();
        LogConfigAssist.Init();
        MaintainConfigAssist.Init();
        MessageQueueConfigAssist.Init();
        MongoConfigAssist.Init();
        PayCallbackConfigAssist.Init();
        RedisConfigAssist.Init();
        ServiceConfigAssist.Init();
        SwaggerConfigAssist.Init();
    }
}