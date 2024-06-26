﻿using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.PermissionVerification.Detectors.Interfaces;

/// <summary>
/// 访问探测器 用于探测权限配置
/// </summary>
public interface IAccessWayDetector
{
    /// <summary>
    /// Find
    /// </summary>
    /// <param name="guidTag"></param>
    /// <returns></returns>
    [LogRecord]
    public Task<ExecutiveResult<AccessWayModel>> Find(string guidTag);
}