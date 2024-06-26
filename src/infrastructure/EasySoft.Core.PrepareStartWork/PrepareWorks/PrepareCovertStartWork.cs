﻿using EasySoft.Core.Config.Pretreatments;

namespace EasySoft.Core.PrepareStartWork.PrepareWorks;

internal class PrepareCovertStartWork : IPrepareCovertStartWork
{
    public void DoWork()
    {
        PrepareStartAssist.ToWork();

        SuperConfigAssist.Init();
        AgileConfigAssist.Init();
        AppSettingAssist.Init();
        DatabaseConfigAssist.Init();
        ConsulConfigCenterConfigAssist.Init();
        ConsulRegistrationCenterConfigAssist.Init();
        DevelopConfigAssist.Init();
        ElasticSearchConfigAssist.Init();
        JobConfigAssist.Init();
        LogConfigAssist.Init();
        MaintainConfigAssist.Init();
        MongoConfigAssist.Init();
        PayCallbackConfigAssist.Init();
        RabbitMQConfigAssist.Init();
        ServiceConfigAssist.Init();
    }
}