namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("key_value_section")]
public class KeyValueSection : KeyValueBaseEntity<KeyValueSection>
{
    #region Properties

    [AdvanceColumnInformation("栏目标识")]
    [AdvanceColumnMapper("section_id")]
    public long SectionId { get; set; } = 0;

    #endregion Properties
}