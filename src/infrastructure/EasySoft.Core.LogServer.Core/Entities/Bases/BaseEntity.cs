using EasySoft.UtilityTools.Core.Entities.Implements;

namespace EasySoft.Core.LogServer.Core.Entities.Bases;

/// <summary>
/// BaseEntity
/// </summary>
public abstract class BaseEntity : Entity, IIdString
{
    /// <inheritdoc />
    public string GetIdString()
    {
        return Id.ToString();
    }
}