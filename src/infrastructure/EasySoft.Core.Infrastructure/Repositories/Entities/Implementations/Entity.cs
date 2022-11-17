using System.ComponentModel;
using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

namespace EasySoft.Core.Infrastructure.Repositories.Entities.Implementations;

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
}