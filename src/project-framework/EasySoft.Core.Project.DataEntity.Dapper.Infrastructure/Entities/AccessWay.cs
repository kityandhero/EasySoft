using System.Linq.Expressions;
using EasySoft.Core.Entities.Common.Bases;
using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 访问模块
/// </summary>
[AdvanceTableInformation("访问模块")]
[AdvanceTableMapper("access_way")]
public class AccessWay : AbstractFunctionEntity<AccessWay>, IAccessWayPure
{
    #region Properties

    [AdvanceColumnInformation("名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(50)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("识别标识")]
    [AdvanceColumnMapper("guid_tag")]
    [AdvanceColumnLength(36)]
    [AdvanceColumnNational]
    public string GuidTag { get; set; } = "";

    [AdvanceColumnInformation("相对父级路径")]
    [AdvanceColumnMapper("relative_parent_path")]
    [AdvanceColumnLength(800)]
    public string RelativeParentPath { get; set; } = "";

    [AdvanceColumnInformation("相对路径")]
    [AdvanceColumnMapper("relative_path")]
    [AdvanceColumnLength(800)]
    public string RelativePath { get; set; } = "";

    [AdvanceColumnInformation("相对父级路径等级")]
    [AdvanceColumnMapper("relative_parent_path_leve")]
    public int RelativeParentPathLevel { get; set; }

    [AdvanceColumnInformation("相对路径等级")]
    [AdvanceColumnMapper("relative_path_level")]
    [AdvanceColumnLength(800)]
    public int RelativePathLevel { get; set; }

    [AdvanceColumnInformation("扩展权限")]
    [AdvanceColumnMapper("expand")]
    [AdvanceColumnLength(500)]
    public string Expand { get; set; } = "";

    [AdvanceColumnInformation("结果类型")]
    [AdvanceColumnMapper("result_type")]
    [AdvanceColumnLength(80)]
    public string ResultType { get; set; } = "";

    [AdvanceColumnInformation("扩展权限")]
    [AdvanceColumnMapper("expand")]
    [AdvanceColumnLength(500)]
    public string Group { get; set; } = "";

    [AdvanceColumnInformation("渠道码")]
    [AdvanceColumnMapper("channel")]
    public int Channel { get; set; }

    [AdvanceColumnInformation("Ip")]
    [AdvanceColumnMapper("ip")]
    [AdvanceColumnLength(50)]
    public string Ip { get; set; } = "";

    #endregion Properties

    public sealed override Expression<Func<AccessWay, object>> GetPrimaryKeyLambda()
    {
        return o => o.Id;
    }
}