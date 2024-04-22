using EasySoft.Core.Infrastructure.Entities.Interfaces;

namespace EasySoft.Core.Infrastructure.Entities.Implements;

/// <summary>
/// Entity
/// </summary>
public abstract class Entity : IEntity<long>
{
    /// <summary>
    /// 数据标识
    /// </summary>
    [Description("数据标识")]
    public virtual long Id { get; set; }

    /// <inheritdoc />
    [Obsolete("此模型不支持该方法, 如有必要, 请重载")]
    public virtual string GetPrimaryKeyName()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Obsolete("此模型不支持该方法, 如有必要, 请重载")]
    public virtual string GetSqlSchemaName()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Obsolete("此模型不支持该方法, 如有必要, 请重载")]
    public virtual string GetSqlFieldStringValueDecorateStart()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Obsolete("此模型不支持该方法, 如有必要, 请重载")]
    public virtual string GetSqlFieldStringValueDecorateEnd()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Obsolete("此模型不支持该方法, 如有必要, 请重载")]
    public virtual string GetSqlFieldDecorateStart()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Obsolete("此模型不支持该方法, 如有必要, 请重载")]
    public virtual string GetSqlFieldDecorateEnd()
    {
        throw new NotImplementedException();
    }
}