using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Enums;

namespace EasySoft.Core.Sql.Common;

/// <summary>
/// Condition
/// </summary>
/// <typeparam name="T"></typeparam>
public class Condition<T> where T : IEntity, new()
{
    /// <summary>
    /// 指向表达式
    /// </summary>
    public Expression<Func<T, object>> Expression { get; set; } = null!;

    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; } = null!;

    /// <summary>
    /// 判断条件
    /// </summary>
    public ConditionType ConditionType { get; set; }

    /// <summary>
    /// 协同条件
    /// </summary>
    public string CollaborationCondition { get; private set; }

    /// <summary>
    /// ColumnTransferMode
    /// </summary>
    public ColumnTransferMode ColumnTransferMode { get; }

    /// <summary>
    /// Condition
    /// </summary>
    public Condition() : this(ColumnTransferMode.ContainTableName, "")
    {
    }

    /// <summary>
    /// Condition
    /// </summary>
    /// <param name="columnTransferMode"></param>
    public Condition(ColumnTransferMode columnTransferMode) : this(columnTransferMode, "")
    {
        ColumnTransferMode = columnTransferMode;
    }

    /// <summary>
    /// Condition
    /// </summary>
    /// <param name="columnTransferMode"></param>
    /// <param name="collaborationCondition"></param>
    public Condition(ColumnTransferMode columnTransferMode, string collaborationCondition)
    {
        ColumnTransferMode = columnTransferMode;
        CollaborationCondition = collaborationCondition;
    }

    private Condition<T> AppendCollaboration<T2>(
        CollaborationType collaborationType,
        Condition<T2> collaborationCondition
    ) where T2 : IEntity, new()
    {
        var transferResult = TransferAssist.TransferCondition(collaborationCondition);

        switch (collaborationType)
        {
            case CollaborationType.And:
                CollaborationCondition = string.IsNullOrWhiteSpace(CollaborationCondition)
                    ? transferResult
                    : $"{CollaborationCondition} AND {transferResult}";
                break;

            case CollaborationType.Or:
                CollaborationCondition = string.IsNullOrWhiteSpace(CollaborationCondition)
                    ? transferResult
                    : $"{CollaborationCondition} OR {transferResult}";
                break;
        }

        return this;
    }

    private Condition<T> BuildCollaboration<T2>(
        CollaborationType collaborationType,
        Condition<T2> collaborationCondition
    ) where T2 : IEntity, new()
    {
        var transferResult = TransferAssist.TransferCondition(collaborationCondition);

        switch (collaborationType)
        {
            case CollaborationType.And:
                CollaborationCondition = $" AND {transferResult}";
                break;

            case CollaborationType.Or:
                CollaborationCondition = $" OR {transferResult}";
                break;
        }

        return this;
    }

    /// <summary>
    /// AndCollaboration
    /// </summary>
    /// <param name="collaborationCondition"></param>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public Condition<T> AndCollaboration<T2>(Condition<T2> collaborationCondition) where T2 : IEntity, new()
    {
        return BuildCollaboration(CollaborationType.And, collaborationCondition);
    }

    /// <summary>
    /// AppendAndCollaboration
    /// </summary>
    /// <param name="collaborationCondition"></param>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public Condition<T> AppendAndCollaboration<T2>(Condition<T2> collaborationCondition) where T2 : IEntity, new()
    {
        return AppendCollaboration(CollaborationType.And, collaborationCondition);
    }

    /// <summary>
    /// OrCollaboration
    /// </summary>
    /// <param name="collaborationCondition"></param>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public Condition<T> OrCollaboration<T2>(Condition<T2> collaborationCondition) where T2 : IEntity, new()
    {
        return BuildCollaboration(CollaborationType.Or, collaborationCondition);
    }

    /// <summary>
    /// AppendOrCollaboration
    /// </summary>
    /// <param name="collaborationCondition"></param>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public Condition<T> AppendOrCollaboration<T2>(Condition<T2> collaborationCondition) where T2 : IEntity, new()
    {
        return AppendCollaboration(CollaborationType.Or, collaborationCondition);
    }

    /// <summary>
    /// TransferExpression
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string TransferExpression(out Type type)
    {
        if (ColumnTransferMode == ColumnTransferMode.ContainTableName)
            return TransferAssist.GetTableAndColumnName(Expression, out type);

        if (ColumnTransferMode == ColumnTransferMode.ExclusiveTableName)
            return TransferAssist.GetColumnName(Expression, out type);

        throw new Exception("unknown ColumnTransferMode");
    }
}