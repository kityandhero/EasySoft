namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

[AdvanceTableMapper("share_storage")]
public class ShareStorage : AbstractFunctionEntity<ShareStorage>
{
    #region Properties

    [AdvanceColumnMapper("user_id")]
    public long UserId { get; set; } = 0;

    [AdvanceColumnMapper("url_params")]
    [AdvanceColumnNational]
    public string UrlParams { get; set; } = "";

    #endregion Properties
}