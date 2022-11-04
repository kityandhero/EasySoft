namespace EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

public interface IEntity : IEntity<long>
{
}

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}