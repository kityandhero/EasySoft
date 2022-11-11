using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.JsonWebToken.Middlewares;
using EasySoft.Core.JsonWebToken.Assists;

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
        StartupDescriptionMessageAssist.AddTraceDivider();

        if (!string.IsNullOrWhiteSpace(FlagAssist.TokenMode))
            throw new Exception("token mode disallow use more than one");

        if (FlagAssist.JsonWebTokenConfigComplete)
            throw new Exception("UseAdvanceJsonWebToken disallow inject more than once");

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceJsonWebToken)}<{typeof(TOperator).Name}>."
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

        StartupDescriptionMessageAssist.AddPrompt(
            "JsonWebToken already available, you can config it with config file.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        return builder;
    }
}