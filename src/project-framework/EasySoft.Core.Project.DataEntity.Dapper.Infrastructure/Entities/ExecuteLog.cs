namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

/// <summary>
/// 执行日志
/// </summary>
[AdvanceTableInformation("访问模块")]
[AdvanceTableMapper("execute_log")]
public class ExecuteLog : AbstractFunctionEntity<ExecuteLog>, IExecuteLogStore
{
    #region Properties

    [AdvanceColumnInformation("标记, 用于更新判断等场景")]
    [AdvanceColumnMapper("tag")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string Tag { get; set; } = "";

    [AdvanceColumnInformation("方法的命名空间")]
    [AdvanceColumnMapper("declaring_type_namespace")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string DeclaringTypeNamespace { get; set; } = "";

    [AdvanceColumnInformation("方法的类名，不包括命名空间")]
    [AdvanceColumnMapper("declaring_type_name")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string DeclaringTypeName { get; set; } = "";

    [AdvanceColumnInformation("方法名称")]
    [AdvanceColumnMapper("name")]
    [AdvanceColumnLength(2000)]
    [AdvanceColumnNational]
    public string Name { get; set; } = "";

    [AdvanceColumnInformation("参数集合序列化")]
    [AdvanceColumnMapper("parameter")]
    [AdvanceColumnNational]
    public string Parameter { get; set; } = "";

    [AdvanceColumnInformation("返回结果的类型")]
    [AdvanceColumnMapper("result_type")]
    [AdvanceColumnLength(1000)]
    [AdvanceColumnNational]
    public string ResultType { get; set; } = "";

    [AdvanceColumnInformation("执行结果序列化")]
    [AdvanceColumnMapper("result")]
    [AdvanceColumnNational]
    public string Result { get; set; } = "";

    [AdvanceColumnInformation("执行时刻的时间")]
    [AdvanceColumnMapper("execute_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime ExecuteTime { get; set; } = ConstCollection.DbDefaultDateTime;

    [AdvanceColumnInformation("执行结果返回时刻的时间")]
    [AdvanceColumnMapper("result_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime ResultTime { get; set; } = ConstCollection.DbDefaultDateTime;

    [AdvanceColumnInformation("触发渠道码")]
    [AdvanceColumnMapper("trigger_channel")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string TriggerChannel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    [AdvanceColumnInformation("Ip")]
    [AdvanceColumnMapper("ip")]
    [AdvanceColumnLength(50)]
    public string Ip { get; set; } = "";

    #endregion Properties
}