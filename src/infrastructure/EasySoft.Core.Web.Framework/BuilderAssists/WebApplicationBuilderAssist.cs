using EasySoft.Core.AgileConfigClient.ExtensionMethods;
using EasySoft.Core.AutoFac.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.NLog.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.Web.Framework.BuilderAssists;

public static class WebApplicationBuilderAssist
{
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args
        });

        builder.UseAdvanceAutoFac();

        builder.UseCovertInjection();

        builder.UseAdvanceNLog();

        if (GeneralConfigAssist.GetAgileConfigSwitch())
        {
            builder.UseAgileConfigClient();
        }

        return builder;
    }
}