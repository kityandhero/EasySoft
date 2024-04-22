namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("key_value_application")]
public class KeyValueApplication : KeyValueBaseEntity<KeyValueApplication>, IApplicationPure
{
    #region Properties

    [AdvanceColumnMapper("application_id")]
    public long ApplicationId { get; set; } = 0;

    #endregion Properties
}