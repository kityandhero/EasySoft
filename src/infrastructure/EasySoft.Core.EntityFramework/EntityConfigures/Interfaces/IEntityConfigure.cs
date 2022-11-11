namespace EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;

/// <summary>
/// 适用于 EntityFramework Core 实体配置
/// </summary>
public interface IEntityConfigure
{
    void OnModelCreating(ModelBuilder modelBuilder);
}