using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Enums;

namespace EasySoft.Core.Sql.Common;

/// <summary>
/// ConditionStrange
/// </summary>
/// <typeparam name="T"></typeparam>
public class ConditionStrange<T> where T : new()
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
    /// ConditionStrange
    /// </summary>
    public ConditionStrange() : this("")
    {
    }

    /// <summary>
    /// ConditionStrange
    /// </summary>
    /// <param name="collaborationCondition"></param>
    public ConditionStrange(string collaborationCondition)
    {
        CollaborationCondition = collaborationCondition;
    }

    private ConditionStrange<T> AppendCollaboration<T2>(
        CollaborationType collaborationType,
        ConditionStrange<T2> collaborationCondition
    ) where T2 : IEntity, new()
    {
        var transferResult = TransferStrangeAssist.TransferCondition(collaborationCondition);

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

    private ConditionStrange<T> BuildCollaboration<T2>(
        CollaborationType collaborationType,
        ConditionStrange<T2> collaborationCondition
    ) where T2 : IEntity, new()
    {
        var transferResult = TransferStrangeAssist.TransferCondition(collaborationCondition);

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
    public ConditionStrange<T> AndCollaboration<T2>(ConditionStrange<T2> collaborationCondition)
        where T2 : IEntity, new()
    {
        return BuildCollaboration(CollaborationType.And, collaborationCondition);
    }

    /// <summary>
    /// AppendAndCollaboration
    /// </summary>
    /// <param name="collaborationCondition"></param>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public ConditionStrange<T> AppendAndCollaboration<T2>(ConditionStrange<T2> collaborationCondition)
        where T2 : IEntity, new()
    {
        return AppendCollaboration(CollaborationType.And, collaborationCondition);
    }

    /// <summary>
    /// OrCollaboration
    /// </summary>
    /// <param name="collaborationCondition"></param>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public ConditionStrange<T> OrCollaboration<T2>(ConditionStrange<T2> collaborationCondition)
        where T2 : IEntity, new()
    {
        return BuildCollaboration(CollaborationType.Or, collaborationCondition);
    }

    /// <summary>
    /// AppendOrCollaboration
    /// </summary>
    /// <param name="collaborationCondition"></param>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public ConditionStrange<T> AppendOrCollaboration<T2>(ConditionStrange<T2> collaborationCondition)
        where T2 : IEntity, new()
    {
        return AppendCollaboration(CollaborationType.Or, collaborationCondition);
    }

    /// <summary>
    /// TransferExpression
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string TransferExpression(out Type type)
    {
        return TransferStrangeAssist.GetPropertyName(Expression, out type);
    }
}