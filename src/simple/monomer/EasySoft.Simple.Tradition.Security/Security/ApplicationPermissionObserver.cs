using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Security.Security;

/// <summary>
/// ApplicationPermissionObserver
/// </summary>
public class ApplicationPermissionObserver : PermissionObserverCore
{
    private readonly IUserService _userService;

    /// <summary>
    /// ApplicationPermissionObserver
    /// </summary>
    /// <param name="applicationActualOperator"></param>
    /// <param name="userService"></param>
    public ApplicationPermissionObserver(
        IActualOperator applicationActualOperator,
        IUserService userService
    ) : base(applicationActualOperator)
    {
        _userService = userService;
    }

    /// <summary>
    /// OnJudging
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override bool OnJudging()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// GetCompetenceEntityCollection
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override async Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync()
    {
        var identification = GetActualOperator().GetIdentification();

        if (identification == null || !identification.IsInt64(out var id)) return new List<CompetenceEntity>();

        var result = await _userService.GetRoleGroupIdAsync(id);

        if (!result.Success) return new List<CompetenceEntity>();

        var api = RestService.For<ICompetenceEntityApi>(GeneralConfigAssist.GetPermissionServerHostUrl());

        return await api.GetCompetenceEntityCollectionAsync(result.Data);
    }
}