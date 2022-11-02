namespace EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}