using EasySoft.Core.AuthenticationCore.Operators;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    private const string IdentifierAddAuthenticationCore = "07073d406d8e4c499e16a011ea983b6c";

    /// <summary>
    /// AddInfrastructure
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static WebApplicationBuilder AddAuthenticationCore(
        this WebApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddAuthenticationCore))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddAuthenticationCore)}");

        builder.Services.AddAuthenticationConfigureAssist(builder.Configuration);

        return builder;
    }

    /// <summary>
    /// 注入操作员
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns></returns>
    public static WebApplicationBuilder AddOperator<T>(
        this WebApplicationBuilder builder
    ) where T : ActualOperator
    {
        builder.Host.AddOperator<T>();

        return builder;
    }
}