namespace EasySoft.Core.EntityFramework.EntityFactories;

public static class EntityFactory
{
    public static TEntity Create<TEntity>() where TEntity : Entity, new()
    {
        var entity = new TEntity
        {
            Id = IdentifierAssist.Create()
        };

        return entity;
    }
}