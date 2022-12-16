using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.JsonWebToken.Middlewares;
using EasySoft.Core.JsonWebToken.Assists;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.JsonWebToken.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddAdvanceJsonWebToken = "63fdf92c-827d-45f8-a38e-36c6015aa14f";

    /// <summary>
    /// 配置操作者验证    
    /// </summary> 
    /// <param name="builder"></param>
    /// <param name="middlewareMode"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceJsonWebToken(
        this WebApplicationBuilder builder,
        bool middlewareMode = true
    )
    {
        return builder.AddAdvanceJsonWebToken<DefaultActualOperator>(middlewareMode);
    }

    /// <summary>
    /// 配置操作者验证    
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

        if (builder.HasRegistered(UniqueIdentifierAddAdvanceJsonWebToken))
        {
            StartupWarnMessageAssist.AddWarning(
                $"{nameof(AddAdvanceJsonWebToken)} should be executed only once, please check it."
            );

            return builder;
        }

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

        FlagAssist.TokenMode = ConstCollection.JsonWebToken;

        StartupDescriptionMessageAssist.AddPrompt(
            "JsonWebToken already available, you can config it with config file.",
            GeneralConfigAssist.GetConfigFileInfo()
        );

        return builder;
    }
}