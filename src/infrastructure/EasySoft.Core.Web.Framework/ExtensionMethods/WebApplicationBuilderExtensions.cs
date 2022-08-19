using Autofac;
using Autofac.Extensions.DependencyInjection;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Hangfire.ExtensionMethods;
using EasySoft.Core.IdentityVerification.ExtensionMethods;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Channels;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.PrepareStartWork.ExtensionMethods;
using EasySoft.Core.Swagger.ExtensionMethods;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Web.Framework.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplication EasyBuild(this WebApplicationBuilder builder)
    {
        return EasyBuild(builder, new List<string>());
    }

    public static WebApplication EasyBuild(this WebApplicationBuilder builder, List<string> areas)
    {
        if (!FlagAssist.TokenSecretOptionInjectionComplete)
        {
            builder.UseDefaultTokenSecretOptionsInjection();
        }

        if (!FlagAssist.TokenSecretInjectionComplete)
        {
            builder.UseDefaultTokenSecret();
        }

        if (!FlagAssist.ApplicationChannelInjectionComplete)
        {
            builder.UseAdvanceDefaultApplicationChannel();
        }

        var app = builder.Build();

        AutofacAssist.Instance.Container = app.UseHostFiltering().ApplicationServices.GetAutofacRoot();

        ServiceAssist.ServiceProvider = app.Services;

        // 中间件调用顺序请参阅: https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order

        app.UsePrepareStartWork();

        if (!app.Environment.IsDevelopment())
        {
            // app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        if (FlagAssist.TokenSecretOptionIsDefault)
        {
            app.RecordWarning(
                "TokenSecretOption use DefaultTokenSecretOption, it is not safe, suggest using builder.UseTokenSecretOptionsInjection<T>() with your TokenSecretOption."
            );
        }

        if (GeneralConfigAssist.GetRemoteLogSwitch())
        {
            if (FlagAssist.ApplicationChannelIsDefault)
            {
                app.RecordInformation(
                    "ApplicationChannel use 0, suggest using builder.UseAdvanceApplicationChannel(int channel) with your Application, it make the data source easy to identify in the remote log."
                );
            }
            else
            {
                var applicationChannel = AutofacAssist.Instance.Container.Resolve<IApplicationChannel>();

                app.RecordInformation(
                    $"ApplicationChannel use {applicationChannel.GetChannel()}."
                );
            }
        }

        app.UseHttpsRedirection();

        if (GeneralConfigAssist.GetUseStaticFiles())
        {
            app.UseAdvanceStaticFiles();

            app.RecordInformation("useStaticFiles: enable"
            );
        }
        else
        {
            app.RecordInformation(
                "useStaticFiles: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        app.UseRouting();

        if (GeneralConfigAssist.GetCorsEnable())
        {
            app.UseCors(ConstCollection.DefaultSpecificOrigins);

            app.RecordInformation($"cors: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            app.RecordInformation(
                "cors: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        if (GeneralConfigAssist.GetUseAuthentication())
        {
            app.UseAuthentication();

            app.RecordInformation(
                $"UseAuthentication: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            app.RecordInformation(
                "UseAuthentication: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        if (GeneralConfigAssist.GetUseAuthorization())
        {
            app.UseAuthorization();

            app.RecordInformation(
                $"UseAuthorization: enable, policies: {(GeneralConfigAssist.GetCorsPolicies().Join(","))}"
            );
        }
        else
        {
            app.RecordInformation(
                "UseAuthorization: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        if (GeneralConfigAssist.GetRemoteGeneralLogEnable())
        {
            app.RecordInformation(
                "RemoteGeneralLogEnable: enable"
            );
        }
        else
        {
            app.RecordInformation(
                "RemoteGeneralLogEnable: disable, if you need, you can set it in generalConfig.json, config file path is ./configures/generalConfig.json."
            );
        }

        app.UseAdvanceSwagger();

        app.UseAdvanceHangfire();

        app.RecordInformation(
            "you can set your autoFac config with autoFac.json in ./configures/autoFac.json. The document link is https://autofac.readthedocs.io/en/latest/configuration/xml.html."
        );

        app.RecordInformation(
            "you can get all controller actions by visit https://[host]:[port]/[controller]/getAllActions where controller inherited from CustomControllerBase."
        );

        app.UseAdvanceMapControllers(areas);

        return app;
    }
}