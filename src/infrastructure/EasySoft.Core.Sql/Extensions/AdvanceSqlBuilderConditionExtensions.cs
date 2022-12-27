using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Builders;
using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// AdvanceSqlBuilderConditionExtensions
/// </summary>
public static class AdvanceSqlBuilderConditionExtensions
{
    /// <summary>
    /// OnlyCondition
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder OnlyCondition<T>(
        this AdvanceSqlBuilder builder,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferCondition = SqlAssist.TransferCondition(condition);

        sql = " {0} {1} ".FormatValue(sql, resultTransferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// OnlyConditionStrange
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder OnlyConditionStrange<T>(
        this AdvanceSqlBuilder builder,
        ConditionStrange<T> condition
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferCondition = SqlAssist.TransferConditionStrange(condition);

        sql = " {0} {1} ".FormatValue(sql, resultTransferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// WhereCondition
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder WhereCondition<T>(
        this AdvanceSqlBuilder builder,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferCondition = SqlAssist.TransferCondition(condition);

        sql = "{0} WHERE {1}".FormatValue(sql, resultTransferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// WhereConditionStrange
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder WhereConditionStrange<T>(
        this AdvanceSqlBuilder builder,
        ConditionStrange<T> condition
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferCondition = SqlAssist.TransferConditionStrange(condition);

        sql = "{0} WHERE {1}".FormatValue(sql, resultTransferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// AndCondition
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AndCondition<T>(
        this AdvanceSqlBuilder builder,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferCondition = SqlAssist.TransferCondition(condition);

        sql = "{0} AND {1}".FormatValue(sql, resultTransferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// AndConditionStrange
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder AndConditionStrange<T>(
        this AdvanceSqlBuilder builder,
        ConditionStrange<T> condition
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferCondition = SqlAssist.TransferConditionStrange(condition);

        sql = "{0} AND {1}".FormatValue(sql, resultTransferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// LinkConditions
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="conditions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder LinkConditions<T>(
        this AdvanceSqlBuilder builder,
        ICollection<Condition<T>> conditions
    ) where T : IEntity, new()
    {
        foreach (var condition in conditions) builder.Sql = builder.LinkCondition(condition).Sql;

        return builder;
    }

    /// <summary>
    /// LinkCondition
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder LinkCondition<T>(
        this AdvanceSqlBuilder builder,
        Condition<T> condition
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferCondition = SqlAssist.TransferCondition(condition);

        var connector = "";

        if (!string.IsNullOrWhiteSpace(sql)) connector = SqlAssist.GetConditionConnector(sql);

        sql = "{0} {1} {2}".FormatValue(sql, connector, resultTransferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// LinkConditionStrange
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="condition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder LinkConditionStrange<T>(
        this AdvanceSqlBuilder builder,
        ConditionStrange<T> condition
    ) where T : IEntity, new()
    {
        var sql = builder.Sql;

        var resultTransferCondition = SqlAssist.TransferConditionStrange(condition);

        var connector = "";

        if (!string.IsNullOrWhiteSpace(sql)) connector = SqlAssist.GetConditionConnector(sql);

        sql = "{0} {1} {2}".FormatValue(sql, connector, resultTransferCondition);

        builder.Sql = sql;

        return builder;
    }

    /// <summary>
    /// LinkConditions
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="assignUpdates"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static AdvanceSqlBuilder LinkConditions<T>(
        this AdvanceSqlBuilder builder,
        ICollection<AssignField<T>> assignUpdates
    ) where T : IEntity, new()
    {
        foreach (var assignUpdate in assignUpdates) builder.Sql = builder.LinkAssignField(assignUpdate).Sql;

        return builder;
    }
}