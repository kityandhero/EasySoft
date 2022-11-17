﻿namespace EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

/// <summary>
/// ISoftDelete
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// 逻辑删除标记
    /// </summary>
    int Deleted { get; set; }
}