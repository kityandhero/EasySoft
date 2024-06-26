﻿using EasySoft.Core.PermissionServer.Core.DataTransferObjects;

namespace EasySoft.Core.PermissionServer.Core.Services.Interfaces;

/// <summary>
/// access way service
/// </summary>
public interface IAccessWayService : IBusinessService
{
    /// <summary>
    /// PageListAsync
    /// </summary>
    /// <param name="accessWaySearchDto"></param>
    /// <returns></returns>
    public Task<PageListResult<AccessWayDto>> PageListAsync(AccessWaySearchDto accessWaySearchDto);

    /// <summary>
    /// SaveAccessWayModelAsync
    /// </summary>
    /// <param name="accessWayMessage"></param>
    /// <returns></returns>
    Task SaveAccessWayAsync(IAccessWayMessage accessWayMessage);
}