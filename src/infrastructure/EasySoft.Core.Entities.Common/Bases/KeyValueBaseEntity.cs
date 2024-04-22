namespace EasySoft.Core.Entities.Common.Bases;

public abstract class KeyValueBaseEntity<T> : AbstractFunctionEntity<T> where T : BaseEntity<T>
{
    #region Properties

    [AdvanceColumnInformation("标题")]
    [AdvanceColumnMapper("title")]
    [AdvanceColumnLength(100)]
    [AdvanceColumnNational]
    public string Title { get; set; } = "";

    [AdvanceColumnInformation("唯一标记")]
    [AdvanceColumnMapper("tag")]
    [AdvanceColumnLength(50)]
    public string Tag { get; set; } = "";

    [AdvanceColumnInformation("键")]
    [AdvanceColumnMapper("key")]
    [AdvanceColumnLength(100)]
    public string Key { get; set; } = "";

    [AdvanceColumnInformation("值")]
    [AdvanceColumnMapper("value")]
    [AdvanceColumnNational]
    public string Value { get; set; } = "";

    #endregion Properties
}