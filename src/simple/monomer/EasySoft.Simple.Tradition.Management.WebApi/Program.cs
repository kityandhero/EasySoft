using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Swagger.Configures;
using EasySoft.Simple.Tradition.Common.Enums;
using EasySoft.Simple.Tradition.Data.Contexts;
using EasySoft.Simple.Tradition.Data.EntityConfigures;
using EasySoft.Simple.Tradition.Management.WebApi.Security;
using EasySoft.Simple.Tradition.Service.Services.Implementations;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

AuxiliaryConfigure.PromptStartupExecuteMessage = false;

SwaggerConfigure.ExternalSchemaType.Add(typeof(Task<ApiResult>));

SwaggerConfigure.GeneralParameters.AddRange(new OpenApiParameter
    {
        Name = "headerParam",
        Description = "全局 Header 参数",
        Required = true,
        In = ParameterLocation.Header
    },
    new OpenApiParameter
    {
        Name = "queryParam",
        Description = "全局 Query 参数",
        Required = false,
        In = ParameterLocation.Query
    }, new OpenApiParameter
    {
        Name = "cookieParam",
        Description = "全局 Cookie 参数",
        Required = false,
        In = ParameterLocation.Cookie
    }, new OpenApiParameter
    {
        Name = "pathParam",
        Description = "全局 Path 参数",
        Required = false,
        In = ParameterLocation.Path
    });

SwaggerConfigure.GeneralResponseHeaders.Add(
    new KeyValuePair<string, OpenApiHeader>(
        "responseHeader1",
        new OpenApiHeader
        {
            Description = "common header"
        }
    )
);

EasySoft.Core.EntityFramework.Configures.ContextConfigure.EnableDetailedErrors = true;
EasySoft.Core.EntityFramework.Configures.ContextConfigure.EnableSensitiveDataLogging = true;
EasySoft.Core.EntityFramework.Configures.ContextConfigure.AutoEnsureCreated = true;

// 配置额外的构建项目
ApplicationConfigurator.AddWebApplicationBuilderExtraActions(
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddApiVersioning")
        .SetAction(applicationBuilder =>
        {
            applicationBuilder.Services.AddApiVersioning(o =>
            {
                //return versions in a response header
                o.ReportApiVersions = true;
                //default version select 
                o.DefaultApiVersion = new ApiVersion(1, 0);
                //if not specifying an api version,show the default version
                o.AssumeDefaultVersionWhenUnspecified = true;
            });
        }),
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddAdvanceDbContext<DataContext>")
        .SetAction(applicationBuilder =>
        {
            //使用 Sql Server
            applicationBuilder.AddAdvanceSqlServer<SqlServerDataContext, IntegrationEntityConfigure>(
                DatabaseConfigAssist.GetMainConnection(),
                opt =>
                {
                    //自动转换命名格式
                    opt.UseSnakeCaseNamingConvention();
                }
            );

            // 使用 MySql
            // applicationBuilder.AddAdvanceMySql<MySqlDataContext, IntegrationEntityConfigure>(
            //     DatabaseConfigAssist.GetMainConnection(),
            //     opt =>
            //     {
            //         //自动转换命名格式
            //         opt.UseSnakeCaseNamingConvention();
            //     }
            // );

            applicationBuilder.AddAssemblyBusinessServices(
                typeof(IBlogService).Assembly,
                typeof(BlogService).Assembly
            );
        }),
    // 自定义静态文件配置 如有特殊需求，可以进行配置，不配置将采用内置选项，此处仅作为有需要时的样例
    // applicationBuilder => { applicationBuilder.AddStaticFileOptionsInjection<CustomStaticFileOptions>(); },
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddAdvanceJsonWebToken")
        .SetAction(applicationBuilder => { applicationBuilder.AddAdvanceJsonWebToken<ApplicationOperator>(); }),
    new ExtraAction<WebApplicationBuilder>()
        .SetName("AddPermissionVerification")
        .SetAction(applicationBuilder =>
        {
            applicationBuilder.AddPermissionVerification<ApplicationPermissionObserver>();
        })
);

var app = WebApplicationBuilderAssist
    .CreateBuilder(
        ApplicationChannelCollection.ManagementWebApi.ToApplicationChannel(),
        args.ToArray()
    )
    .EasyBuild();

app.Run();