using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Enums;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// AdvanceSqlBuilderOrderExtensions
/// </summary>
public static class AdvanceSqlBuilderOrderExtensions
{
    /// <summary>
    /// OrderBy
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="sort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder OrderBy<T>(this AdvanceSqlBuilder builder, Sort<T> sort)
    {
        return builder.OrderBy(sort.Expression, sort.SortType);
    }

    /// <summary>
    /// OrderBy
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="sortType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder OrderBy<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T, object>> propertyLambda,
        SortType sortType
    )
    {
        var sql = builder.Sql;

        var sort = SqlAssist.TransferSort(propertyLambda, sortType);

        sql = $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql} ORDER BY ")} {sort}";

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// AndOrderBy
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="sort"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AndOrderBy<T>(this AdvanceSqlBuilder builder, Sort<T> sort)
    {
        return builder.AndOrderBy(sort.Expression, sort.SortType);
    }

    /// <summary>
    /// AndOrderBy
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="propertyLambda"></param>
    /// <param name="sortType"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AndOrderBy<T>(
        this AdvanceSqlBuilder builder,
        Expression<Func<T, object>> propertyLambda,
        SortType sortType
    )
    {
        var sql = builder.Sql;

        var sort = SqlAssist.TransferSort(propertyLambda, sortType);

        sql = $"{(string.IsNullOrWhiteSpace(sql) ? "" : $"{sql},")} {sort}";

        builder.Sql = sql;

        return builder;
    }
}