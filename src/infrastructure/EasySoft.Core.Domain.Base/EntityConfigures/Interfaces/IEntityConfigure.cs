using EasySoft.Core.Infrastructure.Operations.Interfaces;

namespace EasySoft.Core.Domain.Base.EntityConfigures.Interfaces;

/// <summary>
/// 适用于 EntityFramework Core 实体配置
/// </summary>
public interface IEntityConfigure<out TOperator> where TOperator : IOperator
{
    TOperator GetOperator();

    void OnModelCreating(dynamic modelBuilder);
}