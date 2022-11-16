using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Attributes;

public class KeyValueDefinitionAttribute : DescriptionAttribute
{
    private readonly string _tag;

    /// <summary>
    /// 值的模式
    /// </summary>
    public KeyValueType Type { get; }

    /// <summary>
    /// 数据标记
    /// </summary>
    public string Tag
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_tag)) throw new Exception($"Tag不能为空(description:{Description})");

            return _tag;
        }
    }

    /// <summary>
    /// 默认值
    /// </summary>
    public string DefaultValue { get; }

    public KeyValueDefinitionAttribute() : this("", "", KeyValueType.String)
    {
    }

    public KeyValueDefinitionAttribute(
        string description,
        string tag,
        KeyValueType type,
        string defaultValue = ""
    ) : base(description)
    {
        _tag = tag;
        Type = type;
        DefaultValue = defaultValue;
    }

    public KeyValueDefinitionAttribute(
        string description,
        string tag,
        bool defaultValue
    ) : base(description)
    {
        _tag = tag;
        Type = KeyValueType.Boolean;
        DefaultValue = defaultValue ? ((int)Whether.Yes).ToString() : ((int)Whether.No).ToString();
    }
}