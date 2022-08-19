using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Web.Framework.Attributes;
using EasySoft.Core.Web.Framework.CommonAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.Core.Web.Framework.Filters;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EasySoft.Core.Web.Framework.BuilderAssists;

public static class WebApplicationBuilderAssist
{
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = args,
            // Look for static files in webroot
            WebRootPath = "webroot"
        });

        builder.UseAdvanceAutoFac();

        return BuildOther(builder);
    }

    private static WebApplicationBuilder BuildOther(WebApplicationBuilder builder)
    {
        // AddMvc 最为全面， 涵盖 AddControllers 等的全部功能
        builder.Services.AddMvc(
                option =>
                {
                    option.EnableEndpointRouting = false;

                    // 设置及接口数据返回格式
                    option.Filters.Add<WebApiResultAttribute>();

                    // 设置全局异常过滤器
                    option.Filters.Add<GlobalExceptionFilter>();
                }
            )
            // 爆露ApplicationPartManager 实例给外部工具，用以实现某些特定功能
            .ConfigureApplicationPartManager(ApplicationPartManagerAssist.SetApplicationPartManager)
            // 通过AddControllersAsServices方法，将控制器交给 autofac 容器来处理，可以使“属性注入”
            .AddControllersAsServices()
            .AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver(); //序列化时key为驼峰样式
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //忽略循环引用
                }
            );

        builder.Services.AddSingleton<IHostEnvironment, HostingEnvironment>();

        // 扩展支持此类使用 @Html.Action("UserBackView", "UserManage")  
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        if (GeneralConfigAssist.GetCorsEnable())
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: ConstCollection.DefaultSpecificOrigins,
                    configPolicy => { configPolicy.WithOrigins(GeneralConfigAssist.GetCorsPolicies().ToArray()); }
                );
            });
        }

        if (SwaggerConfigAssist.GetEnable())
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        builder.UseGeneralLogTransmitter();

        if (HangfireConfigAssist.GetEnable())
        {
            var storage = HangfireConfigAssist.GetStorage();

            switch (storage)
            {
                case "MemoryStorage":
                    builder.Services.AddHangfire(x => x.UseStorage(new MemoryStorage()));
                    break;

                default:
                    throw new Exception($"Hangfire config Storage {storage} does not support just");
            }

            //启用Hangfire服务.
            builder.Services.AddHangfireServer();
        }

        return builder;
    }
}