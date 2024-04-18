using System.Linq.Expressions;
using EasySoft.Core.Dapper.Base;
using EasySoft.Core.Sql.Common;
using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.Core.Entities.Common.Bases;

public abstract class AbstractFunctionEntity<T> : BaseEntity<T> where T : BaseEntity<T>
{
    #region Properties

    [AdvanceColumnInformation("主键标识")]
    [AdvanceColumnMapper(Constants.DefaultTablePrimaryKey)]
    public override long Id { get; set; } = 0;

    #endregion
}