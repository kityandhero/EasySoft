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
    /// RuntimeSchemaType
    /// </summary>
    internal static ISet<Type> RuntimeSchemaType { get; }

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

    static SwaggerConfigure()
    {
        DescribeAllParametersInCamelCase = true;
        EnableAnnotations = true;
        UseNewtonsoft = true;
        ExternalSchemaType = new List<Type>();
        RuntimeSchemaType = new HashSet<Type>();
        GeneralParameters = new List<OpenApiParameter>();
        GeneralResponseHeaders = new Dictionary<string, OpenApiHeader>();
        AbnormalResponseCollection = new Dictionary<string, OpenApiResponse>();
    }

    /// <summary>
    /// BuildHintMessage
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> BuildHintMessage()
    {
        return new[]
        {
            $"{typeof(SwaggerConfigure).FullName}.{nameof(DescribeAllParametersInCamelCase)} is {DescribeAllParametersInCamelCase}.",
            $"{typeof(SwaggerConfigure).FullName}.{nameof(UseNewtonsoft)} is {UseNewtonsoft}."
        };
    }
}