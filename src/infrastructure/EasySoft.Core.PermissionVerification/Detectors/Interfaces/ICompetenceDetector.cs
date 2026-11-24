using EasySoft.Core.AdvanceTrace.Attributes;

namespace EasySoft.Core.PermissionVerification.Detectors.Interfaces;

/// <summary>
/// 拥有权限获取器
/// </summary>
public interface ICompetenceDetector
{
    /// <summary>
    /// 获取权限实体集合
    /// </summary>
    /// <param name="roleGroupId"></param>
    /// <returns></returns>
    [TraceRecord]
    public Task<IList<CompetenceEntity>> GetCompetenceEntityCollection(long roleGroupId);
}