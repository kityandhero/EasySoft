namespace EasySoft.Core.EntityFramework.EntityFactories;

/// <summary>
/// EntityFactory
/// </summary>
public static class EntityFactory
{
    /// <summary>
    /// Create
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static TEntity Create<TEntity>() where TEntity : Entity, new()
    {
        var entity = new TEntity
        {
            Id = IdentifierAssist.Create()
        };

        return entity;
    }
}