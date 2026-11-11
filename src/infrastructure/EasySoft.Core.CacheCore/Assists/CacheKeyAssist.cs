using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.CacheCore.Assists;

/// <summary>
/// CacheKeyAssist
/// </summary>
public class CacheKeyAssist
{
    #region Properties

    public CacheConfigureAssist CacheConfigureAssist { get; private set; }

    #endregion

    #region Constructors

    public CacheKeyAssist(CacheConfigureAssist cacheConfigureAssist)
    {
        CacheConfigureAssist = cacheConfigureAssist;
    }

    #endregion

    #region Methods

    private string KeyPrefix()
    {
        return CacheConfigureAssist.GetCacheKeyPrefix();
    }

    public string BuildKey<T>(params string[] nameArray)
    {
        var nameArrayAdjust = new List<string>
        {
            typeof(T).Name.ToLowerFirst()
        };

        nameArrayAdjust.AddRange(nameArray);

        return BuildKey(nameArrayAdjust.ToArray());
    }

    public string BuildKey<T1, T2>(params string[] nameArray)
    {
        var nameArrayAdjust = new List<string>
        {
            typeof(T1).Name.ToLowerFirst(),
            typeof(T2).Name.ToLowerFirst()
        };

        nameArrayAdjust.AddRange(nameArray);

        return BuildKey(nameArrayAdjust.ToArray());
    }

    public string BuildKey<T1, T2, T3>(params string[] nameArray)
    {
        var nameArrayAdjust = new List<string>
        {
            typeof(T1).Name.ToLowerFirst(),
            typeof(T2).Name.ToLowerFirst(),
            typeof(T3).Name.ToLowerFirst()
        };

        nameArrayAdjust.AddRange(nameArray);

        return BuildKey(nameArrayAdjust.ToArray());
    }

    public string BuildKey(params string[] nameArray)
    {
        if (nameArray == null)
        {
            throw new UnhandledException("参数不能为null");
        }

        if (nameArray.Length == 0)
        {
            throw new UnhandledException("不能构建空的缓存键名");
        }

        var nameList = new List<string>
        {
            KeyPrefix(),
            nameArray
        };

        var nameListAdjust = nameList
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .Select(o => o.Remove(" ").Trim().ToLowerFirst())
            .ToList();

        var result = nameListAdjust.Join(
            ":",
            "",
            false
        );

        if (string.IsNullOrWhiteSpace(result))
        {
            throw new UnhandledException("构建缓存键名发生错误");
        }

        return result;
    }

    #endregion
}