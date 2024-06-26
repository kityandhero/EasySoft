﻿using EasySoft.Core.Swagger.ConfigAssist;

namespace EasySoft.Core.Swagger.Configures;

/// <summary>
/// SwaggerC 配置
/// </summary>
public static class SwaggerConfigure
{
    /// <summary>
    /// describe all parameters in camelCase
    /// </summary>
    public static bool DescribeAllParametersInCamelCase { get; set; }

    /// <summary>
    /// EnableAnnotations
    /// </summary>
    public static bool EnableAnnotations { get; set; }

    /// <summary>
    /// 输出配置文件信息
    /// </summary>
    public static bool UseNewtonsoft { get; set; }

    /// <summary>
    /// ExternalSchemaType
    /// </summary>
    public static ICollection<Type> ExternalSchemaType { get; }

    /// <summary>
    /// 全局参数
    /// </summary>
    public static ICollection<OpenApiParameter> GeneralParameters { get; }

    /// <summary>
    /// GeneralResponseHeaders
    /// </summary>
    public static IDictionary<string, OpenApiHeader> GeneralResponseHeaders { get; }

    /// <summary>
    /// AbnormalResponseCollection
    /// </summary>
    public static IDictionary<string, OpenApiResponse> AbnormalResponseCollection { get; }

    /// <summary>
    /// IgnoreRoutes
    /// </summary>
    public static IDictionary<string, Func<string, string, bool>> IgnoreRoutesAssemblyFilters { get; }

    /// <summary>
    /// SecurityScheme, 配置认证模式, 默认不配置
    /// </summary>
    public static KeyValuePair<string, OpenApiSecurityScheme?> SecurityScheme { get; set; }

    /// <summary>
    /// Global Security Switch
    /// </summary>
    public static bool GlobalSecuritySwitch { get; set; }

    static SwaggerConfigure()
    {
        DescribeAllParametersInCamelCase = true;
        EnableAnnotations = true;
        UseNewtonsoft = true;
        ExternalSchemaType = new List<Type>();
        GeneralParameters = new List<OpenApiParameter>();
        GeneralResponseHeaders = new Dictionary<string, OpenApiHeader>();
        AbnormalResponseCollection = new Dictionary<string, OpenApiResponse>();
        IgnoreRoutesAssemblyFilters = new Dictionary<string, Func<string, string, bool>>();
        SecurityScheme = new KeyValuePair<string, OpenApiSecurityScheme?>("", null);
        GlobalSecuritySwitch = false;
    }

    /// <summary>
    /// BuildHintMessage
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> BuildHintMessage()
    {
        if (!EnvironmentAssist.IsDevelopment() || !SwaggerConfigAssist.GetSwitch()) return Array.Empty<string>();

        return new[]
        {
            $"{typeof(SwaggerConfigure).FullName}.{nameof(DescribeAllParametersInCamelCase)} is {DescribeAllParametersInCamelCase}.",
            $"{typeof(SwaggerConfigure).FullName}.{nameof(EnableAnnotations)} is {EnableAnnotations}.",
            $"{typeof(SwaggerConfigure).FullName}.{nameof(UseNewtonsoft)} is {UseNewtonsoft}."
        };
    }
}