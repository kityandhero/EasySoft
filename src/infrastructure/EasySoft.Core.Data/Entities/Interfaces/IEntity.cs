namespace EasySoft.Core.Data.Entities.Interfaces;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}