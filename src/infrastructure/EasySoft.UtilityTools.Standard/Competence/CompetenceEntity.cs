using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Standard.Competence;

/// <summary>
/// 权限体
/// </summary>
[Serializable]
public class CompetenceEntity
{
    /// <summary>
    /// Guid标识
    /// </summary>
    public string GuidTag { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string RelativePath { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string Explain { get; set; }

    /// <summary>
    /// 扩展权限设置
    /// </summary>
    public string ExpansionSet { get; set; }

    /// <summary>
    /// CompetenceEntity
    /// </summary>
    public CompetenceEntity()
    {
        GuidTag = "";
        Name = "";
        RelativePath = "";
        Explain = "";
        ExpansionSet = "";
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public ExpandoObject ToObject(Func<CompetenceEntity, ExpandoObject> func)
    {
        var additionalData = func(this);

        return ToObject(additionalData);
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public ExpandoObject ToObject(ExpandoObject? additionalData = null)
    {
        var d = this.ToExpandoObject();

        if (additionalData != null) d.AddKeyValuePair(new KeyValuePair<string, object?>("additional", additionalData));

        var json = JsonConvertAssist.SerializeAndKeyToLower(d);

        var jObject = JsonConvert.DeserializeObject<JObject>(json);

        return jObject?.ToExpandoObject() ?? new ExpandoObject();
    }

    /// <summary>
    /// GetExpansionSetCollection
    /// </summary>
    /// <returns></returns>
    public List<object> GetExpansionSetCollection()
    {
        var result = new List<object>();

        var listExpand = Explain.Split('|').ToList().Where(o => !string.IsNullOrWhiteSpace(o)).ToList();

        var listExpansionSet = ExpansionSet.ToCharArray().Select(o => o.ToString()).ToList();

        if (listExpand.Count <= 0) return result;

        var count = listExpand.Count;

        if (listExpand.Count != listExpansionSet.Count())
            //result = listExpand.Select(o => (object)new { name = o, value = "0" }).ToList();
            for (var i = 0; i < count; i++)
                result.Add(new
                {
                    indexNo = i,
                    name = listExpand[i],
                    value = "0"
                });
        else
            for (var i = 0; i < count; i++)
                result.Add(new
                {
                    indexNo = i,
                    name = listExpand[i],
                    value = listExpansionSet[i] == "1" ? "1" : "0"
                });

        return result;
    }

    /// <summary>
    /// 获取两个单权限的公共权限，不适用于权限集合形式
    /// </summary>
    /// <param name="competenceBefore">权限结构体实例</param>
    /// <param name="competenceAfter">权限结构体实例</param>
    /// <returns></returns>
    public static CompetenceEntity operator +(CompetenceEntity competenceBefore, CompetenceEntity competenceAfter)
    {
        if (competenceBefore.GuidTag != competenceAfter.GuidTag) throw new Exception("合并的两个权限体必须标识一致");

        var mergerResult = Merger(competenceBefore, competenceAfter);
        var result = new CompetenceEntity
        {
            GuidTag = competenceBefore.GuidTag,
            ExpansionSet = mergerResult
        };

        return result;
    }

    /// <summary>
    /// 合并权限
    /// </summary>
    /// <param name="competenceBefore">权限</param>
    /// <param name="competenceAfter">权限</param>
    /// <returns></returns>
    private static string Merger(CompetenceEntity competenceBefore, CompetenceEntity competenceAfter)
    {
        var result = Merger(competenceBefore.ExpansionSet, competenceAfter.ExpansionSet);
        return result;
    }

    /// <summary>
    /// 合并权限
    /// </summary>
    /// <param name="permissionsBefore">权限</param>
    /// <param name="permissionsAfter">权限</param>
    /// <returns></returns>
    private static string Merger(string permissionsBefore, string permissionsAfter)
    {
        var beforeList = Enumerable.ToList(permissionsBefore.ToCharArray());
        var afterList = Enumerable.ToList(permissionsAfter.ToCharArray());

        List<char> maxList;
        List<char> minList;
        if (beforeList.Count >= afterList.Count)
        {
            maxList = beforeList;
            minList = afterList;
        }
        else
        {
            maxList = afterList;
            minList = beforeList;
        }

        foreach (var c in minList)
            if (maxList.Contains(c))
            {
                var index = maxList.IndexOf(c);

                var be = maxList[index] == '1' ? '1' : '0';
                var af = c == '1' ? '1' : '0';

                if (be == '0' && af == '0')
                    maxList[index] = '0';
                else
                    maxList[index] = '1';
            }
            else
            {
                maxList.Add(c);
            }

        return maxList.Count > 0 ? maxList.Join("") : "";
    }
}

/// <summary>
/// SortCompetenceComparer
/// </summary>
public class SortCompetenceComparer : IComparer<CompetenceEntity>
{
    /// <summary>
    /// Compare
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(CompetenceEntity? x, CompetenceEntity? y)
    {
        if (y != null && x != null && string.Compare(x.Name, y.Name, StringComparison.Ordinal) > 0) return 1;

        return 0;
    }
}

/// <summary>
/// 
/// </summary>
public class UrlCompetenceComparer : IEqualityComparer<CompetenceEntity>, IComparer<CompetenceEntity>
{
    /// <inheritdoc />
    public bool Equals(CompetenceEntity? x, CompetenceEntity? y)
    {
        if (x != null && y != null && x.GuidTag == y.GuidTag) return true;

        return false;
    }

    /// <inheritdoc />
    public int GetHashCode(CompetenceEntity obj)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public int Compare(CompetenceEntity? x, CompetenceEntity? y)
    {
        if (y != null && x != null && x.GuidTag == y.GuidTag) return 1;

        return 0;
    }
}

/// <summary>
/// 权限集合
/// </summary>
[Serializable]
public class CompetenceBox
{
    /// <summary>
    /// 权限集
    /// </summary>
    public IList<CompetenceEntity> CompetenceCollection { get; set; }

    /// <summary>
    /// CompetenceBox
    /// </summary>
    public CompetenceBox()
    {
        CompetenceCollection = new List<CompetenceEntity>();
    }

    /// <summary>
    /// CheckAllowAccess 传递null参数则判断当前链接
    /// </summary>
    /// <param name="guidTag">GuidTag</param>
    /// <returns>true or false</returns>
    public bool CheckAllowAccess(string guidTag)
    {
        return CompetenceCollection.Any(c => c.GuidTag.Equals(guidTag, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// CheckAllowAccess
    /// </summary>
    /// <param name="competence">扩展权限</param>
    /// <param name="guidTag">GuidTag</param>
    /// <returns>true or false</returns>
    public bool CheckCompetence(string competence, string guidTag)
    {
        var index = Competence.CompetenceCollection.GetInstance().GetCompetenceIndex(guidTag, competence);
        if (index == null) return false;

        var v = GetExpansionValue(guidTag);
        var cv = v.GetByIndex((int)index);
        return cv == "1";
    }

    /// <summary>
    /// GetExpansionValue
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    public string GetExpansionValue(string guidTag)
    {
        foreach (var c in CompetenceCollection)
            if (c.GuidTag.Equals(guidTag, StringComparison.OrdinalIgnoreCase))
                return c.ExpansionSet;

        return "";
    }

    /// <summary>
    /// 构建权限键
    /// </summary>
    /// <param name="guidTag">GuidTag</param>
    /// <returns></returns>
    public static string BuildCompetenceKey(string guidTag)
    {
        var v = guidTag.ToLower();
        return v;
    }

    /// <summary>
    /// 转化为Json字符串
    /// </summary>
    /// <returns></returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    /// 转换为CompetenceBox
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static CompetenceBox? ConvertToCompetenceBox(string json)
    {
        return JsonConvert.DeserializeObject<CompetenceBox>(json);
    }

    /// <summary>
    /// 获取两个单权限的公共权限，不适用于权限集合形式
    /// </summary>
    /// <param name="competenceBefore">权限结构体实例</param>
    /// <param name="competenceAfter">权限结构体实例</param>
    /// <returns></returns>
    public static CompetenceBox operator +(CompetenceBox competenceBefore, CompetenceBox competenceAfter)
    {
        var tempList = new List<CompetenceEntity>();

        foreach (var cBefore in competenceBefore.CompetenceCollection)
            if (tempList.Contains(cBefore, new UrlCompetenceComparer()))
            {
                var index = tempList.BinarySearch(cBefore, new UrlCompetenceComparer());
                if (index >= 0)
                {
                    var t = tempList[index];
                    t += cBefore;
                    tempList[index] = t;
                }
            }
            else
            {
                tempList.Add(cBefore);
            }

        foreach (var cAfter in competenceAfter.CompetenceCollection)
            if (tempList.Contains(cAfter, new UrlCompetenceComparer()))
            {
                var index = tempList.BinarySearch(cAfter, new UrlCompetenceComparer());
                if (index >= 0)
                {
                    var t = tempList[index];
                    t += cAfter;
                    tempList[index] = t;
                }
            }
            else
            {
                tempList.Add(cAfter);
            }

        return new CompetenceBox
        {
            CompetenceCollection = tempList
        };
    }
}