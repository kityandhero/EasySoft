using EasySoft.Core.Infrastructure.Entities.Implements;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.AppSecurityServer.Core.Entities.Bases;

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