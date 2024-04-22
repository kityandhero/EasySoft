namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// IExecuteLog
/// </summary>
public interface IExecuteLog
{
    /// <summary>
    /// 标记, 用于更新判断等场景
    /// </summary>
    string Tag { get; set; }

    /// <summary>
    /// 方法的命名空间
    /// </summary>
    string DeclaringTypeNamespace { get; set; }

    /// <summary>
    /// 方法的类名，不包括命名空间
    /// </summary>
    string DeclaringTypeName { get; set; }

    /// <summary>
    /// 方法名称
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// 参数集合序列化
    /// </summary>
    string Parameter { get; set; }

    /// <summary>
    /// 返回结果的类型
    /// </summary>
    string ResultType { get; set; }

    /// <summary>
    /// 执行结果序列化
    /// </summary>
    string Result { get; set; }

    /// <summary>
    /// 执行时刻的时间
    /// </summary>
    DateTime ExecuteTime { get; set; }

    /// <summary>
    /// 执行结果返回时刻的时间
    /// </summary>
    DateTime ResultTime { get; set; }
}