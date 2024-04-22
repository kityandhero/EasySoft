using EasySoft.Core.Infrastructure.Entities.Implements;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Simple.Tradition.Data.Entities.Bases;

public abstract class BaseEntity : Entity, IIdString
{
    /// <inheritdoc />
    public string GetIdString()
    {
        return Id.ToString();
    }
}