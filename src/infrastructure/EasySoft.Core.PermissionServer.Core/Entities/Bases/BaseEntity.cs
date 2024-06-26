﻿using EasySoft.Core.Infrastructure.Entities.Implements;

namespace EasySoft.Core.PermissionServer.Core.Entities.Bases;

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