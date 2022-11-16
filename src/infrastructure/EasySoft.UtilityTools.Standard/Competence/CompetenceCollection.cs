﻿using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Standard.Competence;

public sealed class CompetenceCollection
{
    public List<KeyValuePair<string, Dictionary<string, int>>> CompetenceSets { get; }

    private CompetenceCollection()
    {
        CompetenceSets = new List<KeyValuePair<string, Dictionary<string, int>>>();
    }

    /// <summary>
    /// 获取该扩展权限名在扩展权限值字符串中所在的位置
    /// </summary>
    /// <param name="key">       url</param>
    /// <param name="competence">权限文本</param>
    /// <returns></returns>
    public int? GetCompetenceIndex(string key, string competence)
    {
        int? result = null;
        foreach (var item in CompetenceSets)
            if (item.Key == key)
                foreach (var one in item.Value)
                    if (one.Key == competence)
                        result = one.Value;

        return result;
    }

    /// <summary>
    /// Build CompetenceKey
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string BuildCompetenceKey(string url)
    {
        var urlLowerString = url.ToLower();
        var u = new Uri(urlLowerString);
        var key = u.Scheme + "://" + u.Host + u.AbsolutePath;
        return key;
    }

    public void SetCompetenceSets(string key, string competenceSets)
    {
        if (competenceSets.IsNullOrEmpty()) return;

        var valueList = competenceSets.Split('|');
        var compare = new Dictionary<string, int>();
        for (var i = 0; i < valueList.Length; i++) compare.Add(valueList[i], i);

        var remove = false;
        var removeVal = new KeyValuePair<string, Dictionary<string, int>>();
        foreach (var keyPair in CompetenceSets)
            if (keyPair.Key == key)
                if (keyPair.Value != null)
                    if (!keyPair.Value.Equals(compare))
                    {
                        removeVal = keyPair;
                        remove = true;
                    }

        if (remove) CompetenceSets.Remove(removeVal);

        CompetenceSets.Add(new KeyValuePair<string, Dictionary<string, int>>(key, compare));
    }

    public static CompetenceCollection GetInstance()
    {
        return CompetenceSingleton.SET;
    }

    // ReSharper disable once ClassNeverInstantiated.LocalConfigRedis
    private class CompetenceSingleton
    {
        static CompetenceSingleton()
        {
        }

        internal static readonly CompetenceCollection SET = new();
    }
}