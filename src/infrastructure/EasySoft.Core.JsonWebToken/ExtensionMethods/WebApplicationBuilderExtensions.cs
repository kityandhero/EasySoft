using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.JsonWebToken.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.JsonWebToken.Assists;
using EasySoft.UtilityTools.Standard;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.JsonWebToken.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 配置操作者验证以及操作权限验证    
    /// </summary> 
    /// <param name="builder"></param>
    /// <param name="middlewareMode"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceJsonWebToken<TOperator>(
        this WebApplicationBuilder builder,
        bool middlewareMode = true
    ) where TOperator : ActualOperator
    {
        if (!string.IsNullOrWhiteSpace(FlagAssist.TokenMode))
            throw new Exception("token mode disallow use more than one");

        if (FlagAssist.JsonWebTokenConfigComplete)
            throw new Exception("UseAdvanceJsonWebToken disallow inject more than once");

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Debug)
                .SetMessage(
                    $"Execute {nameof(AddAdvanceJsonWebToken)}<{typeof(TOperator).Name}>()."
                )
        );

        builder.AddOperator<TOperator>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = TokenAssist.GenerateTokenValidationParameters();
        });

        if (middlewareMode)
        {
            builder.Services.AddTransient<JsonWebTokenMiddleware>();

            FlagAssist.JsonWebTokenMiddlewareModeSwitch = true;
        }

        FlagAssist.JsonWebTokenConfigComplete = true;

        FlagAssist.TokenMode = ConstCollection.JsonWebToken;

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetLevel(LogLevel.Information)
                .SetMessage(
                    "JsonWebToken already available, you can config it with config file."
                )
                .SetExtra(GeneralConfigAssist.GetConfigFileInfo())
        );

        return builder;
    }
}