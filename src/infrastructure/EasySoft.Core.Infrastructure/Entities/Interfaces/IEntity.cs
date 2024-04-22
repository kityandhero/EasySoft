using System.Linq.Expressions;

namespace EasySoft.Core.Infrastructure.Entities.Interfaces;

/// <summary>
/// IEntity
/// </summary>
public interface IEntity : IEntity<long>
{
}

/// <summary>
/// IEntity
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IEntity<TKey>
{
    /// <summary>
    /// Id
    /// </summary>
    TKey Id { get; set; }

    /// <summary>
    /// GetPrimaryKeyName
    /// </summary>
    /// <returns></returns>
    string GetPrimaryKeyName();

    /// <summary>
    /// GetSqlSchemaName
    /// </summary>
    /// <returns></returns>
    string GetSqlSchemaName();

    /// <summary>
    /// GetSqlFieldStringValueDecorateStart
    /// </summary>
    /// <returns></returns>
    string GetSqlFieldStringValueDecorateStart();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string GetSqlFieldStringValueDecorateEnd();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string GetSqlFieldDecorateStart();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string GetSqlFieldDecorateEnd();
}