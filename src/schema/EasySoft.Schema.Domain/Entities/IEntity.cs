namespace EasySoft.Schema.Domain.Entities
{
    public interface IEntity
    {
        object GetKey();
    }

    public interface IEntity<out TKey> : IEntity
    {
        TKey Id { get; }
    }
}