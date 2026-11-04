namespace EasySoft.Core.CacheImplementation.CacheKeyGenerators;

public class DefaultFusionCacheKeyGenerator : IFusionCacheKeyGenerator
{
    public string GenerateKey(MethodInfo method, object[] arguments, string keyTemplate)
    {
        if (string.IsNullOrWhiteSpace(keyTemplate))
        {
            throw new ArgumentNullException(nameof(keyTemplate));
        }

        // 简单复刻EasyCaching默认模板解析：{参数名}
        var parameters = method.GetParameters();
        var result = keyTemplate;
        for (var i = 0; i < parameters.Length; i++)
        {
            var paramName = parameters[i].Name;
            var placeholder = $"{{{paramName}}}";
            if (result.Contains(placeholder))
            {
                var val = arguments[i]?.ToString() ?? string.Empty;
                result = result.Replace(placeholder, val);
            }
        }

        return result;
    }
}