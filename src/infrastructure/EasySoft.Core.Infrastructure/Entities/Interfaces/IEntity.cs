namespace EasySoft.Core.Infrastructure.Entities.Interfaces;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}