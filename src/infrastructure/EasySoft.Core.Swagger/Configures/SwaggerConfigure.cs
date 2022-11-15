namespace EasySoft.Core.Swagger.Configures;

/// <summary>
/// 开发助手配置
/// </summary>
public static class SwaggerConfigure
{
    /// <summary>
    /// describe all parameters in camelCase
    /// </summary>
    public static bool DescribeAllParametersInCamelCase { get; set; }

    /// <summary>
    /// 输出配置文件信息
    /// </summary>
    public static bool UseNewtonsoft { get; set; }

    static SwaggerConfigure()
    {
        DescribeAllParametersInCamelCase = false;

        UseNewtonsoft = true;
    }

    public static IEnumerable<string> BuildHintMessage()
    {
        return new[]
        {
            $"{typeof(SwaggerConfigure).FullName}.{nameof(DescribeAllParametersInCamelCase)} is {DescribeAllParametersInCamelCase}.",
            $"{typeof(SwaggerConfigure).FullName}.{nameof(UseNewtonsoft)} is {UseNewtonsoft}."
        };
    }
}