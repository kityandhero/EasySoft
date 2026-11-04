namespace EasySoft.Core.CacheImplementation.CacheKeyGenerators;

public interface IFusionCacheKeyGenerator
{
    /// <summary>
    /// 根据方法信息、模板、参数，生成最终缓存key
    /// </summary>
    /// <param name="method">被拦截的方法</param>
    /// <param name="arguments">方法入参</param>
    /// <param name="keyTemplate">key模板，例如 "user:{id}"</param>
    string GenerateKey(MethodInfo method, object[] arguments, string keyTemplate);
}