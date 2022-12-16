using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// CollectionExtensions
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// 过滤null，返回集合
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> ToListFilterNullable<T>(this IEnumerable<T?> source)
    {
        var result = new List<T>();

        source.ForEach(o =>
        {
            if (o != null) result.Add(o);
        });

        return result;
    }

    /// <summary>
    /// 过滤null/empty，返回集合
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static List<string> ToListFilterNullOrWhiteSpace(this IEnumerable<string?> source)
    {
        var result = new List<string>();

        source.ForEach(o =>
        {
            if (!string.IsNullOrWhiteSpace(o)) result.Add(o);
        });

        return result;
    }

    #region Set 集合

    /// <summary>
    /// 当前集合是否是目标集合target的父集
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsTargetSuperset<T>(this ICollection<T> source, IEnumerable<T> target)
    {
        return target.All(source.Contains);
    }

    /// <summary>
    /// 当前集合是否是目标集合target的子集
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsTargetSubset<T>(this IEnumerable<T> source, ICollection<T> target)
    {
        return source.All(target.Contains);
    }

    /// <summary>
    /// 获取当前集合不在目标集合中的元素集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ICollection<T> GetListNotInTarget<T>(this IEnumerable<T> source, ICollection<T> target)
    {
        return source.Where(item => !target.Contains(item)).ToList();
    }

    /// <summary>
    /// 获取集合交集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ICollection<T> GetIntersection<T>(this IEnumerable<T> source, ICollection<T> target)
    {
        return source.Where(target.Contains).ToList();
    }

    /// <summary>
    /// 获取两集合全集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ICollection<T> GetUniversalSet<T>(this ICollection<T> source, IEnumerable<T> target)
    {
        var list = source.ToList();

        list.AddRange(target.Where(item => !source.Contains(item)));

        return list;
    }

    #endregion

    #region Distinct

    /// <summary>
    /// 去重(public属性值完全重复的函数)
    /// 较适用于基础类型
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ICollection<T> DistinctByProperty<T>(this ICollection<T> source) where T : class
    {
        var result = source.GetType().Create() as ICollection<T> ?? new List<T>();

        var sourceAdjust = source.ToListFilterNullable();

        foreach (var item in sourceAdjust)
            if (result != null && !result.ContainsByProperty(item))
                result.Add(item);

        return result ?? new List<T>();
    }

    #endregion

    #region Get  Property Value  Collection  取出特定的属性值的集合

    /// <summary>
    /// 取出特定的属性值的集合
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="propertyName">属性名</param>
    /// <param name="distinct">去除重复值</param>
    /// <example>
    /// </example>
    /// <returns></returns>
    public static IList<TOther> GetPropertyValueCollection<T, TOther>(
        this IEnumerable<T> source,
        string propertyName,
        bool distinct = true
    )
    {
        var sourceArray = source.ToArray();

        return sourceArray.GetPropertyValueCollection<T, TOther>(propertyName, distinct);
    }

    #endregion

    #region Get By Property  根据属性条件获取对象

    /// <summary>
    /// 根据属性条件获取对象
    /// 满足筛选条件的第一个元素
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="filter">筛选对象</param>
    /// <param name="propertyName">属性名</param>
    /// <example>
    /// </example>
    /// <returns></returns>
    public static T? Get<T>(
        this IEnumerable<T> source,
        object filter,
        string? propertyName = null
    ) where T : class
    {
        var sourceArray = source.ToArray();

        return sourceArray.Get(filter, propertyName);
    }

    /// <summary>
    /// 根据属性条件获取对象
    /// 满足筛选条件的所有元素
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="filter">筛选对象</param>
    /// <param name="propertyName">属性名</param>
    /// <example>
    /// </example>
    /// <returns></returns>
    public static IEnumerable<T> GetAll<T>(
        this IEnumerable<T> source,
        object filter,
        string? propertyName = null
    ) where T : class
    {
        var sourceArray = source.ToArray();

        return sourceArray.GetAll(filter, propertyName);
    }

    #endregion

    #region RemoveValue By Property 根据属性条件移除对象

    /// <summary>
    /// 根据属性条件移除对象
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="filter">筛选对象</param>
    /// <param name="propertyName">属性名</param>
    /// <param name="onlyFirstMatch">只移除第一个匹配项</param>
    /// <example>
    /// </example>
    /// <returns></returns>
    public static void RemoveValue<T>(
        this ICollection<T> source,
        object filter,
        string? propertyName = null,
        bool onlyFirstMatch = true
    ) where T : class
    {
        if (source.IsReadOnly) throw new Exception("该集合已设置为只读，不能删除元素");

        if (onlyFirstMatch)
        {
            var v = source.Get(filter, propertyName);

            if (v != null) source.Remove(v);
        }
        else
        {
            var r = source.GetAll(filter, propertyName);

            foreach (var item in r) source.Remove(item);
        }
    }

    #endregion

    #region ContentsByProperty 判断对象是否被包含

    /// <summary>
    /// 判断对象是否被包含
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="filter">筛选对象</param>
    /// <example>
    /// </example>
    /// <returns></returns>
    public static bool ContainsByProperty<T>(this IEnumerable<T> source, object filter) where T : class
    {
        var sourceArray = source.ToArray();

        return sourceArray.ExistByPropertyValue(filter);
    }

    #endregion

    #region ExistByProperty 判断对象是否存在

    /// <summary>
    /// 判断对象是否存在
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="filter">筛选对象</param>
    /// <param name="propertyName">属性名</param>
    /// <example>
    /// </example>
    /// <returns></returns>
    public static bool ExistByPropertyValue<T>(
        this IEnumerable<T> source,
        object filter,
        string propertyName
    ) where T : class
    {
        var sourceArray = source.ToArray();

        return sourceArray.ExistByPropertyValue(filter, propertyName);
    }

    #endregion

    #region SortByPropertyValue

    /// <summary>
    /// 判断对象是否存在
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="propertyName">属性名</param>
    /// <param name="rule">排序规则</param>
    /// <param name="sortCount">针对前几位元素进行排序，默认全部元素</param>
    /// <example>
    /// </example>
    /// <returns></returns>
    public static IEnumerable<T> SortByPropertyValue<T>(
        this IEnumerable<T> source,
        SortRule rule = SortRule.Asc,
        string? propertyName = null,
        int? sortCount = null
    )
    {
        var sourceArray = source.ToArray();

        return sourceArray.SortByPropertyValue(rule, propertyName, sortCount);
    }

    #endregion

    #region Join

    /// <summary>
    /// Join
    /// 用于把数组中的所有元素放入一个字符串,元素是通过指定的分隔符进行分隔,
    /// 如果属性值不是Int,或者string,将会被忽略
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="propertyName">属性名称(非基础类型必填)</param>
    /// <param name="connector">连接符</param>
    /// <param name="template">格式化字符串</param>
    /// <param name="distinct">去除重复，默认去重</param>
    /// <example>
    /// </example>
    /// <returns></returns>
    public static string Join<T>(
        this IEnumerable<T> source,
        string connector,
        string template = "",
        bool distinct = true,
        string propertyName = ""
    )
    {
        var enumerable = source as T[] ?? source.ToArray();

        return enumerable.Join(connector, template, distinct, propertyName);
    }

    /// <summary>
    /// Merge
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="target"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ICollection<KeyValuePair<TKey, TValue>> Merge<TKey, TValue>(
        this ICollection<KeyValuePair<TKey, TValue>> collection,
        ICollection<KeyValuePair<TKey, TValue>> target
    )
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        var targetKeys = new List<TKey>();

        foreach (var itemTarget in target) targetKeys.Add(itemTarget.Key);

        var result = new List<KeyValuePair<TKey, TValue>>();

        foreach (var item in collection)
            if (!targetKeys.Contains(item.Key))
                result.Add(item);

        result.AddRange(target);

        return result;
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a list of items to the collection
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection</typeparam>
    /// <param name="collection">Collection</param>
    /// <param name="items">Items to add</param>
    /// <returns>The collection with the added items</returns>
    public static ICollection<T> Add<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        items.ForEach(collection.Add);

        return collection;
    }

    /// <summary>
    /// Adds a list of items to the collection
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection</typeparam>
    /// <param name="collection">Collection</param>
    /// <param name="items">Items to add</param>
    /// <returns>The collection with the added items</returns>
    public static ICollection<T> Add<T>(this ICollection<T> collection, params T[] items)
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        items.ForEach(collection.Add);

        return collection;
    }

    #endregion

    #region AddAndReturn

    /// <summary>
    /// Adds an item to a list and returns the item
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="collection">Collection to add to</param>
    /// <param name="item">Item to add to the collection</param>
    /// <returns>The original item</returns>
    public static T AddAndReturn<T>(this ICollection<T> collection, T item)
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        if (item.IsNull()) throw new ArgumentNullException(nameof(item));

        collection.Add(item);
        return item;
    }

    #endregion

    #region AddIf

    /// <summary>
    /// Adds items to the collection if it passes the predicate test
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection to add to</param>
    /// <param name="items">Items to add to the collection</param>
    /// <param name="predicate">Predicate that an item needs to satisfy in order to be added</param>
    /// <returns>True if any are added, false otherwise</returns>
    public static bool AddIf<T>(
        this ICollection<T> collection,
        Predicate<T> predicate,
        params T[] items
    )
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        if (predicate.IsNull()) throw new ArgumentNullException(nameof(predicate));

        var returnValue = false;

        foreach (var item in items)
        {
            if (!predicate(item)) continue;

            collection.Add(item);

            returnValue = true;
        }

        return returnValue;
    }

    /// <summary>
    /// Adds an item to the collection if it isn't already in the collection
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection to add to</param>
    /// <param name="items">Items to add to the collection</param>
    /// <param name="predicate">Predicate that an item needs to satisfy in order to be added</param>
    /// <returns>True if it is added, false otherwise</returns>
    public static bool AddIf<T>(
        this ICollection<T> collection,
        Predicate<T> predicate,
        IEnumerable<T> items
    )
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        if (predicate.IsNull()) throw new ArgumentNullException(nameof(predicate));

        return collection.AddIf(predicate, items.ToArray());
    }

    #endregion

    #region AddIfUnique

    /// <summary>
    /// Adds an item to the collection if it isn't already in the collection
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection to add to</param>
    /// <param name="items">Items to add to the collection</param>
    /// <returns>True if it is added, false otherwise</returns>
    public static bool AddIfUnique<T>(this ICollection<T> collection, params T[] items)
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        return collection.AddIf(x => !collection.Contains(x), items);
    }

    /// <summary>
    /// Adds an item to the collection if it isn't already in the collection
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection to add to</param>
    /// <param name="items">Items to add to the collection</param>
    /// <returns>True if it is added, false otherwise</returns>
    public static bool AddIfUnique<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        return collection.AddIf(x => !collection.Contains(x), items);
    }

    #endregion

    #region Copy

    /// <summary>
    /// 赋值集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void CopyTo<T>(this IEnumerable<T> source, ICollection<T> target)
    {
        target.Clear();

        foreach (var val in source) target.Add(val);
    }

    #endregion

    #region Remove

    /// <summary>
    /// Removes all items that fit the predicate passed in
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection</typeparam>
    /// <param name="collection">Collection to remove items from</param>
    /// <param name="predicate">Predicate used to determine what items to remove</param>
    public static ICollection<T> Remove<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        return collection.Where(x => !predicate(x)).ToList();
    }

    /// <summary>
    /// Removes all items in the list from the collection
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection</typeparam>
    /// <param name="collection">Collection</param>
    /// <param name="items">Items to remove</param>
    /// <returns>The collection with the items removed</returns>
    public static ICollection<T> Remove<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        if (collection.IsNull()) throw new ArgumentNullException(nameof(collection));

        return collection.Where(x => !items.Contains(x)).ToList();
    }

    #endregion
}